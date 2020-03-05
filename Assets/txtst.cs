using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ArabicSupport;

[ExecuteInEditMode]
public class txtst : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        text.text = ArabicFixer.Fix("1234  ") + "ﮋﭘ ﭻﮔ ﻦﻤﻠﮐ ﯽﻄﺣ ﺯﻮﻫ ﺪﺠﺑﺍ";
    }
}
