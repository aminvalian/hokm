using UnityEngine;
using System.Collections;

public class CardDownBigScript : MonoBehaviour {

    public Sprite Bad;
    public int pos;
    public Vector2 position;
    public GameObject card;
    bool running = false;
	// Use this for initialization
	void Start () {
        if (pos == 1)
        {
            transform.position = new Vector2(8.5f, 2.7f);
            position = new Vector2(1.65f, 2.7f);
            transform.eulerAngles = new Vector3(0, 0, 90);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-570, 0));
        }
        if (pos == 2)
        {
            transform.position = new Vector2(-1.73f, 10.8f);
            position = new Vector2(-1.73f, 2.41f);
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-770));
        }
        if (pos == 0)
        {
            transform.position = new Vector2(1.73f, -10.8f);
            position = new Vector2(1.73f, -2.41f);
            transform.eulerAngles = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 770));
        }
        if (pos == 3)
        {
            transform.position = new Vector2(-8.5f, -1.5f);
            position = new Vector2(-1.65f, -1.5f);
            transform.eulerAngles = new Vector3(0, 0, 270);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(570, 0));
        }
    }
	
	// Update is called once per frame
	void Update () {
        float x = Mathf.Abs(transform.position.x - position.x);
        float y = Mathf.Abs(transform.position.y - position.y);
        if (x + y < 0.3 && !running)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = position;


            transform.localScale = new Vector3(transform.localScale.x - 0.09f, transform.localScale.y, transform.localScale.z);
            if (transform.localScale.x < 0.05f)
            {
                running = true;
                GetComponent<SpriteRenderer>().sprite = null;
                StartCoroutine(card.GetComponent<CardScript>().Flip(pos, position,this));
            }


        }
    }
}
