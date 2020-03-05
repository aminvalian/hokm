using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HokmFadeScript : MonoBehaviour {
    public float speed;
    float t = 1;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject,2f);
        StartCoroutine(move());
    }
	
	// Update is called once per frame
	private IEnumerator move () {
        GetComponent<Image>().CrossFadeAlpha(0f, 1.5f, false);

        while (Vector3.Distance(GetComponent<RectTransform>().position, Vector3.zero) > 10)
        {
            GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x - (GetComponent<RectTransform>().position.x / 10), GetComponent<RectTransform>().position.y + (GetComponent<RectTransform>().position.y / 10), 0);
            GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x + 0.01f, GetComponent<RectTransform>().localScale.y + 0.01f, 1);
            Color color = GetComponent<Image>().material.color;
            yield return new WaitForSeconds(0.05f);
        }
        
        Color col = GetComponent<Image>().material.color;
        col.a = 1f;
        GetComponent<Image>().material.color = col;
        Destroy(gameObject);
        Debug.Log(col.a);
    }

    public IEnumerator Fade()
    {
        float t = 1;
        Color color = GetComponent<SpriteRenderer>().material.color;
        while (t >= 0f)
        {
            color.a = Mathf.Lerp(1f, 0f, t);
            GetComponent<SpriteRenderer>().material.color = color;
            t -= Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
