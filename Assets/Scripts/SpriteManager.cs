using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpriteManager : MonoBehaviour {

    public string kind;
    public bool button;
    public Sprite[] sprites;

	// Use this for initialization
	void OnEnable () {
        if (button)
        {
            GetComponent<Image>().sprite = sprites[PlayerPrefs.GetInt("using" + kind, 0)];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[PlayerPrefs.GetInt("using" + kind, 0)];
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
