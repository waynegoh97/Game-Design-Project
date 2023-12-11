using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControllerScript : MonoBehaviour
{
    List<GameObject> listOfMonsters = new List<GameObject>();
    public GameObject monsterPrefab;

    private void Awake()
    {
        //TODO load monsters from database
        //GameObject monster = new GameObject();
        //monster.AddComponent<MonsterScript>().init(1, 50, 10);
        //listOfMonsters.Add(monster);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getMonster(int _id)
    {
        for (int i = 0; i < listOfMonsters.Count; ++i)
        {
            if (_id == listOfMonsters[i].GetComponent<MonsterScript>().Id)
            {
                return listOfMonsters[i];
            }
        }
        return null;
    }
}
