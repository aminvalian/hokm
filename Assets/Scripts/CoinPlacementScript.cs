using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CoinPlacementScript : MonoBehaviour {

    public Text textObject;
    public Vector2 origin;
    public float distance;
	// Update is called once per frame
	void Update ()
    {
        if(PlayerPrefs.GetInt("usinglang",1) == 1)
            transform.localPosition = new Vector2(origin.x + (distance * 0.8f * (textObject.text.Length - 1)), origin.y);
        else
            transform.localPosition = new Vector2(origin.x + (distance * (textObject.text.Length - 1)), origin.y);

    }
}
