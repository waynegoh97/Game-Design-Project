using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private int health;
    private string userName;
    private int damage;
    private int experience;
    private int level;
    private int coin;

    public int Health { get => health; set => health = value; }
    public string UserName { get => userName; set => userName = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Experience { get => experience; set => experience = value; }
    public int Level { get => level; set => level = value; }
    public int Coin { get => coin; set => coin = value; }

    // Start is called before the first frame update

    public void init(string _userName, int _health, int _damage, int _experience, int _level, int _coin)
    {
        health = _health;
        userName = _userName;
        damage = _damage;
        experience = _experience;
        level = _level;
        coin = _coin;
}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
