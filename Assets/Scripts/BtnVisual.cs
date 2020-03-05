using UnityEngine;
using System.Collections;

public class BtnVisual : MonoBehaviour {
    RectTransform trans;
    public float t = 0.01f;
    // Use this for initialization
    void Start () {
        trans = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
	    if( Mathf.Abs( trans.localScale.x)  <= 1)
        {
            trans.localScale = new Vector3(trans.localScale.x-t,1, 1);
        }
        else
        {
            trans.localScale = new Vector3(Mathf.Abs(trans.localScale.x)/(trans.localScale.x), 1, 1);
            t = -t;
        }
	}
}
