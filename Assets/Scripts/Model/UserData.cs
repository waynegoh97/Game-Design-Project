using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string email;
    public string userName;
    public int level;
    public int experience;
    public int maxExperience;
    public int hp;
    public int coin;
    public bool verified;
    public string localId;

    public UserData()
    {
        this.email = email;
        this.userName = userName;//email
        this.level = 1;
        this.experience = 0;
        this.maxExperience = 100;
        this.hp = 100;
        this.coin = 0;
        this.verified = verified;
        this.localId = localId;
    }

    //  public string getUserName()
    // {
    //     return email;
    // }

    public string getEmail(){
        return email;
    }

    public string getName()
    {
        return userName;
    }

    public int getLevel()
    {
        return level;
    }

    public void setLevel(int _level)
    {
        level = _level;
    }

    public int getExperience()
    {
        return experience;
    }

    public void setExperience(int _experience)
    {
        experience = _experience;
    }

    public int getMaxExperience()
    {
        return maxExperience;
    }

    public int getHp()
    {
        return hp;
    }

    public int getCoin()
    {
        return coin;
    }

    public void setCoin(int _coin)
    {
        coin = _coin;
    }

    public bool getVerified()
    {
        return verified;
    }
}