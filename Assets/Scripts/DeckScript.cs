using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using ArabicSupport;

public class DeckScript : MonoBehaviour
{

    public GameObject cardDown;
    public GameObject othersCard;
    public GameObject cardDownBad;
    public GameObject othersCardBad;
    public GameObject hokmFade;
    public GameObject yesRestart;
    public GameObject no;
    public GameObject blackScr;
    public GameObject winMessage;
    public GameObject loseMessage;
    public GameObject extraMessage;
    public GameObject coinSmall;
    public GameObject watchBtn;
    public GameObject watchExtraBtn;
    public GameObject coinStack;
    public GameObject extraCoinStack;
    public GameObject winTextObj;
    public GameObject recover;
    public GameObject wallet;

    public Button watchExtra;
    public Button noThanksBtn;
    public Button watch;

    public MainScript main;
    public PlayerHand pHand;
    public WinMessageScript winMsg;
    public LoseMessageScript loseMsg;

    public int hokm;
    public int hakem = 0;
    public int turn;
    public int betAmount;
    public int totalLose;
    public int totalWin;
    public int winAmount;
    public int extraWin = 0;
    public int friendsHandWon;
    public int oppsHandsWon;
    public int friendsRoundsWon;
    public int oppsRoundsWon;
    public int handStarter;

    public bool handIsOver;

    public GameObject shapes;
    public Sprite transparentSprite;
    public List<GameObject> cards = new List<GameObject>();

    public Text betText;
    public Text winText;
    public Text money;
    public Text extraMoney;
    public Text extraRecover;
    public Text inputWinText;
    public Text errorText;
    public Text overBetText;

    public GameObject opp;
    public GameObject opp2;
    public GameObject friend;
    AI oppAI;
    AI opp2AI;
    AI friendAI;
    public GameObject hokmSprite;
    public GameObject cardDownSmall;
    public GameObject cardDownSmallBad;
    public GameObject oppsRounds;
    public GameObject yourRounds;
    public InputField betInput;
    public GameObject betInputObject;
    public GameObject continueBtn;
    public List<Sprite> hokmSprites = new List<Sprite>();
    public List<GameObject> crowns = new List<GameObject>();
    public List<GameObject> pCards = new List<GameObject>();
    public List<GameObject> inGameCards = new List<GameObject>();
    public List<CardScript> onBoardCards = new List<CardScript>();
    public List<List<int>> boresh = new List<List<int>>();
    public List<int> playedTimes = new List<int>();
    public List<List<CardScript>> outCards = new List<List<CardScript>>();
    public List<List<CardScript>> handHistory = new List<List<CardScript>>();
    public List<int> handHistoryStarters = new List<int>();
    public AudioSource clipCard;
    public AudioSource shuffle;
    public AudioSource click;
    public AudioSource betSound;
    public AudioSource watchSound;
    public AudioSource bonus;
    public Font yekta;
    public Font myriad;
    // Use this for initialization
    void Start()
    {
        handIsOver = true;
        if (PlayerPrefs.GetInt("oppsRounds", 0) < 7 && PlayerPrefs.GetInt("friendsRounds", 0) < 7)
        {
            oppsRoundsWon = PlayerPrefs.GetInt("oppsRounds", 0);
            friendsRoundsWon = PlayerPrefs.GetInt("friendsRounds", 0);
            totalLose = PlayerPrefs.GetInt("totalLose", 0);
            totalWin = PlayerPrefs.GetInt("totalWin", 0);
            hakem = PlayerPrefs.GetInt("hakem", -1);
            string s = oppsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                oppsRounds.GetComponent<Text>().font = yekta;
            }
            oppsRounds.GetComponent<Text>().text = s;
            s = friendsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                yourRounds.GetComponent<Text>().font = yekta;
            }
            yourRounds.GetComponent<Text>().text = s;
        }
        else
        {
            PlayerPrefs.SetInt("hakem", -1);
            hakem = -1;
            PlayerPrefs.SetInt("totalLose", 0);
            PlayerPrefs.SetInt("totalWin", 0);
            PlayerPrefs.SetInt("friendsRounds", 0);
            PlayerPrefs.SetInt("oppsRounds", 0);
            friendsRoundsWon = 0;
            oppsRoundsWon = 0;
            string s = oppsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                oppsRounds.GetComponent<Text>().font = yekta;
            }
            oppsRounds.GetComponent<Text>().text = s;
            s = friendsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                yourRounds.GetComponent<Text>().font = yekta;
            }
            yourRounds.GetComponent<Text>().text = s;
        }


        oppAI = opp.GetComponent<AI>();
        opp2AI = opp2.GetComponent<AI>();
        friendAI = friend.GetComponent<AI>();

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
        pHand = GameObject.Find("Background").GetComponent<PlayerHand>();
        inGameCards = new List<GameObject>(cards);
        for (int i = 0; i < 52; i++)
        {
            cards[i].GetComponent<CardScript>().rank = cards[i].GetComponent<CardScript>().value;
        }
        if (hakem >= 0)
        {
            continueBtn.SetActive(true);
            crowns[hakem].SetActive(true);
        }

        else
        {
            for (int i = 0; i < 4; i++)
            {
                crowns[i].SetActive(false);
            }
            friendsRoundsWon = 0;
            oppsRoundsWon = 0;
            string s = oppsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                oppsRounds.GetComponent<Text>().font = yekta;
            }
            oppsRounds.GetComponent<Text>().text = s;
            s = friendsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                yourRounds.GetComponent<Text>().font = yekta;
            }
            yourRounds.GetComponent<Text>().text = s;
            StartCoroutine(chooseHakem());
        }
    


    }

    public IEnumerator chooseHakem()
    {
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
                betInputObject.SetActive(true);
                CheckAd();
                string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
                if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                {
                    s = ArabicFixer.Fix(s);
                    money.font = yekta;
                }
                money.text = s;
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

    public void WatchClicked()
    {
        click.Play();

        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }



    }
    private void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            StartCoroutine(addMoney("100"));
            BetValueChanged();
            CheckAd();
        }
    }

    public IEnumerator addMoney(string moneyStr)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject g = Instantiate(coinSmall, watchBtn.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
            g.GetComponent<RectTransform>().localPosition = watchBtn.GetComponent<RectTransform>().position;
            g.transform.SetParent(GameObject.Find("Canvas").transform);
            g.GetComponent<CoinNavigatorScript>().des = coinStack;
            yield return new WaitForSeconds(0.1f);
        }

        int startMoney = int.Parse(PlayerPrefs.GetString("coin", "100"));
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) + 100).ToString());
        int jump = (int)(int.Parse(moneyStr) / 30f);
        int endMoney = startMoney + int.Parse(moneyStr);
        watchSound.Play();
        while (startMoney < endMoney)
        {
            string st = AdjustedNumber((startMoney + jump).ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                st = ArabicFixer.Fix(st);
                money.font = yekta;
            }

            money.text = AdjustedNumber((startMoney + jump).ToString());
            startMoney += jump;
            yield return new WaitForSeconds(0.01f);
        }
        string s = AdjustedNumber(endMoney.ToString());
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            money.font = yekta;
        }
        
        money.text = s;

    }
    public IEnumerator TakeMoney(string moneyStr)
    {
        int startMoney = int.Parse(PlayerPrefs.GetString("coin", "100"));
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) - int.Parse(moneyStr)).ToString());
        int jump = (int)(int.Parse(moneyStr) / 30f);
        int endMoney = startMoney - int.Parse(moneyStr);
        int betMoney = 0;
        int winMoney = 0;
        int WinJump = (int)(winAmount / 30f);
        for (int i = 0; i < 30; i++)
        {
            if (i % 6 == 5)
            {
                GameObject g = Instantiate(coinSmall, coinStack.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
                g.GetComponent<RectTransform>().localPosition = coinStack.GetComponent<RectTransform>().position;
                g.GetComponent<CoinNavigatorScript>().des = winTextObj;
                g.transform.SetParent(GameObject.Find("Canvas").transform);
            }
            string st = AdjustedNumber((startMoney - jump).ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                st = ArabicFixer.Fix(st);
                money.font = yekta;
            }
            money.text = st;
            startMoney -= jump;
            yield return new WaitForSeconds(0.02f);

        }
        string s = AdjustedNumber(endMoney.ToString());
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            money.font = yekta;
        }
        money.text = s;


        while (betMoney < betAmount)
        {
            string st = AdjustedNumber(winMoney.ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                st = ArabicFixer.Fix(st);
                winText.font = yekta;
            }
            winText.text = s;
            s = AdjustedNumber(betMoney.ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                st = ArabicFixer.Fix(st);
                betText.font = yekta;
            }
            betText.text = s;
            betMoney += jump;
            winMoney += WinJump;
            yield return new WaitForSeconds(0.01f);
        }
        betSound.Play();
        string str = AdjustedNumber(winAmount.ToString());
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            str = ArabicFixer.Fix(str);
            winText.font = yekta;
        }
        winText.text = str;
        str = AdjustedNumber(betAmount.ToString());
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            str = ArabicFixer.Fix(str);
            betText.font = yekta;
        }
        betText.text = str;
        StartCoroutine(dealCardstoHakem());
        betInputObject.SetActive(false);

    }

    public void BetValueChanged()
    {
        if (betInput.GetComponent<InputField>().text == "")
        {
            inputWinText.text = "";
            errorText.text = "";
            overBetText.text = "";
        }
        if (betInput.GetComponent<InputField>().text == "-")
        {
            betInput.GetComponent<InputField>().text = "";
        }
        if (betInput.GetComponent<InputField>().text != "" && betInput.GetComponent<InputField>().text != "-")
        {
            if (int.Parse(betInput.GetComponent<InputField>().text) >= 0)
            {
                if (int.Parse(betInput.GetComponent<InputField>().text) > int.Parse(PlayerPrefs.GetString("coin", "100")))
                {
                    string s = PlayerPrefs.GetString("coin", "100");
                    if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                    {
                        s = ArabicFixer.Fix(s);
                        overBetText.font = yekta;
                    }
                    betInput.GetComponent<InputField>().text = PlayerPrefs.GetString("coin", "100");
                    overBetText.text = s;
                }
                else
                {
                    errorText.text = "";
                    string s = betInput.GetComponent<InputField>().text;
                    if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                    {
                        s = ArabicFixer.Fix(s);
                        overBetText.font = yekta;
                    }
                    overBetText.text = s;
                    if (int.Parse(betInput.GetComponent<InputField>().text) > 50000)
                    {
                        s = "50000";
                        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                        {
                            s = "۵۰۰۰۰";

                        }
                        betInput.GetComponent<InputField>().text = "50000";
                        overBetText.text = s;
                    }
                    if (hakem == 0)
                    {
                        s = AdjustedNumber(((int)(int.Parse(betInput.GetComponent<InputField>().text) * 1.5000001f)).ToString());
                        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                        {
                            s = ArabicFixer.Fix(s);
                            inputWinText.font = yekta;
                        }
                        inputWinText.text = s;
                    }
                    else if (hakem == 2)
                    {
                        s = AdjustedNumber(((int)(int.Parse(betInput.GetComponent<InputField>().text) * 1.8000001f)).ToString());
                        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                        {
                            s = ArabicFixer.Fix(s);
                            inputWinText.font = yekta;
                        }
                        inputWinText.text = s;
                    }
                    else
                    {
                        s = AdjustedNumber(((int)(int.Parse(betInput.GetComponent<InputField>().text) * 2.3000001f)).ToString());
                        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                        {
                            s = ArabicFixer.Fix(s);
                            inputWinText.font = yekta;
                        }
                        inputWinText.text = s;
                    }
                }
                
            }
        }
    }

    public void betClicked()
    {
        click.Play();
        if (betInput.GetComponent<InputField>().text != "" && betInput.GetComponent<InputField>().text != "-")
        {

            betAmount = Mathf.Abs(int.Parse(betInput.GetComponent<InputField>().text));
            if (betAmount <= 50000 && betAmount >= 30)
            {
                if (int.Parse(PlayerPrefs.GetString("coin", "100")) >= betAmount)
                {

                    if (hakem == 0)
                    {

                        winAmount = (int)(betAmount * 1.5000001f);
                    }
                    else if (hakem == 2)
                    {
                        winAmount = (int)(betAmount * 1.8000001f);
                    }
                    else
                    {
                        winAmount = (int)(betAmount * 2.3000001f);
                    }
                    handIsOver = false;
                    StartCoroutine(TakeMoney(betAmount.ToString()));
                }
                
            }
            else
            {
                string s = "Bet Range is 30 to 50,000";
                if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                {
                    s = "۵۰,۰۰۰ ﺮﺜﮐﺍﺪﺣ ﻭ ۳۰ ﻁﺮﺷ ﻞﻗﺍﺪﺣ";
                    errorText.font = yekta;
                }
                errorText.text = s;
            }
        }
        else
        {
            string s = "Invalid Bet";
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                s = "ﺖﺳﺭﺩﺎﻧ ﻁﺮﺷ";
                errorText.font = yekta;
            }
            errorText.text = s;
        }
    }

    public IEnumerator dealCardstoHakem()
    {
        yield return new WaitForSeconds(0.5f);
        shuffle.Play();
        yield return new WaitForSeconds(1f);
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
                    ai = null;
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
            if (h == 0)
                pCards.Add(inGameCards[k]);
            else
                ai.addCard(inGameCards[k]);
            inGameCards.RemoveAt(k);
            yield return new WaitForSeconds(0.3f);
        }

        if (h == 0)
        {
            pHand.sortHand(pCards);
            shapes.SetActive(true);
        }
        else if (h == 1)
        {
            StartCoroutine(opp.GetComponent<AI>().chooseHokm());
        }
        else if (h == 2)
        {
            StartCoroutine(friend.GetComponent<AI>().chooseHokm());

        }
        else if (h == 3)
        {
            StartCoroutine(opp2.GetComponent<AI>().chooseHokm());

        }
    }
    public IEnumerator dealTheRest(int h)
    {
        GameObject cardObject = null;
        hokmSprite.GetComponent<Image>().sprite = hokmSprites[(4 * PlayerPrefs.GetInt("usingdeck", 0)) + hokm];
        GameObject g = Instantiate(hokmFade, hokmSprite.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
        g.transform.SetParent(GameObject.Find("Canvas").transform);
        g.GetComponent<RectTransform>().position = hokmSprite.GetComponent<RectTransform>().position;
        g.GetComponent<Image>().sprite = hokmSprites[(4 * PlayerPrefs.GetInt("usingdeck", 0)) + hokm];
        AI ai = null;
        h++;
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                if (h > 3)
                    h = 0;
                Vector2 pos = new Vector2(0, 0);
                switch (h)
                {
                    case 0:
                        pos = new Vector2(0, -5000f);
                        ai = null;
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
                GameObject c = Instantiate(cardObject, gameObject.transform.position, Quaternion.identity) as GameObject;
                c.GetComponent<Rigidbody2D>().AddForce(pos);
                if (h == 0)
                    pCards.Add(inGameCards[k]);
                else
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
                if (h > 3)
                    h = 0;
                Vector2 pos = new Vector2(0, 0);
                switch (h)
                {
                    case 0:
                        pos = new Vector2(0, -5000f);
                        ai = null;
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
                GameObject c = Instantiate(cardObject, gameObject.transform.position, Quaternion.identity) as GameObject;
                c.GetComponent<Rigidbody2D>().AddForce(pos);
                if (h == 0)
                    pCards.Add(inGameCards[k]);
                else
                    ai.addCard(inGameCards[k]);
                inGameCards.RemoveAt(k);
                yield return new WaitForSeconds(0.05f);
            }
            h++;
        }
        oppAI.sort();
        opp2AI.sort();
        friendAI.sort();
        pHand.sortHand(pCards);
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
                if (oppsHandsWon >= 7)
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
                    if (oppsRoundsWon > 7)
                    {
                        oppsRoundsWon = 7;
                    }
                    PlayerPrefs.SetInt("friendsRounds", friendsRoundsWon);
                    PlayerPrefs.SetInt("oppsRounds", oppsRoundsWon);
                    string s = oppsRoundsWon.ToString();
                    if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                    {
                        s = ArabicFixer.Fix(s);
                        oppsRounds.GetComponent<Text>().font = yekta;
                    }
                    oppsRounds.GetComponent<Text>().text = s;
                    StartCoroutine(roundsTextVisual(oppsRounds.GetComponent<Text>()));
                    if (hakem % 2 == 0)
                    {
                        hakem++;
                        if (hakem > 3)
                            hakem -= 4;
                    }
                    loseMessage.SetActive(true);
                    opp2AI.ShowHand();
                    oppAI.ShowHand();
                    friendAI.ShowHand();
                    totalLose += betAmount;
                    betInput.text = "0";
                    BetValueChanged();
                }
            }
            else
            {

                friendsHandWon++;

                if (friendsHandWon >= 7)
                {
                    handIsOver = true;
                    //

                    if (oppsHandsWon == 0)
                    {
                        if (hakem % 2 == 1)
                        {
                            friendsRoundsWon += 3;
                        }
                        else
                        {
                            friendsRoundsWon += 2;
                        }
                    }
                    else
                    {
                        friendsRoundsWon++;
                        

                    }
                    if (friendsRoundsWon > 7)
                    {
                        friendsRoundsWon = 7;
                    }
                    PlayerPrefs.SetInt("friendsRounds", friendsRoundsWon);
                    PlayerPrefs.SetInt("oppsRounds", oppsRoundsWon);
                    string s = friendsRoundsWon.ToString();
                    if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                    {
                        s = ArabicFixer.Fix(s);
                        yourRounds.GetComponent<Text>().font = yekta;
                    }
                    yourRounds.GetComponent<Text>().text = s;
                    StartCoroutine(roundsTextVisual(yourRounds.GetComponent<Text>()));
                    if (hakem % 2 == 1)
                    {
                        hakem++;
                        if (hakem > 3)
                            hakem -= 4;
                    }
                    PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) + winAmount).ToString());
                    totalWin += winAmount;
                    winMessage.SetActive(true);
                    opp2AI.ShowHand();
                    oppAI.ShowHand();
                    friendAI.ShowHand();
                    betInput.text = "0";
                    BetValueChanged();
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
            if (turn == 0)
            {
                pHand.sortPlayableCards(k);
            }
            else
            {
                ai.chooseACard(onBoardCards.Count, k);
            }

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
        handHistoryStarters.Add(handStarter);
        Debug.Log("winner " + winner);
        return winner;
    }


    public IEnumerator startNewRound()
    {
        winMessage.SetActive(false);
        loseMessage.SetActive(false);
        betAmount = 0;
        string s ="0";
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            winText.font = yekta;
        }
        winText.text = s;
        if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            betText.font = yekta;
        }
        betText.text = s;
        errorText.text = "";
        hokmSprite.GetComponent<Image>().sprite = transparentSprite;

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
        pCards.Clear();
        crowns[hakem % 4].SetActive(true);
        friendsHandWon = 0;
        oppsHandsWon = 0;
        oppAI.clearHands();
        opp2AI.clearHands();
        friendAI.clearHands();
        handHistory = new List<List<CardScript>>();
        handHistoryStarters = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            playedTimes[i] = 0;
        }
        yield return new WaitForSeconds(1f);
        if (friendsRoundsWon >= 7 || oppsRoundsWon >= 7)
        {
            totalLose = 0;
            totalWin = 0;
            oppsHandsWon = 0;
            oppsHandsWon = 0;
            oppsRoundsWon = 0;
            friendsRoundsWon = 0;
            hakem = -1;
            yourRounds.GetComponent<Text>().text = friendsRoundsWon.ToString();
            oppsRounds.GetComponent<Text>().text = oppsRoundsWon.ToString();
            StartCoroutine(chooseHakem());
            s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                money.font = yekta;
            }

            money.text = s;
        }
        else
        {
            betInputObject.SetActive(true);
            s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
            if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                money.font = yekta;
            }

            money.text = s;
        }
        CheckAd();
        extraWin = 0;

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
        AI[] AIs = { friendAI, opp2AI, oppAI };
        for (int z = 0; z < 3; z++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (AIs[z].kHand[onBoardCards[j].kind].Count > 0)
                {
                    for (int k = 0; k < AIs[z].kHand[onBoardCards[j].kind].Count; k++)
                    {
                        if (AIs[z].kHand[onBoardCards[j].kind][k].value < onBoardCards[j].value)
                        {
                            AIs[z].kHand[onBoardCards[j].kind][k].rank++;
                        }
                    }
                }

            }
        }
        for (int j = 0; j < 4; j++)
        {
            if (pHand.cards.Count > 0)
            {
                for (int k = 0; k < pHand.cards.Count; k++)
                {
                    if (pHand.cards[k].GetComponent<CardScript>().value < onBoardCards[j].value && pHand.cards[k].GetComponent<CardScript>().kind == onBoardCards[j].kind)
                    {
                        pHand.cards[k].GetComponent<CardScript>().rank++;
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
            cardPos = new Vector2(0.4f * friendsHandWon - 0.5f, 5f + ((friendsHandWon % 2) * 0.4f));
        }
        else
        {
            cardPos = new Vector2(3f - ((oppsHandsWon % 2) * 0.4f), 0.5f - (oppsHandsWon * 0.4f));
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

    public string AdjustedNumber(string number)
    {

        string num = null;
        int n = int.Parse(number);
        int r = n % 1000;

        for (int i = number.Length / 3; i > 0; i--)
        {
            r = n % (int)Mathf.Pow(10, 3 * i);
            int p = (int)(n / Mathf.Pow(10, 3 * i));
            if (p > 0 || i != number.Length / 3)
            {
                num += p.ToString() + ",";
            }

            n = r;
        }
        if (num != null)
        {
            for (int i = r.ToString().Length; i < 3; i++)
            {
                if (PlayerPrefs.GetInt("usinglang" , 0) == 1)
                {
                    num += "۰";
                }else
                    num += "0";
            }
        }
        num += r.ToString();
        return num;
    }


    public void OkStart()
    {

        StartCoroutine(startNewRound());
    }
    

    public void NoThanks()
    {
        click.Play();
        extraMessage.SetActive(false);
        StartCoroutine(startNewRound());
    }
    public void WatchForExtra()
    {
        click.Play();

        /*if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResultForExtra };
            Advertisement.Show("rewardedVideo", options);
            watchExtra.GetComponent<Button>().interactable = false;
            noThanksBtn.GetComponent<Button>().interactable = false;
        }*/
    }
    /*private void HandleShowResultForExtra(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            StartCoroutine(AddExtra(((int)(totalLose*0.2000001)).ToString()));

        }
    }*/
    public IEnumerator AddExtra(string moneyStr)
    {
        extraMoney.text = PlayerPrefs.GetString("coin", "100");
        wallet.SetActive(true);
        recover.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            GameObject g = Instantiate(coinSmall, watchExtraBtn.transform.position, Quaternion.identity) as GameObject;
            g.GetComponent<CoinNavigatorScript>().des = extraCoinStack;
            yield return new WaitForSeconds(0.1f);
        }

        int startMoney = int.Parse(PlayerPrefs.GetString("coin", "100"));
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) + int.Parse(moneyStr)).ToString());
        int jump = (int)(int.Parse(moneyStr) / 30f);
        int endMoney = startMoney + int.Parse(moneyStr);
        while (startMoney < endMoney)
        {
            extraMoney.text = AdjustedNumber((startMoney + jump).ToString());
            startMoney += jump;
            yield return new WaitForSeconds(0.01f);
        }
        bonus.Play();
        extraMoney.text = AdjustedNumber(endMoney.ToString());
        yield return new WaitForSeconds(1f);
        wallet.SetActive(false);
        recover.SetActive(true);
        extraMessage.SetActive(false);
        StartCoroutine(startNewRound());

    }
    public IEnumerator roundsTextVisual(Text text)
    {
        for (int i  = 27; i < 91; i++)
        {
            text.fontSize++;
            yield return new WaitForSeconds(0.02f);
        }
        for(int i = 90; i>=27; i--)
        {
            text.fontSize--;
            yield return new WaitForSeconds(0.02f);

        }
        text.fontSize = 27;
    }
    void OnApplicationQuit()
    {
        if (!handIsOver)
        {
            PlayerPrefs.SetInt("totalLose", totalLose + betAmount);
            oppsRoundsWon++;
            Debug.Log(handIsOver);
            if (hakem % 2 == 0)
            {
                hakem++;
            }
        }
        else
        {
            PlayerPrefs.SetInt("totalLose", totalLose);
        }
        PlayerPrefs.SetInt("friendsRounds", friendsRoundsWon);
        PlayerPrefs.SetInt("oppsRounds", oppsRoundsWon);
        PlayerPrefs.SetInt("totalWin", totalWin);  
        PlayerPrefs.SetInt("hakem", hakem);
    }

    public void CheckAd()
    {
       /* if (!Advertisement.IsReady("rewardedVideo"))
        {
            watch.GetComponent<Button>().interactable = false;
            watchExtra.GetComponent<Button>().interactable = false;
        }
        else
        {
            watch.GetComponent<Button>().interactable = true;
            watchExtra.GetComponent<Button>().interactable = true;
        }*/
    }

    public void ShowHands()
    {

    }
        
    

}