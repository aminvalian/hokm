using UnityEngine;
using System.Collections;

public class oldGUI : MonoBehaviour
{
    public string textFieldString = "ﹰﺎﻀﻳﺃ ﻢﻳﺪﻘﻟﺍ ﻡﺪﺨﺘﺴﻤﻟﺍ ﺔﻬﺟﺍﻭ ﻡﺎﻈﻧ ﻢﻋﺪﻳ";
    
    void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 100, 90), "ﺔﻴﺳﺎﺳﺄﻟﺍ ﺔﻤﺋﺎﻘﻟﺍ");

        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if (GUI.Button(new Rect(20, 40, 80, 20), "ﺃﺪﺑﺇ"))
        {
           
        }

        // Make the second button.
        if (GUI.Button(new Rect(20, 70, 80, 20), "ﺝﻭﺮـﺨﻟﺍ"))
        {
           
        }
           textFieldString = GUI.TextField (new Rect (150, 10, 180, 70), textFieldString); 
    
    }
}