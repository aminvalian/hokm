using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinNavigatorScript : MonoBehaviour {

    public GameObject des;
    private Vector2 destination;
    public AudioSource coin;
    Vector2 force;
	// Use this for initialization
	void Start () {
        destination = des.GetComponent<RectTransform>().position;
        coin.Play();
        
        StartCoroutine(move());
        
	}
	
	// Update is called once per frame
	private IEnumerator move () {
        while (GetComponent<RectTransform>().position.x - destination.x > 20 || GetComponent<RectTransform>().position.y - destination.y > 20)
        {
            force = new Vector2(destination.x - GetComponent<RectTransform>().position.x, destination.y - GetComponent<RectTransform>().position.y);
            GetComponent<RectTransform>().position = new Vector2(GetComponent<RectTransform>().position.x + (force.x * 0.13f), GetComponent<RectTransform>().position.y + (force.y * 0.13f));
            
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
