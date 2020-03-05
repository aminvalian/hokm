using UnityEngine;
using System.Collections;

public class MsgVisual : MonoBehaviour
{
    RectTransform trans;
    public float t;
    // Use this for initialization
    void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        trans.localScale = new Vector3(trans.localScale.x + t, trans.localScale.x + t, 1);
        
        if(trans.localScale.x >2)
        {
            t = - Mathf.Abs(t);
        }
        else if(trans.localScale.x <1)
        {
            t = Mathf.Abs(t);
        }
    }
}
