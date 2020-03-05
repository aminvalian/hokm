using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DeckCoppy : MonoBehaviour
{

    public GameObject cardDown;
    public GameObject othersCard;
    public GameObject cardDownBad;
    public GameObject othersCardBad;
    public GameObject hokmFade;


    public MainScript main;

    public int hokm;
    public int hakem = 0;
    public int turn;
    public int friendsHandWon;
    public int oppsHandsWon;
    public int friendsRoundsWon;
    public int oppsRoundsWon;
    public int handStarter;

    public bool handIsOver = false;

    public GameObject shapes;
    public List<GameObject> cards = new List<GameObject>();


    public GameObject opp;
    public GameObject opp2;
    public GameObject friend;
    public GameObject self;
    AI oppAI;
    AI opp2AI;
    AI friendAI;
    AI selfAI;
    public GameObject hokmSprite;
    public GameObject cardDownSmall;
    public GameObject cardDownSmallBad;
    public GameObject oppsRounds;
    public GameObject yourRounds;
    public List<Sprite> hokmSprites = new List<Sprite>();
    public List<GameObject> crowns = new List<GameObject>();
    public List<GameObject> inGameCards = new List<GameObject>();
    public List<CardScript> onBoardCards = new List<CardScript>();
    public List<List<int>> boresh = new List<List<int>>();
    public List<int> playedTimes = new List<int>();
    public List<List<CardScript>> outCards = new List<List<CardScript>>();
    public List<List<CardScript>> handHistory = new List<List<CardScript>>();
    public List<int> handHistoryStarters = new List<int>();
    public AudioSource clipCard;
    public AudioSource shuffle;

    // Use this for initialization
    void Start()
    {
        oppAI = opp.GetComponent<AI>();
        opp2AI = opp2.GetComponent<AI>();
        friendAI = friend.GetComponent<AI>();
        selfAI = self.GetComponent<AI>();

        List<int> sample = new List<int>();
        List<int> sample1 = new List<int>();
        List<int> sample2 = new List<int>();
        List<int> sample3 = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            sample.Add(0);
            sample1.Add(0);
            sample2.Add(0);
            sample3.Add(0);
        }
        boresh.Add(sample);
        boresh.Add(sample1);
        boresh.Add(sample2);
        boresh.Add(sample3);
        List<CardScript> cardSample = new List<CardScript>();
        List<CardScript> cardSample2 = new List<CardScript>();
        List<CardScript> cardSample3 = new List<CardScript>();
        List<CardScript> cardSample4 = new List<CardScript>();

        outCards.Add(cardSample);
        outCards.Add(cardSample2);
        outCards.Add(cardSample3);
        outCards.Add(cardSample4);
        main = GameObject.Find("Background").GetComponent<MainScript>();
        inGameCards = new List<GameObject>(cards);
        for (int i = 0; i < 52; i++)
        {
            cards[i].GetComponent<CardScript>().rank = cards[i].GetComponent<CardScript>().value;
        }
        StartCoroutine(chooseHakem());


    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator chooseHakem()
    {
        shuffle.Play();
        yield return new WaitForSeconds(1);
        List<GameObject> d = new List<GameObject>(cards);
        List<GameObject> g = new List<GameObject>();
        bool chosen = false;
        int h = 0;
        int count = 0;
        while (!chosen)
        {
            int i = Random.Range(0, 51 - count);
            Vector2 pos = new Vector2(0, 0);
            switch (h)
            {
                case 0:
                    pos = new Vector2(0, -4f);
                    break;
                case 1:
                    pos = new Vector2(2.5f, 0);
                    break;
                case 2:
                    pos = new Vector2(0, 4f);
                    break;
                case 3:
                    pos = new Vector2(-2.5f, 0);
                    break;
            }
            GameObject c = Instantiate(d[i], pos, Quaternion.identity) as GameObject;
            c.GetComponent<SpriteRenderer>().sortingOrder = count;
            c.GetComponent<AudioSource>().Play();
            g.Add(c);
            if (d[i].GetComponent<CardScript>().value == 14)
            {
                chosen = true;
                hakem = h;
                yield return new WaitForSeconds(1.5f);
                for (int k = 0; k < g.Count; k++)
                {
                    Destroy(g[k]);
                }
                crowns[h].SetActive(true);
                StartCoroutine(dealCardstoHakem());
            }
            else
            {
                d.RemoveAt(i);
                h++;
                count++;
                if (h > 3)
                    h = 0;
            }
            yield return new WaitForSeconds(0.2f);
        }

    }

    public IEnumerator dealCardstoHakem()
    {
        GameObject cardObject = null;
        int h = hakem;
        turn = h - 1;
        AI ai = null;
        
        for (int i = 0; i < 5; i++)
        {
            Vector2 pos = new Vector2(0, 0);
            switch (h)
            {
                case 0:
                    pos = new Vector2(0, -1000f);
                    ai = selfAI;
                    cardObject = cardDown;
                    break;
                case 1:
                    pos = new Vector2(1000f, 0);
                    ai = oppAI;
                    cardObject = cardDownBad;
                    break;
                case 2:
                    pos = new Vector2(0, 1000f);
                    ai = friendAI;
                    cardObject = cardDown;
                    break;
                case 3:
                    pos = new Vector2(-1000f, 0);
                    ai = opp2AI;
                    cardObject = cardDownBad;
                    break;
            }

            int k = Random.Range(0, 51 - i);
            GameObject c = Instantiate(cardObject, gameObject.transform.position, Quaternion.identity) as GameObject;
            c.GetComponent<Rigidbody2D>().AddForce(pos);
            ai.addCard(inGameCards[k]);
            inGameCards.RemoveAt(k);
            yield return new WaitForSeconds(0.3f);
        }

        
            StartCoroutine(ai.GetComponent<AI>().chooseHokm());

        
    }
    public IEnumerator dealTheRest(int h)
    {
        GameObject cardObject = null;
        hokmSprite.GetComponent<SpriteRenderer>().sprite = hokmSprites[hokm];
        GameObject g = Instantiate(hokmFade, hokmSprite.transform.position, Quaternion.identity) as GameObject;
        g.GetComponent<SpriteRenderer>().sprite = hokmSprites[hokm];
        AI ai = null;
        h++;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                h %= 4;
                Vector2 pos = new Vector2(0, 0);
                switch (h)
                {
                    case 0:
                        pos = new Vector2(0, -5000f);
                        ai = selfAI;
                        cardObject = cardDown;
                        break;
                    case 1:
                        pos = new Vector2(5000f, 0);
                        ai = oppAI;
                        cardObject = cardDownBad;
                        break;
                    case 2:
                        pos = new Vector2(0, 5000f);
                        ai = friendAI;
                        cardObject = cardDown;
                        break;
                    case 3:
                        pos = new Vector2(-5000f, 0);
                        ai = opp2AI;
                        cardObject = cardDownBad;
                        break;
                }

                int k = Random.Range(0, inGameCards.Count - 1);
                Debug.Log(k);

                GameObject c = Instantiate(cardObject, gameObject.transform.position, Quaternion.identity) as GameObject;
                c.GetComponent<Rigidbody2D>().AddForce(pos);
                ai.addCard(inGameCards[k]);
                inGameCards.RemoveAt(k);
                yield return new WaitForSeconds(0.05f);

            }
            h++;
            yield return new WaitForSeconds(0.2f);

        }
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                h %= 4;
                Vector2 pos = new Vector2(0, 0);
                switch (h)
                {
                    case 0:
                        pos = new Vector2(0, -5000f);
                        ai = selfAI;
                        cardObject = cardDown;
                        break;
                    case 1:
                        pos = new Vector2(5000f, 0);
                        ai = oppAI;
                        cardObject = cardDownBad;
                        break;
                    case 2:
                        pos = new Vector2(0, 5000f);
                        ai = friendAI;
                        cardObject = cardDown;
                        break;
                    case 3:
                        pos = new Vector2(-5000f, 0);
                        ai = opp2AI;
                        cardObject = cardDownBad;
                        break;
                }

                int k = Random.Range(0, inGameCards.Count - 1);
                GameObject c = Instantiate(cardDown, gameObject.transform.position, Quaternion.identity) as GameObject;
                c.GetComponent<Rigidbody2D>().AddForce(pos);
                ai.addCard(inGameCards[k]);
                inGameCards.RemoveAt(k);
                yield return new WaitForSeconds(0.05f);
            }
            h++;
        }
        oppAI.sort();
        opp2AI.sort();
        friendAI.sort();
        selfAI.sort();
        handStarter = hakem % 4;
        moveNext();
    }

    public void moveNext()
    {
        AI ai = null;
        AI oppAI = opp.GetComponent<AI>();
        AI opp2AI = opp2.GetComponent<AI>();
        AI friendAI = friend.GetComponent<AI>();
        if (onBoardCards.Count == 4)
        {
            int winner = findWinner();

            if (winner % 2 == 1)
            {
                oppsHandsWon++;
                if (oppsHandsWon == 7)
                {
                    handIsOver = true;
                    if (friendsHandWon == 0)
                    {
                        if (hakem % 2 == 0)
                        {
                            oppsRoundsWon += 3;
                        }
                        else
                        {
                            oppsRoundsWon += 2;
                        }
                    }
                    else
                    {
                        oppsRoundsWon++;
                    }
                    oppsRounds.GetComponent<Text>().text = oppsRoundsWon.ToString();
                    if (hakem % 2 == 0)
                    {
                        hakem++;
                        if (hakem > 3)
                            hakem -= 4;
                    }
                    StartCoroutine(startNewRound());
                }
            }
            else
            {

                friendsHandWon++;
                if (friendsHandWon == 7)
                {
                    handIsOver = true;
                    if (oppsHandsWon == 0)
                    {
                        if (hakem % 2 == 1)
                        {
                            friendsRoundsWon += 3;
                        }
                        else
                            friendsRoundsWon += 2;
                    }
                    else
                    {
                        friendsRoundsWon++;
                    }
                    yourRounds.GetComponent<Text>().text = friendsRoundsWon.ToString();
                    if (hakem % 2 == 1)
                    {
                        hakem++;
                        if (hakem > 3)
                            hakem -= 4;
                    }
                    StartCoroutine(startNewRound());
                }
            }
            if (!handIsOver)
            {
                turn = winner - 1;
                StartCoroutine(clearHand(winner));
            }
        }
        else
        {
            turn++;
            turn %= 4;
            if (onBoardCards.Count == 0)
                handStarter = turn;

            switch (turn)
            {
                case 0:
                    ai = selfAI;
                    break;
                case 1:
                    ai = oppAI;
                    break;
                case 2:
                    ai = friendAI;
                    break;
                case 3:
                    ai = opp2AI;
                    break;
            }
            int k = -1;
            if (onBoardCards.Count != 0)
                k = onBoardCards[0].kind;
            ai.chooseACard(onBoardCards.Count, k);
            

        }
    }

    public int findWinner()
    {
        int value = 0;
        int winner = -1;
        int playedKind = -1;
        for (int i = 0; i < 4; i++)
        {
            if (onBoardCards[i].kind == hokm)
            {


                if (onBoardCards[i].value > value)
                {
                    value = onBoardCards[i].value;
                    winner = i + handStarter;
                }
            }
        }
        if (winner == -1)
        {
            playedKind = onBoardCards[0].kind;
            for (int i = 0; i < 4; i++)
            {
                if (onBoardCards[i].kind == playedKind && onBoardCards[i].value > value)
                {
                    value = onBoardCards[i].value;
                    winner = i + handStarter;
                }
            }
        }
        winner %= 4;
        handHistoryStarters.Add(winner);
        Debug.Log("winner " + winner);
        return winner;
    }


    public IEnumerator startNewRound()
    {
        hokmSprite.GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(1f);
        shuffle.Play();
        yield return new WaitForSeconds(1f);
        handIsOver = false;
        for (int i = 0; i < 52; i++)
        {
            cards[i].GetComponent<CardScript>().rank = cards[i].GetComponent<CardScript>().value;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                boresh[i][j] = 0;
            }
            playedTimes[i] = 0;
            outCards[i].Clear();
            crowns[i].SetActive(false);

        }
        List<GameObject> g = new List<GameObject>(GameObject.FindGameObjectsWithTag("cardDownSmall"));
        for (int i = 0; i < g.Count; i++)
        {
            Destroy(g[i]);
        }
        List<GameObject> a = new List<GameObject>(GameObject.FindGameObjectsWithTag("cardDownSmall"));
        for (int i = 0; i < a.Count; i++)
        {
            Destroy(a[i]);
        }

        List<GameObject> b = new List<GameObject>(GameObject.FindGameObjectsWithTag("card"));
        Debug.Log(b.Count);
        for (int i = 0; i < b.Count; i++)
        {


            Destroy(b[i]);
        }
        inGameCards = new List<GameObject>(cards);
        onBoardCards.Clear();
        crowns[hakem % 4].SetActive(true);
        friendsHandWon = 0;
        oppsHandsWon = 0;
        oppAI.clearHands();
        opp2AI.clearHands();
        friendAI.clearHands();
        selfAI.clearHands();
        handHistory = new List<List<CardScript>>();
        handHistoryStarters = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            playedTimes[i] = 0;
        }
        yield return new WaitForSeconds(1f);

        if (friendsRoundsWon == 7)
        {
            // you Won!
            //New Game???
            oppsHandsWon = 0;
            oppsHandsWon = 0;
            oppsRoundsWon = 0;
            friendsRoundsWon = 0;
            yourRounds.GetComponent<Text>().text = friendsRoundsWon.ToString();
            oppsRounds.GetComponent<Text>().text = oppsRoundsWon.ToString();
            StartCoroutine(chooseHakem());
        }
        else if (oppsRoundsWon == 7)
        {
            //you lost
            //New Game???
            oppsHandsWon = 0;
            oppsHandsWon = 0;
            oppsRoundsWon = 0;
            friendsRoundsWon = 0;
            yourRounds.GetComponent<Text>().text = friendsRoundsWon.ToString();
            oppsRounds.GetComponent<Text>().text = oppsRoundsWon.ToString();
            StartCoroutine(chooseHakem());
        }
        else
        {
            StartCoroutine(dealCardstoHakem());
        }
    }
    public IEnumerator clearHand(int winner)
    {
        List<CardScript> ca = new List<CardScript>();
        for (int i = 0; i < 4; i++)
        {
            ca.Add(onBoardCards[i]);
        }
        handHistory.Add(ca);
        Debug.Log(handHistory[handHistory.Count - 1][0].name + " " + handHistory[handHistory.Count - 1][1].name + " " + handHistory[handHistory.Count - 1][2].name + " " + handHistory[handHistory.Count - 1][3].name);
        AI[] AIs = { friendAI, opp2AI, oppAI ,selfAI};
        for (int z = 0; z < 3; z++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (AIs[z].kHand[onBoardCards[j].kind].Count > 0)
                {
                    for (int k = 0; k < AIs[z].kHand[onBoardCards[j].kind].Count; k++)
                    {
                        if (AIs[z].kHand[onBoardCards[j].kind][k].rank < onBoardCards[j].rank)
                        {
                            AIs[z].kHand[onBoardCards[j].kind][k].rank++;
                        }
                    }
                }

            }
        }
        
        playedTimes[onBoardCards[0].kind]++;
        if (onBoardCards[0].kind == hokm)
        {
            for (int i = 1; i < 4; i++)
            {
                if (onBoardCards[i].kind != hokm)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        boresh[(i + handStarter) % 4][j] = -1;
                    }
                }
            }
        }
        else
        {
            if (onBoardCards[0].rank > 9 && onBoardCards[0].rank != 14)
            {
                if (boresh[handStarter][onBoardCards[0].kind] == 0)
                {
                    boresh[handStarter][onBoardCards[0].kind] = 1;
                    Debug.Log(handStarter + " " + boresh[handStarter][onBoardCards[0].kind]);
                }
            }
            for (int i = 1; i < 4; i++)
            {
                if (onBoardCards[i].kind == hokm)
                {

                    if (boresh[(i + handStarter) % 4][onBoardCards[0].kind] < onBoardCards[i].value)
                    {
                        boresh[(i + handStarter) % 4][onBoardCards[0].kind] = onBoardCards[i].value;
                        Debug.Log(handStarter + " v" + boresh[(i + handStarter) % 4][onBoardCards[0].kind] + " k" + onBoardCards[0].kind);

                    }
                }
                if (onBoardCards[i].kind != onBoardCards[0].kind)
                {
                    if (boresh[(i + handStarter) % 4][onBoardCards[0].kind] == 0)
                    {
                        boresh[(i + handStarter) % 4][onBoardCards[0].kind] = 1;
                        Debug.Log(handStarter + " " + boresh[(i + handStarter) % 4][onBoardCards[0].kind]);
                    }
                }

                else
                {
                    for (int j = 0; j < i; j++)
                        if (onBoardCards[i].kind == onBoardCards[j].kind && onBoardCards[i].rank < onBoardCards[j].rank && onBoardCards[i].rank + onBoardCards[j].rank >= 25)
                        {
                            if (boresh[(i + handStarter) % 4][onBoardCards[0].kind] == 0)
                            {
                                boresh[(i + handStarter) % 4][onBoardCards[0].kind] = 1;
                                Debug.Log(i + handStarter + " " + boresh[(i + handStarter) % 4][onBoardCards[0].kind]);
                            }
                        }
                }
            }
        }
        Debug.Log(boresh[0][0] + " " + boresh[0][1] + " " + boresh[0][2] + " " + boresh[0][3]);
        Debug.Log(boresh[1][0] + " " + boresh[1][1] + " " + boresh[1][2] + " " + boresh[1][3]);
        Debug.Log(boresh[2][0] + " " + boresh[2][1] + " " + boresh[2][2] + " " + boresh[2][3]);
        Debug.Log(boresh[3][0] + " " + boresh[3][1] + " " + boresh[3][2] + " " + boresh[3][3]);

        Vector2 cardPos = Vector2.zero;

        if (winner % 2 == 0)
        {
            cardPos = new Vector2(0.4f * friendsHandWon, 6.4f + ((friendsHandWon % 2) * 0.4f));
        }
        else
        {
            cardPos = new Vector2(3f - ((oppsHandsWon % 2) * 0.4f), -1.7f + (oppsHandsWon * 0.4f));
        }
        int b = 0;
        clipCard.Play();
        while (b < 10)
        {
            b = 0;
            for (int i = 0; i < 4; i++)
            {
                float x = cardPos.x - onBoardCards[i].transform.position.x;
                float y = cardPos.y - onBoardCards[i].transform.position.y;
                if (Mathf.Abs(x) + Mathf.Abs(y) < 0.1)
                {
                    x = 0;
                    y = 0;
                    b += i + 1;
                }
                onBoardCards[i].transform.localScale = new Vector3(onBoardCards[i].transform.localScale.x - 0.05f, onBoardCards[i].transform.localScale.y - 0.05f, onBoardCards[i].transform.localScale.z);
                onBoardCards[i].GetComponent<Rigidbody2D>().velocity = new Vector2(x * 8f, y * 8f);
                yield return new WaitForSeconds(0.005f);
            }

        }
        if (winner % 2 == 0)
        {
            GameObject g = Instantiate(cardDownSmall, cardPos, Quaternion.AngleAxis(90, Vector3.forward)) as GameObject;
            g.GetComponent<SpriteRenderer>().sortingOrder = friendsHandWon;
        }
        else
        {
            GameObject g = Instantiate(cardDownSmallBad, cardPos, Quaternion.identity) as GameObject;
            g.GetComponent<SpriteRenderer>().sortingOrder = oppsHandsWon;
        }

        for (int i = 0; i < 4; i++)
        {
            onBoardCards[0].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            onBoardCards[0].transform.localScale = new Vector3(1, 1, 1);
            outCards[onBoardCards[0].kind].Add(onBoardCards[0]);
            onBoardCards.RemoveAt(0);
        }
        for (int k = 0; k < 4; k++)
        {
            for (int j = 0; j < outCards[k].Count - 1; j++)
            {
                for (int z = j + 1; z < outCards[k].Count; z++)
                {
                    if (outCards[k][j].value > outCards[k][z].value)
                    {
                        CardScript c = outCards[k][j];
                        outCards[k][j] = outCards[k][z];
                        outCards[k][z] = c;
                    }
                }
            }
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < outCards[i].Count; j++)
            {
                outCards[i][j].gameObject.transform.position = new Vector2(16 + i * 5, 10 - j * 2);
                outCards[i][j].GetComponent<SpriteRenderer>().sortingOrder = j;
                outCards[i][j].gameObject.transform.localRotation = Quaternion.AngleAxis(0f, Vector3.zero);
            }
        }
        moveNext();
    }


}