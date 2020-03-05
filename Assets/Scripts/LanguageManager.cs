using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {
    public Font yekta;
    public Font myriad;
    public bool doEnglish;
    public List<Text> texts;
    
    public List<string> farsi;
    public List<string> engl;

    // Use this for initialization
    void Start () {
        adjustLang();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void adjustLang()
    {
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].text = farsi[i];
                texts[i].font = yekta;
            }
        }
        else if(doEnglish)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].text = engl[i];
                texts[i].font = myriad;

            }
        }
    }
}
