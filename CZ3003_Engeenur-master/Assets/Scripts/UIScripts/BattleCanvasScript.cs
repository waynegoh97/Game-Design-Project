//using UnityEngine;
//using System.ComponentModel;
//using UnityWeld.Binding;

//[Binding]
//public class BattleCanvasScript : MonoBehaviour, INotifyPropertyChanged
//{
//    private float timer = 0;
//    public event PropertyChangedEventHandler PropertyChanged;
//    private string monsterName = "<Monster name>";
//    //private string monsterHealth = "<Monster health>";
//    private int count = 0;
//    public BattleControllerScript battleControllerScript;

//    [Binding]
//    public string MonsterName
//    {
//        get
//        {
//            return monsterName;
//        }
//        set
//        {
//            if (monsterName == value)
//            {
//                return; // No change.
//            }

//            monsterName = value;

//            OnPropertyChanged("MonsterName");
//        }
//    }

//    [Binding]
//    public string MonsterHealth
//    {
//        get
//        {
//            if (battleControllerScript.monsterScript)
//                return battleControllerScript.monsterScript.Health.ToString();
//            return "";
//        }
//        set
//        {
//            if (battleControllerScript.monsterScript.Health == int.Parse(value))
//            {
//                return; // No change.
//            }

//            battleControllerScript.monsterScript.Health = int.Parse(value);

//            if (PropertyChanged != null && battleControllerScript.monsterScript)
//            {
//                //OnPropertyChanged("MonsterHealth");
//                PropertyChanged(battleControllerScript.monsterScript, new PropertyChangedEventArgs("Health"));
//            }
//        }
//    }


//    private void OnPropertyChanged(string propertyName)
//    {
//        if (PropertyChanged != null)
//        {
//            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        MonsterHealth = battleControllerScript.monsterScript.Health.ToString();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        timer += Time.deltaTime;

//        if (timer >= 1f)
//        {
//            count++;
//            MonsterName = count.ToString();
//            timer = 0f;
//        }

//    }
//}
