using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
/// <summary>
/// <br>Author: Heng Fuwei, Esmond</br> 
/// 
/// </summary>
public class TrFriendViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader, specialLevelCanvas;
    public InputField friendInput;
    public static string userName="Jame"; //retrieve user
    public bool chk = false;
    //public SpecialLevelController specialLevelController;
    private bool chkUsernameResult = false;

    public UIControllerScript UIController;
    

    // Start is called before the first frame update
    async void Start()
    {
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    // Create friend buttons
    public async Task Read()
    {
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        FriendDatabaseQAHandler.GetFriends(friends =>
        {
            foreach (var friend in friends)
            {
                Debug.Log($"{friend.Key} {friend.Value.userName}");
                if (friend.Value.userName == userName)
                {
                    GameObject tmp_btn = Instantiate(item, itemParent.transform);
                    tmp_btn.name = friend.Key;
                    Debug.Log("item name: " + tmp_btn.name);
                    tmp_btn.transform.GetChild(1).GetComponent<Text>().text = friend.Key;
                }
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Read elapsed milliseconds: {0}" + sec);
    }

    //When Cancel is clicked 
    public void Cancel()
    {
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    }

    //friend creation check
    public async void CreateFriend()
    {
        string friendName = friendInput.text;
        bool handler = Check();
        if (handler == true)
        {
            await InvokeFriendCheckExist(friendName);
            await CheckUsernameExist(friendName); //Check if this username already has an account 
            if (chkUsernameResult)
            {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "friend userame does not exist.";
            }
        }
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
        chk = false;
    }


    //Basic checking of form elements
    public bool Check()
    {
        string str = Regex.Replace(friendInput.text, @"\s", "");
        if (friendInput.text == null || str == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a friend name.";
            return false;
        }
        return true;
    }

    // Invoke checkfriendExist
    public async Task InvokeFriendCheckExist(string friendName)
    {
        Task<bool> task = CheckFriendExist(friendName);
        bool friendExist = await task;
        Debug.Log("Friend Exist: " + friendExist);
        if (friendExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = friendName + " already exist in your friend list.";
        }
        else
        {
            List<string> students = new List<string>();
            var friend = new Friend(userName, students);
            loader.SetActive(true);
            await PostingFriend(friend, friendName);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = friendName + " created successfully.";

        }
    }

    // Creating friend in database
    public async Task PostingFriend(Friend friend, string friendName)
    {
        FriendDatabaseQAHandler.PostFriend(friend, friendName, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating friend Elapsed milliseconds: {0}" + sec);
    }

    // Check if the friend already exist
    public async Task<bool> CheckFriendExist(string friendName)
    {
        FriendDatabaseQAHandler.GetFriend(friendName, friend =>
        {
            chk = true;
            Debug.Log("chFriendExist1" + chk);
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(500).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("check friend exist elapsed milliseconds: {0}" + sec);
        return chk;
    }


    //Check the name of the friend to be deleted
    public string friendKey;
    public void CheckItemDelName(GameObject item)
    {
        friendKey = item.name;
        Debug.Log("CHECK DELETE name: " + friendKey);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete " + friendKey + "?";

    }

    //Delete friend in database
    public async void DeleteFriend()
    {
        loader.SetActive(true);
        Debug.Log("DELETE name: " + friendKey);
        FriendDatabaseQAHandler.DeleteFriend(friendKey, () => { });
       
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Delete elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = friendKey + " deleted successfully.";
        friendKey = null;
    }
    public async Task CheckUsernameExist(string friendName)
    {
        chkUsernameResult = false;
        LoginDbHandler.GetUsers(userDatas =>
        {
            foreach (var userData in userDatas)
            {
                Debug.Log($"{userData.Key}");
                if (userData.Value.userName == friendName)
                {
                    chkUsernameResult = true;
                    break;
                }
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Username check elapsed milliseconds: {0}" + sec);
    }
}
