using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Twity.DataModels.Core;

/// <summary>
/// Author: Tan Soo Yong <br/>
/// Controller class for sharing Battle Results to user's Twitter account which includes Twitter authentication
/// </summary>
public class TwitterShareController : MonoBehaviour
{
    public GameObject popup;
    public InputField pinField;
    public Button submitBtn;
    public Text info;

    public static string CONSUMER_KEY = "LxL3mKtWPRCkELljFghQL7ae2";
    public static string CONSUMER_SECRET = "w2qVN3IznTkcOpthcnOadXH6dsBDu5945XUHNDiFFYqreXiUQj";

    private int level = 0;
    private int score = 0;

    public static string screenshotBase64;

    /// <summary>
    /// Setter method for getting game Level and Score from Battle System
    /// </summary>
    /// <param name="level">Completed game level</param>
    /// <param name="score">Score of completed level</param>
    public void setResults(int level, int score) {
        this.level = level;
        this.score = score;
    }

    /// <summary>
    /// OnClick Handler for Share button to trigger sharing to Twitter
    /// </summary>
    #region Twity API
    public void ShareToTwitter() {
        StartCoroutine(TakeScreenshot());
        Twity.Oauth.consumerKey = CONSUMER_KEY;
        Twity.Oauth.consumerSecret = CONSUMER_SECRET;

        StartCoroutine(Twity.Client.GenerateRequestToken(RequestTokenCallback));
    }

    /// <summary>
    /// Callback method after generating Twitter RequestToken
    /// </summary>
    /// <param name="success">Status after generating Twitter RequestToken</param>
    void RequestTokenCallback(bool success) {
        if (!success) {
            return;
        }
        info.text = "Enter 7-Digit PIN";
        Application.OpenURL(Twity.Oauth.authorizeURL);
        pinField.text = string.Empty;
        popup.SetActive(true);
    }

    /// <summary>
    /// Generate Twitter AccessToken with user entered PIN
    /// </summary>
    /// <param name="pin">Twitter 7-Digit PIN entered by user</param>
    void GenerateAccessToken(string pin) {
        // pin is numbers displayed on web browser when user complete authorization.
        StartCoroutine(Twity.Client.GenerateAccessToken(pin, AccessTokenCallback));
    }

    /// <summary>
    /// Callback method after generating Twitter AccessToken
    /// </summary>
    /// <param name="success">Status after generating Twitter AccessToken</param>
    void AccessTokenCallback(bool success) {
        if (!success) {
            info.text = "Wrong PIN. Try Again.";
            StartCoroutine(Twity.Client.GenerateRequestToken(RequestTokenCallback));
            return;
        }
        // When success, authorization is completed. You can make request to other endpoint.
        // User's screen_name is in '`Twity.Client.screenName`.
        info.text = "Authentication Successful! Posting Tweet...";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters["media_data"] = screenshotBase64;
        StartCoroutine(Twity.Client.Post("media/upload", parameters, MediaUploadCallback));
    }

    /// <summary>
    /// Callback method after uploading media to Twitter API
    /// </summary>
    /// <param name="success">Status after uploading media</param>
    /// <param name="response">Response received from Twitter API to get media_ids to update Tweet</param>
    void MediaUploadCallback(bool success, string response) {
        if (success) {
            UploadMedia media = JsonUtility.FromJson<UploadMedia>(response);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["media_ids"] = media.media_id.ToString();
            parameters["status"] = $"I cleared level {this.level} with a score of {this.score}!";
            StartCoroutine(Twity.Client.Post("statuses/update", parameters, StatusesUpdateCallback));
        } else {
            Debug.Log(response);
        }
    }

    /// <summary>
    /// Callback method after posting a Tweet
    /// </summary>
    /// <param name="success">Status after posting a Tweet</param>
    /// <param name="response"></param>
    void StatusesUpdateCallback(bool success, string response) {
        if (success) {
            info.text = "Battle Results Posted! You may close this popup.";
            Debug.Log("Tweet Posted");
        } else {
            Debug.Log(response);
        }
    }
    #endregion

    /// <summary>
    /// OnClick Handler for Popup Submit button
    /// </summary>
    public void submitBtnListener() {
        string pin = pinField.text;
        GenerateAccessToken(pin);
    }

    /// <summary>
    /// Take Screenshot
    /// </summary>
    IEnumerator TakeScreenshot() {
        yield return new WaitForEndOfFrame();

        Texture2D screenImg = new Texture2D(Screen.width, Screen.height);
        screenImg.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImg.Apply();
        screenshotBase64 = System.Convert.ToBase64String(screenImg.EncodeToPNG());
    }

    /// <summary>
    /// Close Sharing Popup window
    /// </summary>
    public void closePopup() {
        popup.SetActive(false);
    }
}
