using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Canvas options_canvas;
    public Canvas help_canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBodyPersistence(float value)
    {
        GlobalVariables.BODY_PERSISTENCE = value;
    }

    public void ToggleOptions()
    {
        options_canvas.gameObject.SetActive(!options_canvas.gameObject.activeSelf);
    }

    public void ToggleHelp()
    {
        help_canvas.gameObject.SetActive(!help_canvas.gameObject.activeSelf);
    }

}
