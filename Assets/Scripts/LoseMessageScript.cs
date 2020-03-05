using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ArabicSupport;

public class LoseMessageScript : MonoBehaviour {

    public Text loseAmount;
    public Text loseMsg;
    public Text wallet;
    public Text yourRounds;
    public Text oppsRounds;
    public Text moneyLost;
    public Text moneyEarned;
    public DeckScript deck;
    public GameObject okBtn;
    public GameObject walletObj;
    

    public AudioSource loseSound;
    int money;
    float jump;
    int startmoney;
	void OnEnable()
    {
        string s;
        if (deck.oppsRoundsWon >= 7)
        {
            okBtn.SetActive(true);
            walletObj.SetActive(false);
            yourRounds.gameObject.SetActive(true);
            oppsRounds.gameObject.SetActive(true);
            moneyEarned.gameObject.SetActive(true);
            moneyLost.gameObject.SetActive(true);

            s = "You lost this game";
            loseMsg.font = deck.myriad;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = "!ﺪﻴﺘﺧﺎﺑ ﺍﺭ ﯼﺯﺎﺑ ﻦﻳﺍ ﺎﻤﺷ";
                loseMsg.font = deck.yekta;
            }
            loseMsg.text = s;
            s = deck.AdjustedNumber(PlayerPrefs.GetString("coin", "100"));
            loseAmount.font = deck.myriad;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                loseAmount.font = deck.yekta;
            }
            loseAmount.color = Color.white;
            loseAmount.text = s;
            s = "Your Rounds: " + deck.friendsRoundsWon.ToString();
            yourRounds.font = deck.myriad;
            yourRounds.alignment = TextAnchor.MiddleLeft;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.friendsRoundsWon.ToString()) + " :ﺎﻤﺷ ﻩﺩﺮﺑ ﯼﺎﻫ ﺖﺳﺩ";
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
            moneyEarned.alignment = TextAnchor.MiddleLeft;
            s = "Total Wins: " + deck.totalWin.ToString();
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(deck.AdjustedNumber(deck.totalWin.ToString())) + " :ﻩﺩﺭﻭﺁ ﺖﺳﺩ ﻪﺑ ﻪﮑﺳ ﻉﻮﻤﺠﻣ";
                moneyEarned.font = deck.yekta;
                moneyEarned.alignment = TextAnchor.MiddleRight;
            }

            moneyEarned.text = s;
        }
        else
        {
            walletObj.SetActive(true);

            yourRounds.gameObject.SetActive(false);
            oppsRounds.gameObject.SetActive(false);
            moneyEarned.gameObject.SetActive(false);
            moneyLost.gameObject.SetActive(false);

            loseMsg.text = "You lost";
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                loseMsg.text = "!ﺪﻴﺘﺧﺎﺑ ﺎﻤﺷ";
                loseMsg.font = deck.yekta;
            }
            loseAmount.color = Color.red;

            s = deck.AdjustedNumber(deck.betAmount.ToString());
            loseAmount.font = deck.myriad;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                loseAmount.font = deck.yekta;
            }
            loseAmount.text = s;
            s = deck.AdjustedNumber(PlayerPrefs.GetString("coin", "100"));
            wallet.font = deck.myriad;
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                wallet.font = deck.yekta;
            }
            wallet.text = s;
            startmoney = int.Parse(PlayerPrefs.GetString("coin", "100")) + deck.betAmount;
            money = int.Parse(PlayerPrefs.GetString("coin", "100"));
            jump = deck.betAmount / 30;
        }
    }

    void Update()
    {
        if (deck.oppsRoundsWon < 7)
        {
            if (startmoney - (int)jump > money)
            {
                startmoney -= (int)jump;
                if (!loseSound.isPlaying)
                {
                    loseSound.Play();
                }
            }
            else
            {
                startmoney = money;
                okBtn.SetActive(true);
            }
            string s = deck.AdjustedNumber(startmoney.ToString());

            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            {
                s = ArabicFixer.Fix(s);
                wallet.font = deck.yekta;

            }
            wallet.text = s;

        }
    }

    void OnDisable()
    {
        okBtn.SetActive(false);
    }

}
