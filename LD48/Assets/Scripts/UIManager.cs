using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text health_text;

    // Start is called before the first frame update
    void Start()
    {
        ServiceLocator.SetUIManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(int value)
    {
        health_text.text = $"Health: {value}";
    }
}
