using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {
	public GameObject Otherscard;
    public GameObject cardDownBig;


	public DeckCoppy deck;
	public int position;
    
	public List<GameObject> othersCards = new List<GameObject>();

	// Use this for initialization
	void Start () {
		deck = GameObject.Find("cardDeck").GetComponent<DeckCoppy>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clearHands()
    {

        othersCards.Clear();
        othersCards = new List<GameObject>();
    }
    

	public IEnumerator playCard(CardScript card){
        deck.onBoardCards.Add(card);
        Destroy(othersCards[0].gameObject);
        othersCards.RemoveAt(0);
        
        GameObject g = Instantiate(cardDownBig)as GameObject;
        g.GetComponent<CardDownBigScript>().card = card.gameObject;
        g.GetComponent<CardDownBigScript>().pos = position;
        card.GetComponent<SpriteRenderer>().sortingOrder = deck.onBoardCards.Count+60;
		
		
		yield return new WaitForSeconds(2f);
		
        deck.moveNext();


	}
}
