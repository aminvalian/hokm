using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour {
	public List<GameObject> cards = new List<GameObject>();
	public DeckScript deck;

	public AI oppAI;
	public AI friendAI;
	public AI opp2AI;

	// Use this for initialization
	void Start () {
		deck = GameObject.Find("cardDeck").GetComponent<DeckScript>();

	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)){
			RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,1<<9);
			if (hit != null){
				int j = 0;
				int k = -1;
				for (int i = 0; i<hit.Length; i++){
					if (hit[i].transform != null){
						if (hit[i].transform.tag == "card"){
							if (hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder>j){
								k = i;
								j = hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
							}
						}
					}
				}
                if (k >= 0)
                {
                    hit[k].transform.GetComponent<CardScript>().clicked();
                }
			}
		}
#endif
#if UNITY_ANDROID
        foreach (Touch touch in Input.touches)
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << 9);
                if (hit != null)
                {
                    int j = 0;
                    int k = -1;
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].transform != null)
                        {
                            if (hit[i].transform.tag == "card")
                            {
                                if (hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder > j)
                                {
                                    k = i;
                                    j = hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
                                }
                            }
                        }
                    }
                    if (k >= 0)
                    {
                        hit[k].transform.GetComponent<CardScript>().clicked();
                    }
                }
            }
#endif
#if UNITY_IOS
         foreach (Touch touch in Input.touches)
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 1 << 9);
                if (hit != null)
                {
                    int j = 0;
                    int k = -1;
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].transform != null)
                        {
                            if (hit[i].transform.tag == "card")
                            {
                                if (hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder > j)
                                {
                                    k = i;
                                    j = hit[i].transform.GetComponent<SpriteRenderer>().sortingOrder;
                                }
                            }
                        }
                    }
                    if (k >= 0)
                    {
                        hit[k].transform.GetComponent<CardScript>().clicked();
                    }
                }
            }
#endif
    }

    public void sortHand(List<GameObject> hand){
		for(int i =0; i<cards.Count;i++){
			Destroy(cards[i]);
			cards.RemoveAt(i);
			i--;
		}
		List<GameObject> sortedCards = new List<GameObject>();
		int p = 0;
		int q = 0;
		for (int j = 0; j < 4 ; j++){
			for (int i = 0; i< hand.Count ;i++){
				if(hand[i].GetComponent<CardScript>().kind == j){
					sortedCards.Add(hand[i]);
					p++;
				}
			}
			for(int k = q; k<p-1; k++){
				for(int z = k+1; z<p ; z++){
					if (sortedCards[k].GetComponent<CardScript>().value>sortedCards[z].GetComponent<CardScript>().value){
						GameObject g = sortedCards[k];
						sortedCards[k] = sortedCards[z];
						sortedCards[z] = g;
					}
				}
			}
			q = p;
		}
		float x = -3.63f;
		float y = -5f;
		for(int i = 2 ; i <=sortedCards.Count+1 ; i++){
			GameObject g = Instantiate(sortedCards[i-2],new Vector2(x+(i*0.5f),y- (Mathf.Abs(i-7f)*0.16f)),Quaternion.AngleAxis(-(i-7f)*5f,Vector3.forward))as GameObject;
			g.GetComponent<SpriteRenderer>().sortingOrder = i+1;
            g.GetComponent<CardScript>().sortingOrder = i + 1;
            g.GetComponent<CardScript>().pos = g.transform.position;
            g.GetComponent<CardScript>().angle = g.transform.eulerAngles.z;
            g.GetComponent<CardScript>().mousePos2 = g.transform.position;
			cards.Add(g);
		}
	}
	
	public void removeCard(int kind, int value){
		for(int i = 0; i<cards.Count; i++){
			if(kind == cards[i].GetComponent<CardScript>().kind){
				if ( value == cards[i].GetComponent<CardScript>().value){
					cards.RemoveAt(i);
				}
			}
		}
	}

	public void sortPlayableCards(int kind){
		List<CardScript> playableCards = new List<CardScript>();
		for(int i = 0; i<cards.Count;i++){
			if(cards[i].GetComponent<CardScript>().kind == kind)
				playableCards.Add(cards[i].GetComponent<CardScript>());
		}
		if(playableCards.Count==0){
			for(int i = 0; i<cards.Count;i++){
				playableCards.Add(cards[i].GetComponent<CardScript>());
			}
		}
		for(int i = 0; i<playableCards.Count;i++){
			playableCards[i].isActive = true;
		}
	}

	public void setAllInactive(){
		for(int i = 0; i<cards.Count;i++){
			cards[i].GetComponent<CardScript>().isActive = false;
		}
	}

	public void putcardBack(){
		for (int i = 0; i<cards.Count; i++){
			cards[i].transform.position =cards[i].GetComponent<CardScript>().pos;
            cards[i].transform.eulerAngles = new Vector3(0, 0, cards[i].GetComponent<CardScript>().angle);
            cards[i].GetComponent<SpriteRenderer>().sortingOrder = cards[i].GetComponent<CardScript>().sortingOrder;
			cards[i].GetComponent<CardScript>().isReady = false;
		}
	}
}
