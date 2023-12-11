using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepInputFieldInFocusScript : MonoBehaviour
{
    InputField inputfield;
    // Start is called before the first frame update

    void Awake()
    {
        inputfield = gameObject.GetComponent<InputField>();
    }

    public void focusInputField()
    {
        inputfield.ActivateInputField();
    }

    void Start()
    {
        inputfield.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
