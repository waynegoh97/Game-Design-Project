using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private int id;
    private int health;
    private int damage;
    private string monsterName;
    private int coin;
    private int experience;

    public MonsterScript(int _id, string _monsterName, int _health, int _damage, int _coin, int _experience)
    {
        id = _id;
        monsterName = _monsterName;
        health = _health;
        damage = _damage;
        coin = _coin;
        experience = _experience;
    }

    public int Id { get => id; set => id = value; }
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }
    public string MonsterName { get => monsterName; set => monsterName = value; }
    public int Coin { get => coin; set => coin = value; }
    public int Experience { get => experience; set => experience = value; }

    public void init(int _id, int _health, int _damage, int _coin, int _experience)
    {
        id = _id;
        health = _health;
        damage = _damage;
        coin = _coin;
        experience = _experience;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
