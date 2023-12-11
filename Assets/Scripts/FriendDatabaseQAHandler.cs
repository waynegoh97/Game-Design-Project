using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
/// <summary>
/// <br>Author: Heng Fuwei, Esmond</br> 
/// 
/// </summary>
public static class FriendDatabaseQAHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void DelFriendCallback();
    public delegate void PostFriendCallback();
    public delegate void GetFriendCallback(Friend friend);
    public delegate void GetFriendsCallback(Dictionary<string, Friend> friends);
    public delegate void GetUsersCallback(Dictionary<string, UserData> userDatas);

    //Create a Friend
    public static void PostFriend(Friend friend, string friendName, PostFriendCallback callback)
    {
        RestClient.Put<Friend>($"{databaseURL}friend/{friendName}.json", friend).Then(response => { callback(); });
    }

    //Retrieve All Friend
    public static void GetFriends(GetFriendsCallback callback)
    {
        RestClient.Get($"{databaseURL}friend.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Friend>), ref deserialized);

            var friends = deserialized as Dictionary<string, Friend>;
            callback(friends);
        });
    }

    //Retrieve a friend
    public static void GetFriend(string friendName, GetFriendCallback callback)
    {
        RestClient.Get<Friend>($"{databaseURL}friend/{friendName}.json").Then(friend => { callback(friend); });
    }

    //Delete a friend
    public static void DeleteFriend(string friendKey, DelFriendCallback callback)
    {
        RestClient.Delete($"{databaseURL}friend/" + friendKey + ".json").Then(response => { callback(); });
    }

    //Update a friend
    public static void PutFriend(string friendName, Friend friend, PostFriendCallback callback)
    {
        RestClient.Put($"{databaseURL}friend/"+friendName+".json", friend).Then(response => { callback(); });
    }

    

    //Retrieve all user details
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}students.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);

            var userDatas = deserialized as Dictionary<string, UserData>;
            callback(userDatas);
        });
    }



  

}