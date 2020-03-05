using UnityEngine;
using System.Collections;

public class ScreenResolutionPlacement : MonoBehaviour {
    

    public bool isUI;
    public bool fixOnStart;

    // Use this for initialization
    float xPos;
    float yPos;

    void Start()
    {
        if(fixOnStart)
            Adjust();
    }

    public void Adjust()
    {
        

        if (isUI)
        {
            xPos = GetComponent<RectTransform>().position.x;
            yPos = GetComponent<RectTransform>().position.y;
        }
        else
        {
            xPos = transform.position.x;
            yPos = transform.position.y;
        }

        float xRes = Screen.width;
        float yRes = Screen.height;
        float xdiff = (yRes / 1.6f) - xRes;
        float x = xPos - (xdiff/ (yRes / 1.6f)) * xPos;

        if (isUI)
        {
            GetComponent<RectTransform>().position = new Vector2(x, yPos);
        }
        else
        {
            transform.position = new Vector2(x, yPos);
        }
    }
	
	
}
