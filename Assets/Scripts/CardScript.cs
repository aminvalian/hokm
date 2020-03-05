using UnityEngine;
using System.Collections;

public class CardScript : MonoBehaviour {

	public bool isActive = false;
	public bool isReady = false;
	public bool moving = false;
    public bool t = false;
    public int rank;
	public int kind;
    public int index;
	// Spade = 1 , Heart = 2 , club = 3 , diamond = 4
	public int value;
	public PlayerHand playerHand;
	DeckScript deck;
	public Vector2 pos;
    public float rotation = 0;
    public float angle;
    public int sortingOrder;
    public Vector2 cardPos1;
    public Vector2 mousePos1 = Vector2.zero;
    public Vector2 mousePos2 = Vector2.zero;

    // Use this for initialization
    void Start () {
		playerHand = GameObject.Find("Background").GetComponent<PlayerHand>();
		deck = GameObject.Find("cardDeck").GetComponent<DeckScript>();
        rank = value;
        

	}
	
	// Update is called once per frame
	void Update () {


        if (isReady)
        {
#if UNITY_EDITOR
            mousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
                t = true;
#endif
#if UNITY_ANDROID
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    mousePos2 = Camera.main.ScreenToWorldPoint(touch.position);
                else if (touch.phase == TouchPhase.Ended)
                    t = true;
            }
#endif
#if UNITY_IOS
            foreach (Touch touch in Input.touches){
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    mousePos2 = Camera.main.ScreenToWorldPoint(touch.position);
                else if (touch.phase == TouchPhase.Ended)
                   t = true;
            }
#endif
            if (mousePos2 != mousePos1)
            {
                transform.position = mousePos2;

                rotation = -(mousePos1.y - mousePos2.y) / (Mathf.Sqrt(Mathf.Pow((mousePos2.y - mousePos1.y), 2) + Mathf.Pow((mousePos2.x - mousePos1.x), 2))) + (mousePos1.x - mousePos2.x) / (Mathf.Sqrt(Mathf.Pow((mousePos2.y - mousePos1.y), 2) + Mathf.Pow((mousePos2.x - mousePos1.x), 2)));

                transform.RotateAround(mousePos2, Vector3.forward, (mousePos1.y - mousePos2.y) / (Mathf.Sqrt(Mathf.Pow((mousePos2.y - mousePos1.y), 2) + Mathf.Pow((mousePos2.x - mousePos1.x), 2))) + (mousePos1.x - mousePos2.x) / (Mathf.Sqrt(Mathf.Pow((mousePos2.y - mousePos1.y), 2) + Mathf.Pow((mousePos2.x - mousePos1.x), 2))));
                mousePos1 = mousePos2;
            }
            if (t)
            {
                if (transform.position.y > -2)
                {
                    isReady = false;
                    isActive = false;
                    moving = true;
                    GetComponent<SpriteRenderer>().sortingOrder = deck.onBoardCards.Count + 61;

                }
                else
                {
                    isReady = false;
                    t = false;
                    playerHand.putcardBack();
                }
            }
        }
        if (moving) {
            float x = -transform.position.x * 40;
            float y = -transform.position.y * 40;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
            transform.Rotate(new Vector3(0, 0, rotation));
            if (Mathf.Abs(rotation) > 1)
            {
                rotation -= Mathf.Abs(rotation) / rotation;

            }
            else
            {
                rotation = 0;
            }
            if (Mathf.Sqrt(Mathf.Pow(transform.position.y, 2) + Mathf.Pow(transform.position.x, 2)) < 0.5)
            {



                transform.position = new Vector2(0f, 0f);
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                rotation = 0;
                t = false;
                moving = false;
                deck.onBoardCards.Add(this);
                playerHand.removeCard(kind, value);
                playerHand.removeCard(kind, value);
                playerHand.setAllInactive();
                deck.moveNext();
            }
            
        }

    }

	public void clicked() {
		if( isActive){
#if UNITY_EDITOR
            mousePos1 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            cardPos1 = transform.position;

#endif

#if UNITY_STANDALONE_WIN
            mousePos1 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            cardPos1 = transform.position;

#endif
#if UNITY_ANDROID
            foreach (Touch touch in Input.touches)
                if (touch.phase == TouchPhase.Began)
                {
                    cardPos1 = transform.position;
                    mousePos1 = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).x);

                }
#endif
#if UNITY_IOS
            foreach (Touch touch in Input.touches)
                if (touch.phase == TouchPhase.Began)
                {
                    cardPos1 = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                    mousePos1 = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).x);
                    mousePos2 = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).x);

                }
#endif
            GetComponent<SpriteRenderer>().sortingOrder = 61 + deck.onBoardCards.Count;
            isReady = true;

        }
    }


    public IEnumerator Flip(int pos , Vector2 position, CardDownBigScript c) {
        transform.position = position;
        GetComponent<AudioSource>().Play();
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
            while (transform.localScale.x < 0.97f)
            {
                transform.localScale = new Vector3(transform.localScale.x + 0.08f, transform.localScale.y, transform.localScale.z);
                yield return new WaitForSeconds(0.01f);
            }
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        Destroy(c.gameObject);
        
    }

}
