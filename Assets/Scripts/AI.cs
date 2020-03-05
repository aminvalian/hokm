using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {
	public GameObject Otherscard;
    public GameObject cardDownBig;

	public List<GameObject> hand = new List<GameObject>();

	public List<List<CardScript>> kHand = new List<List<CardScript>>();
	public DeckScript deck;
	public int position;
    public bool goingToKot = false;
	List<CardScript> spade = new List<CardScript>();
	List<CardScript> hearth = new List<CardScript>();
	List<CardScript> club = new List<CardScript>();
	List<CardScript> diamond = new List<CardScript>();
	public List<GameObject> othersCards = new List<GameObject>();

	// Use this for initialization
	void Start () {
		deck = GameObject.Find("cardDeck").GetComponent<DeckScript>();
		kHand.Add(spade);
		kHand.Add(hearth);
		kHand.Add(club);
		kHand.Add(diamond);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clearHands(){
		spade.Clear();
		hearth.Clear();
		diamond.Clear();
		club.Clear();
		hand.Clear();
		for(int i = 0; i<4; i++){
			kHand[i].Clear();
		}
        othersCards.Clear();
        othersCards = new List<GameObject>();
	}

	public IEnumerator chooseHokm(){
		List<List<CardScript>> kinds = new List<List<CardScript>>();
		kinds.Add(spade);
		kinds.Add(hearth);
		kinds.Add(club);
		kinds.Add(diamond);
		for (int i = 0;i<kinds.Count;i++){
			if(kinds[i].Count > 2){
				deck.hokm = kinds[i][0].kind;

				kinds.Clear();
			}
			else if(kinds[i].Count < 2 ){
				kinds.RemoveAt(i);
				i--;
			}
		}

		if(kinds.Count == 1){
			deck.hokm = kinds[0][0].kind;
		}else if(kinds.Count == 2){
			int p = kinds[0][0].value + kinds[0][1].value;
			int q = kinds[1][0].value + kinds[1][1].value;
			if(Mathf.Abs(p-q)>7){
				if(p>q){
					deck.hokm = kinds[0][0].kind;
				}else {
					deck.hokm = kinds[1][0].kind;
					
				}
			}else{
				int a = 0;
				if (kinds[0][0].value > kinds[0][1].value){
					a = kinds[0][0].value;
				}else{
					a = kinds[0][1].value;
				}
				if (a>kinds[1][0].value && a > kinds[1][1].value){
					deck.hokm = kinds[0][0].kind;
				}else{
					deck.hokm = kinds[1][0].kind;

				}
			}
		}
		yield return new WaitForSeconds(1f);
		StartCoroutine(deck.dealTheRest(deck.hakem));
	}

	public void sort(){
		for(int k = 0 ; k<spade.Count-1; k++){ 
			for(int z = k+1; z<spade.Count ; z++){
				if (spade[k].value>spade[z].value){
					CardScript c = spade[k];
					spade[k] = spade[z];
					spade[z] = c;
				}
			}
		}
		for(int k = 0 ; k<club.Count-1; k++){ 
			for(int z = k+1; z<club.Count ; z++){
				if (club[k].value>club[z].value){
					CardScript c = club[k];
					club[k] = club[z];
					club[z] = c;
				}
			}
		}
		for(int k = 0 ; k<diamond.Count-1; k++){ 
			for(int z = k+1; z<diamond.Count ; z++){
				if (diamond[k].value>diamond[z].value){
					CardScript c = diamond[k];
					diamond[k] = diamond[z];
					diamond[z] = c;
				}
			}
		}
		for(int k = 0 ; k<hearth.Count-1; k++){ 
			for(int z = k+1; z<hearth.Count ; z++){
				if (hearth[k].value>hearth[z].value){
					CardScript c = hearth[k];
					hearth[k] = hearth[z];
					hearth[z] = c;
				}
			}
		}

		int n = 0;
		for (int i = 0; i < kHand.Count; i++){
			for( int j = 0; j<kHand[i].Count; j++){
				n++;
                if (position == 2)
                {
                    //friend
                    GameObject v = Instantiate(kHand[i][j].gameObject, new Vector2(-2.8f + ((n - 1) * 0.6f), 10.8f), Quaternion.Euler(new Vector3(0, 0,0))) as GameObject;
                    v.GetComponent<SpriteRenderer>().sortingOrder = n;
                    kHand[i][j] = v.GetComponent<CardScript>();

                }
                else if (position == 3)
                {
                    //opp2

                    GameObject v = Instantiate(kHand[i][j].gameObject, new Vector2(-10, -2.8f + ((n - 1) * 0.6f)), Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
                    v.GetComponent<SpriteRenderer>().sortingOrder = 14-n;
                    kHand[i][j] = v.GetComponent<CardScript>();
                }

                else if (position == 0)
                {
                    //opp2

                    GameObject v = Instantiate(kHand[i][j].gameObject, new Vector2(-2.8f + ((n - 1) * 0.6f), -10.8f), Quaternion.identity) as GameObject;
                    v.GetComponent<SpriteRenderer>().sortingOrder = n;
                    kHand[i][j] = v.GetComponent<CardScript>();
                }
                else
                {
                    //opp

                    GameObject v = Instantiate(kHand[i][j].gameObject, new Vector2(10, -2.8f + ((n - 1) * 0.6f)), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
                    v.GetComponent<SpriteRenderer>().sortingOrder = n;
                    kHand[i][j] = v.GetComponent<CardScript>();

                }
			}

		}
	}

	public void addCard(GameObject c){
		hand.Add(c);
		CardScript card = c.GetComponent<CardScript>();
		switch (card.kind){
		case 0:
			spade.Add(card);
			break;
		case 1:
			hearth.Add(card);
			break;
		case 2:
			club.Add(card);
			break;
		case 3:
			diamond.Add(card);
			break;
		}

		if(position == 2){
			//friend
			GameObject g = Instantiate(Otherscard,new Vector2(-2.8f+((hand.Count-1)*0.4f),7.3f),Quaternion.identity)as GameObject;
			g.GetComponent<SpriteRenderer>().sortingOrder = hand.Count;
			othersCards.Add(g);

		}else if(position == 3){
			//opp2
			GameObject g = Instantiate(Otherscard,new Vector2(-5,-1.8f+((hand.Count-1)*0.4f)),Quaternion.Euler(new Vector3(0,0,270)))as GameObject;
			g.GetComponent<SpriteRenderer>().sortingOrder = hand.Count;
			othersCards.Add(g);

		}
        else if (position == 0)
        {
            //opp2
            GameObject g = Instantiate(Otherscard, new Vector2(-2.8f + ((hand.Count - 1) * 0.4f), -5.8f), Quaternion.identity) as GameObject;
            g.GetComponent<SpriteRenderer>().sortingOrder = hand.Count;
            othersCards.Add(g);

        }
        else {
			//opp
			GameObject g = Instantiate(Otherscard,new Vector2(5,-1.8f+((hand.Count-1)*0.4f)),Quaternion.Euler(new Vector3(0,0,90)))as GameObject;
			g.GetComponent<SpriteRenderer>().sortingOrder = hand.Count;
			othersCards.Add(g);

		}

	}

	public void chooseACard (int pos, int kind){
		bool found = false;
		//Debug.Log(kHand[1].Count+" "+kHand[2].Count+" "+kHand[3].Count+" "+kHand[0].Count);
		if (pos == 3){
			int winner = -1;
			int value = 0;
			bool hokmInvolved = false;
			for (int i= 0; i<3; i++){
				if (deck.onBoardCards[i].kind == deck.hokm){
					hokmInvolved = true;
				}
			}
			if (hokmInvolved){
				for (int i= 0; i<3; i++){
					if (deck.onBoardCards[i].kind == deck.hokm && deck.onBoardCards[i].rank>value){
						value = deck.onBoardCards[i].rank;
						winner = i;
					}
				}
			}else{
				for (int i= 0; i<3; i++){
					if (deck.onBoardCards[i].kind == deck.onBoardCards[0].kind && deck.onBoardCards[i].rank>value){
						value = deck.onBoardCards[i].rank;
						winner = i;
					}
				}
			}
		    if(kHand[kind].Count == 0){
				if(kHand[deck.hokm].Count>0 && winner != 1){
				// if boride or opp has it
					int index = -1;
					for(int i = 0; i < kHand[deck.hokm].Count;i++){
						if (value<kHand[deck.hokm][i].rank  && kHand[deck.hokm][i].rank !=14){
							index = i;
							i = kHand[deck.hokm].Count;
						}
					}
                    if (!hokmInvolved)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][0]));
                        Debug.Log(pos + " kind" + kind);
                    }
                    else if (index != -1)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][index]));
                        Debug.Log(pos + " kind" + kind);
                    }
                    else
                    {

                        if (kHand[deck.hokm][kHand[deck.hokm].Count - 1].rank == 14)
                        {
                            found = true;
                            StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                            Debug.Log(pos + " kind" + kind);

                        }
                        else
                        {
                            int count = 13;
                            for (int i = 0; i < 4 && !found; i++)
                            {
                                if (kHand[i].Count > 0)
                                {
                                    if (kHand[i].Count < count && i != deck.hokm && kHand[i][0].rank != 14)
                                    {
                                        index = i;
                                        count = kHand[i].Count;
                                    }
                                }
                            }
                            if (index != -1 &&!found)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[index][0]));
                                Debug.Log(pos + " kind" + kind);

                            }
                            else
                            {
                                rad(pos, kind);
                                found = true;
                            }

                        }
                    }
				}else {
                    rad(pos, kind);
                    found = true;
                }


            }
            else if(kHand[kind].Count == 1 ){
                found = true;
                StartCoroutine( playCard(kHand[kind][0]));
				Debug.Log(pos+" kind"+kind);
			}
			else if(kHand[kind].Count > 1 ){
				int index = -1;
				for (int i = 0; i<kHand[kind].Count;i++){
					if (kHand[kind][i].kind == deck.onBoardCards[0].kind && kHand[kind][i].rank>value){
						index = i;
						i = kHand[kind].Count;
					}
				}
				if (winner !=1 && (!hokmInvolved ||deck.onBoardCards[0].kind == deck.hokm) && index>-1){
                    found = true;
                    StartCoroutine( playCard(kHand[kind][index]));
                    Debug.Log(pos + " kind" + kind + " winner" + winner);
                }
                else
                {
                    found = true;
                    StartCoroutine( playCard(kHand[kind][0]));
					Debug.Log(pos+" kind"+kind+ " winner"+ winner);
				}
			}

		}
		if (pos == 2){
			if(kHand[kind].Count == 0 ){
                if (kHand[deck.hokm].Count > 0) {
                    int powerTwo = deck.boresh[(position + 1) % 4][kind];
                    
                    // if boride or opp has it //must act
                    if (deck.onBoardCards[1].kind == deck.hokm || deck.onBoardCards[0].rank != 14  || powerTwo > 0)
                    {
                        if (deck.onBoardCards[1].kind == deck.hokm && powerTwo < deck.onBoardCards[1].value)
                        {
                            powerTwo = deck.onBoardCards[1].value;
                        }
                        for (int i = 0; i < kHand[deck.hokm].Count && !found; i++)
                        {
                            if (powerTwo < kHand[deck.hokm][i].value)
                            {
                                Debug.Log(pos + " kind" + kind);
                                found = true;
                                StartCoroutine(playCard(kHand[deck.hokm][i]));
                                i = kHand[deck.hokm].Count;
                            }
                        }


                        if (deck.onBoardCards[1].kind == deck.hokm && !found)
                        {
                            // play higher hokm
                            for (int i = 0; i < kHand[deck.hokm].Count; i++)
                            {
                                if (kHand[deck.hokm][i].value > deck.onBoardCards[1].value)
                                {
                                    Debug.Log(pos + " kind" + kind);
                                    found = true;
                                    StartCoroutine(playCard(kHand[deck.hokm][i]));
                                    i = kHand[deck.hokm].Count;
                                }
                            }
                        }
                        
                            if (!found)
                        {
                            List<int> availKinds = new List<int>();
                            for (int i = 0; i < 4; i++)
                            {
                                if (kHand[i].Count > 0)
                                    if (i != deck.hokm)
                                        availKinds.Add(i);
                            }
                            // find the lowest 
                            while (!found)
                            {
                                int r = Random.Range(0, availKinds.Count - 1);
                                int k = availKinds[r];
                                if (kHand[k][0].rank != 13 || kHand[k][0].rank != 14)
                                {
                                    found = true;
                                    StartCoroutine(playCard(kHand[k][0]));
                                    Debug.Log(pos + " kind" + kind);

                                }
                                else
                                {
                                    if (availKinds.Count > 0)
                                        availKinds.RemoveAt(r);
                                    else
                                    {
                                        for (int i = 0; i < 4 && !found; i++)
                                        {
                                            //find Random
                                            if (kHand[i].Count > 0)
                                                if (i != deck.hokm)
                                                {
                                                    availKinds.Add(i);
                                                    r = Random.Range(0, availKinds.Count - 1);
                                                    k = Random.Range(0, kHand[availKinds[r]].Count - 1);
                                                    found = true;
                                                    StartCoroutine(playCard(kHand[availKinds[r]][k]));
                                                    Debug.Log(pos + " kind" + kind);
                                                }
                                        }

                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        List<int> availKinds = new List<int>();
                        for (int i = 0; i < 4; i++)
                        {
                            if (kHand[i].Count > 0)
                                if (i != deck.hokm)
                                    availKinds.Add(i);
                        }
                        // find the lowest 
                        if (!found)
                        {
                            rad(pos, kind);
                            found = true;
                        }
                    }
                }

                else
                {
                    // send Ace hint
                    for (int i = 0; i < 4 && !found; i++)
                    {
                        if (kHand[i].Count > 0)
                        {
                            if ((kHand[i][kHand[i].Count - 1].rank == 14 || kHand[i][kHand[i].Count - 1].value == 13) && (kHand[i].Count > 1))
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[i][0]));
                            }
                        }
                    }
                    if (!found)
                    {
                        rad(pos, kind);
                        found = true;
                    }
                }
            }
			else if(kHand[kind].Count == 1 ){
				StartCoroutine( playCard(kHand[kind][0]));
				found = true;
				Debug.Log(pos+" kind"+kind);

				
			}else if(kHand[kind].Count > 1 ){
                if(deck.onBoardCards[0].kind != deck.hokm && deck.onBoardCards[1].kind == deck.hokm)
                {
                    Debug.Log(pos + " kind" + kind);
                    found = true;
                    StartCoroutine(playCard(kHand[kind][0]));
                }
                if (deck.boresh[(position + 1) % 4][kind] < 0 && !found)
                {
                    if (deck.onBoardCards[0].kind == deck.onBoardCards[1].kind && deck.onBoardCards[0].rank > deck.onBoardCards[1].rank)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][0]));
                    }else if (deck.onBoardCards[0].kind == deck.onBoardCards[1].kind && deck.onBoardCards[0].rank < deck.onBoardCards[1].rank)
                    {
                        for ( int i = 0; i < kHand[kind].Count &&!found; i++)
                        {
                            if (kHand[kind][i].rank > deck.onBoardCards[1].rank)
                            {
                                Debug.Log(pos + " kind" + kind);
                                found = true;
                                StartCoroutine(playCard(kHand[kind][i]));
                                i = kHand[kind].Count;
                            }
                        }
                    }
                }
                else if(kHand[kind][kHand[kind].Count-1].rank > deck.onBoardCards[1].rank)
                {
                    int isoRank = deck.onBoardCards[0].rank;

                    for (int i = 0; i < kHand[kind].Count; i++)
                    {
                        if (isoRank + 1 == kHand[kind][i].rank)
                        {
                            isoRank++;
                        }


                        Debug.Log("iso " + isoRank);

                    }
                    if (isoRank < kHand[kind][kHand[kind].Count - 1].rank && !found)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][kHand[kind].Count - 1]));
                    }
                    else if (!found)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][0]));
                    }
                }

                if (!found){
					// find the highest card


					if(deck.onBoardCards[0].kind == deck.onBoardCards[1].kind && deck.onBoardCards[0].value < deck.onBoardCards[1].value){
						int power = deck.onBoardCards[1].value;
						for (int i = 0; i<kHand[kind].Count&& !found; i++){
							if(power<kHand[kind][i].value){
                                Debug.Log(pos + " kind" + kind);
                                found = true;
                                StartCoroutine(playCard(kHand[kind][i]));
                            }
						}
						
					}else if((deck.onBoardCards[0].kind == deck.onBoardCards[1].kind && deck.onBoardCards[0].value > deck.onBoardCards[1].value)&&deck.onBoardCards[0].value<11 && kHand[kind][kHand[kind].Count - 1].value-1 > deck.onBoardCards[0].value)
                    {
						Debug.Log(pos+" kind"+kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][kHand[kind].Count-1]));
					}
					if (!found){
						Debug.Log(pos+" kind"+kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][0]));
					}
				}
			}
		}
		if (pos == 1){
			if(kHand[kind].Count == 0 ){

                if (deck.outCards[kind].Count > 11)
                {
                    for (int i = 0; i < 4; i++)
                        if (deck.boresh[(position + i) % 4][kind] == 0)
                            deck.boresh[(position + i) % 4][kind] = 1;
                }

                if (kHand[deck.hokm].Count > 0 && deck.boresh[(position + 2) % 4][kind] <= 0)
                {
                    //boresh

                    if (deck.boresh[(position + 1) % 4][kind] > deck.boresh[(position + 2) % 4][kind])
                    {
                        for (int j = 0; j < kHand[deck.hokm].Count && !found; j++)
                        {
                            if (kHand[deck.hokm][j].rank > 10)
                            {
                                if (kHand[deck.hokm][j].rank == 14 && deck.outCards[deck.hokm].Count > 8)
                                {
                                    found = true;
                                    StartCoroutine(playCard(kHand[deck.hokm][0]));
                                    Debug.Log(pos + " kind" + kind);
                                }
                                else
                                {
                                    if (deck.boresh[position][kind] < kHand[deck.hokm][j].value)
                                    {
                                        found = true;
                                        StartCoroutine(playCard(kHand[deck.hokm][j]));
                                        Debug.Log(pos + " kind" + kind);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        int w = 0;
                        if (position % 2 == 0)
                            w = deck.oppsRoundsWon;
                        else
                            w = deck.friendsRoundsWon;
                        float r = Random.Range(0f, 10f);
                        if (deck.onBoardCards[0].rank == 14 || r < 5 || w>4)
                        {
                            found = true;
                            StartCoroutine(playCard(kHand[deck.hokm][0]));
                            Debug.Log(pos + " kind" + kind);
                        }
                        else
                        {
                            rad(pos, kind);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                        Debug.Log(pos + " kind" + kind);
                    }


                }
                else
                {
                    rad(pos, kind);
                    found = true;
                }

            }
            else if(kHand[kind].Count == 1 ){
				StartCoroutine( playCard(kHand[kind][0]));
				Debug.Log(pos+" kind"+kind);

				
			}else if(kHand[kind].Count > 1 ){
				int v = 0;
				int index = 0;
				v = deck.onBoardCards[0].value;
                if (kHand[kind][kHand[kind].Count - 1].rank == 14 && (deck.boresh[(position + 1) % 4][kind] == 0 || deck.playedTimes[kind]<2))
                {
                    //AQ
                    float r = Random.Range(0f, 10f);
                    if (r < 5 && kHand[kind][kHand[kind].Count - 2].rank == 12 && deck.playedTimes[kind] < 2 && kHand[kind].Count < 6 && deck.onBoardCards[0].rank!=13)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][kHand[kind].Count - 2]));
                    }
                    else
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][kHand[kind].Count - 1]));
                    }
                }
                else if (kHand[kind][kHand[kind].Count - 1].rank == 13)
                {
                    if (kHand[kind][kHand[kind].Count - 2].rank == 12 && deck.onBoardCards[0].rank != 14)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][kHand[kind].Count - 1]));
                    }
                    else if (deck.onBoardCards[0].rank == 12)
                    {
                        //  Based on 50%
                        float r = Random.Range(0f, 10f);
                        if (r < 5)
                        {
                            Debug.Log(pos + " kind" + kind);
                            found = true;
                            StartCoroutine(playCard(kHand[kind][kHand[kind].Count - 1]));
                        }
                        else
                        {
                            Debug.Log(pos + " kind" + kind);
                            found = true;
                            StartCoroutine(playCard(kHand[kind][0]));
                        }
                    }
                }
				if (!found){
					for (int i = 0; i< kHand[kind].Count; i++){
						if (kHand[kind][i].rank > v){
							index = i;
							i = kHand[kind].Count;
						}
					}
                    if (index == 0)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][0]));

                    }
                    else if (kHand[kind][index].rank != 13)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][index]));
                    }
                    else
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[kind][0]));
                    }
				}
			}
		}else if (pos == 0){
            
            if(position%2 == 0)
            {
                if (deck.oppsHandsWon == 0)
                {
                    goingToKot = CheckForKot();
                }
            }
            else
            {
                if(deck.friendsHandWon == 0)
                {
                    goingToKot = CheckForKot();
                }
            }
            for (int i = 0; i < deck.handHistory.Count; i++)
            {
                if ((deck.handHistoryStarters[i] + 2) % 4 == position)
                {
                    int handsWon = 0;
                    if (position % 2 == 0)
                        handsWon = deck.oppsHandsWon;
                    else
                        handsWon = deck.friendsHandWon;
                    Debug.Log("count " + kHand[deck.hokm].Count + " kind " + deck.handHistory[i][0].kind + " hands " + handsWon);
                    if (kHand[deck.hokm].Count > 0 && deck.handHistory[i][0].kind == deck.hokm && handsWon == 0)
                    {
                        Debug.Log("going to kot");
                        i = deck.handHistory.Count;
                        goingToKot = true;
                        if (kHand[deck.hokm].Count > 0 && !found)
                        {
                            if (kHand[deck.hokm][kHand[deck.hokm].Count - 1].rank == 14)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                                Debug.Log(pos + " kind" + kind);
                            }
                            else
                            {
                                for (int j = 0; j < 4 && !found; j++)
                                {
                                    if (kHand[j].Count > 0)
                                    {
                                        if (kHand[j][kHand[j].Count - 1].rank == 14 && deck.playedTimes[j] <3)
                                        {
                                            found = true;
                                            StartCoroutine(playCard(kHand[j][kHand[j].Count - 1]));
                                            Debug.Log(pos + " kind" + kind);
                                        }
                                    }
                                }
                            }
                            if (!found)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[deck.hokm][0]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 4 && !found; j++)
                            {
                                if (kHand[j].Count > 0)
                                {
                                    if (kHand[j][kHand[j].Count - 1].rank == 14 && deck.playedTimes[j] < 3)
                                    {
                                        found = true;
                                        StartCoroutine(playCard(kHand[j][kHand[j].Count - 1]));
                                        Debug.Log(pos + " kind" + kind);
                                    }
                                }
                            }
                        }
                        if (!found)
                        {
                            List<int> availKinds = new List<int>();
                            for (int j = 0; j < 4; i++)
                            {
                                if (kHand[j].Count > 0)
                                    availKinds.Add(j);
                            }
                            if (availKinds.Count > 0 && !found)
                            {
                                int r = Random.Range(0, availKinds.Count);
                                found = true;
                                StartCoroutine(playCard(kHand[availKinds[r]][0]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                    }
                }

            }
            if (goingToKot)
            {
                if(kHand[deck.hokm].Count>0)
                    if(kHand[deck.hokm][kHand[deck.hokm].Count - 1].rank == 14 && !found)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                        Debug.Log(pos + " kind" + kind);
                    }
                if (!found)
                {
                    for(int i = 0; i<4 && !found; i++)
                    {
                        if (kHand[i].Count > 0)
                        {
                            if (kHand[i][kHand[i].Count - 1].rank == 14)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[i][kHand[i].Count - 1]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                    }
                    if (!found)
                    {
                        for (int i = 0; i < deck.handHistory.Count && !found; i++)
                        {
                            if (deck.handHistoryStarters[i] == (position + 2) % 4 && deck.handHistory[i][0].kind != deck.hokm && kHand[deck.handHistory[i][0].kind].Count>0)
                                if (deck.handHistory[i][2].kind != deck.hokm)
                                {
                                    found = true;
                                    StartCoroutine(playCard(kHand[deck.handHistory[i][0].kind][0]));
                                    Debug.Log(pos + " kind" + kind);
                                }
                        }
                        if (!found)
                        {
                            for (int i = 0; i < deck.handHistory.Count && !found; i++)
                            {
                                if (deck.handHistoryStarters[i] == position && deck.handHistory[i][0].kind == deck.hokm)
                                    if (deck.handHistory[i][2].kind != deck.hokm)
                                    {
                                        found = true;
                                        StartCoroutine(playCard(kHand[deck.handHistory[i][0].kind][0]));
                                        Debug.Log(pos + " kind" + kind);
                                    }
                            }
                        }
                        
                        if (!found && kHand[deck.hokm].Count>0)
                        {
                            found = true;
                            StartCoroutine(playCard(kHand[deck.hokm][0]));
                            Debug.Log(pos + " kind" + kind);
                        }
                        if (!found)
                        {
                            for (int i = 0; i < deck.handHistory.Count && !found; i++)
                            {
                                if ((deck.handHistoryStarters[i] + 2) % 4 == position && deck.handHistory[i][0].rank!=14)
                                {
                                    if (kHand[deck.handHistory[i][0].kind].Count > 0)
                                    {
                                        Debug.Log(pos + " kind" + kind);
                                        found = true;
                                        StartCoroutine(playCard(kHand[deck.handHistory[i][0].kind][0]));
                                    }
                                }

                            }
                            List<int> availKinds = new List<int>();
                            for (int i = 0; i < 4 ; i++)
                            {
                                if (kHand[i].Count > 0)
                                    if (i != deck.hokm)
                                        availKinds.Add(i);
                            }
                            if (availKinds.Count > 0 && !found)
                            {
                                int r = Random.Range(0, availKinds.Count);
                                found = true;
                                StartCoroutine(playCard(kHand[availKinds[r]][0]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i<deck.handHistoryStarters.Count && i < 7 && !found;i++)
            {
                int w = 0;
                if (position % 2 == 1)
                {
                    w = deck.friendsHandWon;
                }
                else
                    w = deck.oppsHandsWon;
                if (deck.handHistoryStarters[i] == (position + 2) % 4 && w == 0 && kHand[deck.hokm].Count>0 && deck.handHistory[i][0].kind == deck.hokm)
                {
                    for(int j = 0; j <4 && !found; j++)
                    {
                        if(kHand[j][kHand[j].Count-1].value == 14){
                            found = true;
                            StartCoroutine(playCard(kHand[j][kHand[j].Count - 1]));
                            Debug.Log(pos + " kind" + kind);
                        }
                    }
                    if (!found && kHand[deck.hokm].Count > 0)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][0]));
                        Debug.Log(pos + " kind" + kind);
                    }
                }
            }
            int grantedHandsCount = 0;
            if( position%2 == 0)
            {
                grantedHandsCount = deck.friendsHandWon;
            }
            else
            {
                grantedHandsCount = deck.oppsHandsWon;
            }
            grantedHandsCount += CountTopHokms();

            if (grantedHandsCount > 6 && !found)
            {
                found = true;
                StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                Debug.Log(pos + " kind" + kind);
            }
            
            //if High card is safe to play 
            for (int i = 0; i < 4 && !found; i++)
            {
                if (kHand[i].Count > 0)
                {
                    if (kHand[i][kHand[i].Count - 1].rank == 14)
                    {
                        if (i == deck.hokm)
                        {
                            int h = 0;
                            for (int j = 0; j< 4; j++)
                            {
                                h += deck.boresh[(position + 1) % 4][i];
                                h += deck.boresh[(position + 3) % 4][i];
                            }
                            if (h > 0 || deck.handHistoryStarters.Count>8)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[i][kHand[i].Count - 1]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                        else
                        {

                            if (deck.boresh[(position + 3) % 4][i] + deck.boresh[(position + 1) % 4][i] == 0 || (deck.boresh[(position + 1) % 4][i] == 0 && deck.boresh[(position + 2) % 4][i] > deck.boresh[(position + 3) % 4][i]))
                            {
                                //A2
                                if (kHand[i].Count > 1 && kHand[i].Count < 4)
                                {
                                    float r = Random.Range(0f, 10f);
                                    if (r < 3 && kHand[i][kHand[i].Count - 2].rank != 13 && deck.playedTimes[i] < 1)
                                    {
                                        found = true;
                                        StartCoroutine(playCard(kHand[i][0]));
                                        Debug.Log(pos + " kind" + kind);

                                    }
                                    else
                                    {
                                        found = true;
                                        StartCoroutine(playCard(kHand[i][kHand[i].Count - 1]));
                                        Debug.Log(pos + " kind" + kind);

                                    }
                                }
                                else
                                {
                                    found = true;
                                    StartCoroutine(playCard(kHand[i][kHand[i].Count - 1]));
                                    Debug.Log(pos + " kind" + kind);
                                }
                            }
                        }
                    }
                }
            
			}

            

            // if friend can boresh the card
            for (int i = 0; i < 4 && !found; i++)
            {
                if (kHand[i].Count > 0)
                    if (deck.boresh[(position + 3) % 4][i] < 2)
                    {
                        if (/*(deck.boresh[(position + 1) % 4][i] <= deck.boresh[(position + 2) % 4][i]) &&*/ deck.boresh[(position + 2) % 4][i]!=0)
                        {
                            if (kHand[i][kHand[i].Count - 1].rank == 14)
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[i][kHand[i].Count - 1]));
                                Debug.Log(pos + " kind" + kind);
                            }
                            else
                            {
                                found = true;
                                StartCoroutine(playCard(kHand[i][0]));
                                Debug.Log(pos + " kind" + kind);
                            }
                        }
                    }
            }
            for (int i = 0; i < deck.handHistory.Count && !found; i++)
            {
                if ((deck.handHistoryStarters[i] + 2) % 4 == position && kHand[deck.handHistory[i][0].kind].Count > 0)
                    if (kHand[deck.handHistory[i][0].kind][kHand[deck.handHistory[i][0].kind].Count - 1].rank != 14)
                    {
                        Debug.Log("handHistory" + deck.handHistory.Count + " handHisstoryStarter" + deck.handHistoryStarters + " i" + i + " handHistoryicount" + deck.handHistory[i].Count);
                        if (kHand[deck.handHistory[i][0].kind].Count > 0)
                        {
                            Debug.Log(pos + " kind" + kind);
                            found = true;
                            StartCoroutine(playCard(kHand[deck.handHistory[i][0].kind][0]));
                        }
                    }

            }
            for (int i = 0; i<kHand.Count && !found; i++){
				if (kHand[i].Count == 1 && i!=deck.hokm && deck.boresh[(position + 1) % 4][i] + deck.boresh[(position + 3) % 4][i] == 0){
                    found = true;
                    StartCoroutine(playCard(kHand[i][0]));
					Debug.Log(pos+" kind"+kind);		
				} 
			}
            for (int i = 0; i < kHand.Count && !found; i++)
            {
                if (kHand[i].Count == 2 && i != deck.hokm)
                {
                    if (kHand[i][1].rank != 14)
                    {
                        found = true;
                        StartCoroutine(playCard(kHand[i][0]));
                        Debug.Log(pos + " kind" + kind);
                    }
                    
                }
            }

            for (int i = 0; i<kHand.Count && !found; i++){
				if(kHand[i].Count > 1)
					if (kHand[i][kHand[i].Count-1].rank == 13 && i != deck.hokm){
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[i][0]));
					}
			}
            

            //try finind a card tht opps dont boresh
            if (!found){
				List<int> availKinds = new List<int>();
				for (int i = 0; i<4; i++){
					if(kHand[i].Count > 0 && i!= deck.hokm)
						availKinds.Add(i);
				}
				int r = Random.Range(0,availKinds.Count-1);
                for (int j = 0; j < availKinds.Count && !found; j++)
                {
                    r %= availKinds.Count;
                    int k = availKinds[r];
                    if (deck.boresh[(position + 1) % 4][k] + deck.boresh[(position + 3) % 4][k] == 0)
                    {
                        
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[k][0]));
                    }
                    else
                    {
                        r++;
                    }
                }
                if (kHand[deck.hokm].Count > 0 && !found)
                {
                    if(kHand[deck.hokm][kHand[deck.hokm].Count-1].rank == 14)
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][kHand[deck.hokm].Count - 1]));
                    }
                    else
                    {
                        Debug.Log(pos + " kind" + kind);
                        found = true;
                        StartCoroutine(playCard(kHand[deck.hokm][0]));
                    }
                }
                //play a random
                if (!found)
                {
                    r %= availKinds.Count;
                    Debug.Log(pos + " kind" + kind);
                    found = true;
                    StartCoroutine(playCard(kHand[availKinds[r]][0]));
                }

			}
		}
	}

    public int CountTopHokms()
    {
        int h = 0;
        for (int i = 0; i < kHand[deck.hokm].Count; i++)
        {
            if (kHand[deck.hokm][kHand[deck.hokm].Count - i - 1].rank == 14 - i)
            {
                h++;
            }
            else
            {
                i = kHand[deck.hokm].Count;
            }
        }
        return h;
    }
	
    public bool CheckForKot()
    {
        int h = 0;
        int hs = 0;
        int gaped = 0;
        int o = 0;
        for(int i = 0; i< kHand[deck.hokm].Count && gaped <2; i++)
        {
            if (kHand[deck.hokm][i].rank + i+gaped == 14)
            {
                hs++;
            }
            else
            {
                gaped++;
                hs++;
            }
        }
        hs--;
        if (kHand[deck.hokm].Count > 0)
        {
            if (kHand[deck.hokm][kHand[deck.hokm].Count - 1].rank == 14)
            {
                h++;
                if (kHand[deck.hokm].Count > 1)
                    if (kHand[deck.hokm][kHand[deck.hokm].Count - 2].rank == 13)
                    {
                        h++;
                        if (kHand[deck.hokm].Count > 2)
                            if (kHand[deck.hokm][kHand[deck.hokm].Count - 3].rank == 12)
                            {
                                h++;
                                if (kHand[deck.hokm].Count > 3)
                                    if (kHand[deck.hokm][kHand[deck.hokm].Count - 4].rank == 11)
                                    {
                                        h++;
                                        if (kHand[deck.hokm].Count > 4)
                                            if (kHand[deck.hokm][kHand[deck.hokm].Count - 5].rank == 10)
                                            {
                                                h++;
                                                if (kHand[deck.hokm].Count > 5)
                                                    if (kHand[deck.hokm][kHand[deck.hokm].Count - 6].rank == 9)
                                                    {
                                                        h++;
                                                        if (kHand[deck.hokm].Count > 6)
                                                            if (kHand[deck.hokm][kHand[deck.hokm].Count - 7].rank == 8)
                                                            {
                                                                h++;
                                                            }
                                                    }
                                            }
                                    }
                            }                   
                    }
            }
        }
        if (hs > h)
            h = hs;
        if (h >= 6)
            return true;
        for (int i = 0; i < 4; i++)
        {
            if (i != deck.hokm && deck.boresh[(position + 1) % 4][i] + deck.boresh[(position + 3) % 4][i] == 0)
            {
                if (kHand[i].Count > 0)
                    if (kHand[i][kHand[i].Count - 1].rank == 14)
                    {
                        h++;
                        if (kHand[i].Count > 1)
                            if (kHand[i][kHand[i].Count - 2].rank == 13)
                            {
                                h++;
                                if (kHand[i].Count > 2 && (h >3 || (deck.outCards[deck.hokm].Count+kHand[deck.hokm].Count == 13)))
                                    if (kHand[i][kHand[i].Count - 3].rank == 12)
                                    {
                                        h++;
                                    }

                            }
                    }
            }
             
        }
        int w = 0;
        if (position % 2 == 0)
            w = deck.friendsHandWon;
        else
            w = deck.oppsHandsWon;
        if (o + h+w >= 6)
            return true;
        else
            return false;
    }

    private void rad(int pos, int kind)
    {
        bool found = false;
        bool tried = false;
        bool b = true;
        int v = 14;
        List<int> availKinds = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            if (kHand[i].Count > 0)
                if (i != deck.hokm)
                    availKinds.Add(i);
        }
        if(availKinds.Count == 1)
        {
            found = true;
            b = false;
            StartCoroutine(playCard(kHand[availKinds[0]][0]));
            Debug.Log(pos + " kind" + kind);
        }
        else if(availKinds.Count == 0)
        {
            found = true;
            b = false;
            StartCoroutine(playCard(kHand[deck.hokm][0]));
            Debug.Log(pos + " kind" + kind);
        }
        // find the lowest 
        while (b)
        {
            int r = Random.Range(0, availKinds.Count);
            Debug.Log(r + " " + availKinds.Count + " " + tried);
            if (availKinds.Count > 0) { 
                int k = availKinds[r];
                if (kHand[k][0].rank != v-1 && kHand[k][0].rank != v)
                {
                    found = true;
                    b = false;
                    StartCoroutine(playCard(kHand[k][0]));
                    Debug.Log(pos + " kind" + kind);

                }
                else
                {
                    if (availKinds.Count > 1)
                    {
                        availKinds.RemoveAt(r);
                    }
                    else
                    {
                        availKinds.Clear();
                        if (tried)
                        {
                            b = false;
                        }
                        else
                        {
                            v = 0;
                            tried = true;
                            for (int i = 0; i < 4; i++)
                            {
                                if (kHand[i].Count > 0)
                                    if (i != deck.hokm)
                                        availKinds.Add(i);
                            }
                        }
                    }
                }
            }
        }
        if (!found)
        {
            found = true;
            StartCoroutine(playCard(kHand[deck.hokm][0]));
            Debug.Log(pos + " kind" + kind);
        }

        
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
		for(int i = 0; i<kHand[card.kind].Count; i++){
			if ( card.value == kHand[card.kind][i].value){
				kHand[card.kind].RemoveAt(i);
			}
			
		}
        deck.moveNext();


	}

    public void ShowHand()
    {
        for (int i = 0; i < deck.onBoardCards.Count; i++)
        {
            Destroy(deck.onBoardCards[i].gameObject);
        }
            for (int i = 0; i < othersCards.Count; i++)
        {
            Destroy(othersCards[i].gameObject); 
        }
        int m = 0;
        if(position == 1)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < kHand[i].Count; j++)
                {
                    
                    kHand[i][j].transform.position = new Vector2(5.5f, -1.5f + m*0.6f);
                    kHand[i][j].GetComponent<SpriteRenderer>().sortingOrder = m+10;
                    kHand[i][j].GetComponent<ScreenResolutionPlacement>().Adjust();
                    m++;
                }
            }
        }
        else if(position == 2)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < kHand[i].Count; j++)
                {
                    kHand[i][j].transform.position = new Vector2(-0.2f - m * 0.6f,7.8f);
                    kHand[i][j].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                    kHand[i][j].GetComponent<ScreenResolutionPlacement>().Adjust();
                    kHand[i][j].GetComponent<SpriteRenderer>().sortingOrder = m+10;
                    m++;
                }
            }
        }
        else
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < kHand[i].Count; j++)
                {
                    kHand[i][j].transform.position = new Vector2(-5.5f, 0.9f - m * 0.6f);
                    kHand[i][j].GetComponent<SpriteRenderer>().sortingOrder = m + 10;
                    kHand[i][j].GetComponent<ScreenResolutionPlacement>().Adjust();
                    m++;
                }
            }
        }
    }
}
