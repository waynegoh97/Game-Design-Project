using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerScript : MonoBehaviour
{
    public List<LevelScript> listOfLevels = new List<LevelScript>();
    // Start is called before the first frame update
    void Start()
    {
        //TODO load levels from database
        LevelScript level = new LevelScript();
        for (int i = 0; i < 3; ++i)
        {
            level.levelNumber = i;
            listOfLevels.Add(level);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
