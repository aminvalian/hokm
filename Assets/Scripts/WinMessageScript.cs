using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ArabicSupport;

public class WinMessageScript : MonoBehaviour {

    public Text winAmount;
    public Text winmessage;
    public Text yourRounds;
    public Text oppsRounds;
    public Text moneyLost;
    public Text moneyEarned;
    
    public GameObject coin;
    public DeckScript deck;
    public GameObject okBtn;

    

    int win;
    float startCounter;
    int money;
    float counterJump;
    int endMoney;
    public AudioSource winSound;
	// Use this for initialization
    void OnEnable()
    {
        if (deck.friendsRoundsWon>=7)
        {
            yourRounds.gameObject.SetActive(true);
            oppsRounds.gameObject.SetActive(true);
            moneyEarned.gameObject.SetActive(true);
            moneyLost.gameObject.SetActive(true);
            winmessage.text = "You Won!";
            winmessage.font = deck.myriad;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                winmessage.text = "!ﺪﻳﺩﺮﺑ ﺎﻤﺷ";
                winmessage.font = deck.yekta;
            }
            string s = "Your Rounds: " + deck.friendsRoundsWon.ToString();
            yourRounds.font = deck.myriad;
            yourRounds.alignment = TextAnchor.MiddleLeft;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.friendsRoundsWon.ToString())+ " :ﺎﻤﺷ ﻩﺩﺮﺑ ﯼﺎﻫ ﺖﺳﺩ";
                yourRounds.font = deck.yekta;
                yourRounds.alignment = TextAnchor.MiddleRight;

            }
            yourRounds.text = s;
            oppsRounds.font = deck.myriad;
            oppsRounds.alignment = TextAnchor.MiddleLeft;
            s = "Opps Rounds: " + deck.oppsRoundsWon.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.oppsRoundsWon.ToString()) + " :ﻒﻳﺮﺣ ﻩﺩﺮﺑ ﯼﺎﻫ ﺖﺳﺩ";
                oppsRounds.font = deck.yekta;
                oppsRounds.alignment = TextAnchor.MiddleRight;

            }

            oppsRounds.text = s;
            moneyLost.font = deck.myriad;
            moneyLost.alignment = TextAnchor.MiddleLeft;
            s = "Total lose: " + deck.totalLose.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.AdjustedNumber(deck.totalLose.ToString())) + " :ﻩﺩﺍﺩ ﺖﺳﺩ ﺯﺍ ﻪﮑﺳ ﻉﻮﻤﺠﻣ";
                moneyLost.font = deck.yekta;
                moneyLost.alignment = TextAnchor.MiddleRight;

            }

            moneyLost.text = s;
            moneyEarned.font = deck.myriad;
            moneyEarned.alignment =  TextAnchor.MiddleLeft;
            s = "Total Wins: " + deck.totalWin.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.AdjustedNumber( deck.totalWin.ToString())) + " :ﻩﺩﺭﻭﺁ ﺖﺳﺩ ﻪﺑ ﻪﮑﺳ ﻉﻮﻤﺠﻣ";
                moneyEarned.font = deck.yekta;
                moneyEarned.alignment = TextAnchor.MiddleRight;
            }

            moneyEarned.text = s;
        }
        else
        {
            yourRounds.gameObject.SetActive(false);
            oppsRounds.gameObject.SetActive(false);
            moneyEarned.gameObject.SetActive(false);
            moneyLost.gameObject.SetActive(false);
        }
        winSound.Play();
        win = deck.winAmount;
        winAmount.text = deck.AdjustedNumber( PlayerPrefs.GetString("coin", "100"));
        startCounter = int.Parse( PlayerPrefs.GetString("coin","100")) - win;
        money = int.Parse(PlayerPrefs.GetString("coin", "100"));
        counterJump = win  / 45;
        okBtn.SetActive(false);
        StartCoroutine(CoinEmition());
        endMoney = money;
        StartCoroutine(addMoney());

        
    }

    public IEnumerator addMoney() {
        string s = deck.AdjustedNumber(startCounter.ToString());
        while (startCounter< endMoney)
        {
            startCounter += counterJump;
            s = deck.AdjustedNumber(startCounter.ToString());
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                winAmount.font = GameObject.Find("Background").GetComponent<LanguageManager>().yekta;
            }
                winAmount.text = s;

            yield return new WaitForSeconds(0.01f);
        }
        startCounter = money;
        s = deck.AdjustedNumber(((int)startCounter).ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            winAmount.font = GameObject.Find("Background").GetComponent<LanguageManager>().yekta;
        }
        winAmount.text = s;
    }
    

    public IEnumerator CoinEmition()
    {
        for (int i = 0; i < 15; i++)
        {
            Instantiate(coin, new Vector2(Random.Range(-4f,4f),Random.Range(2f,3f)), Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
       
        yield return new WaitForSeconds(0.5f);
        okBtn.SetActive(true);
    }
    void OnDisable()
    {
    }
}
