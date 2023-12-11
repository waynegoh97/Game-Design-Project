using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData
{
    public int health, attack, coin, experience;
    public string monsterName;

    public MonsterData(int hp, int att, int c, int exp, string mons)
    {
        health = hp;
        attack = att;
        coin = c;
        experience = exp;
        monsterName = mons;
    }
}
