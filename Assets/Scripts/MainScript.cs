using UnityEngine;
using UnityEngine.SceneManagement;
using ArabicSupport;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {
	public GameObject cardDeck;
	public DeckScript deck;
	public GameObject shapes;

    public GameObject restartBtn;
    public GameObject exitBtn;
    public GameObject loseWarning;

    public GameObject noBtn;

    public GameObject pausedBack;

    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject continueBtn;



	// Use this for initialization
	void Start () {
		deck = cardDeck.GetComponent<DeckScript>();
            
	}
    

    public void hokmS(){
        deck.click.Play();
        deck.hokm = 0;
		StartCoroutine(deck.dealTheRest(deck.hakem));
		shapes.SetActive(false);

	}
	public void hokmH(){
        deck.click.Play();
        deck.hokm = 1;
		StartCoroutine(deck.dealTheRest(deck.hakem));
		shapes.SetActive(false);
	}
	public void hokmC(){
        deck.click.Play();
        deck.hokm = 2;
		StartCoroutine(deck.dealTheRest(deck.hakem));
		shapes.SetActive(false);
	}
	public void hokmD(){
        deck.click.Play();
        deck.hokm = 3;
		StartCoroutine(deck.dealTheRest(deck.hakem));
		shapes.SetActive(false);
	}

    public void Continue()
    {
        deck.click.Play();
        deck.betInputObject.SetActive(true);
        string s = deck.AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            deck.money.font = deck.yekta;
        }
        deck.money.text = s;
        continueBtn.SetActive(false);
    }

    public void StartNewGame()
    {

        deck.click.Play();
        for (int i = 0; i < 4; i++)
        {
            deck.crowns[i].SetActive(false);
        }
        PlayerPrefs.SetInt("hakem", -1);
        deck.hakem = -1;
        PlayerPrefs.SetInt("totalLose", 0);
        PlayerPrefs.SetInt("friendsRounds", 0);
        PlayerPrefs.SetInt("oppsRounds", 0);
        continueBtn.SetActive(false);
        deck.friendsRoundsWon = 0;
        deck.oppsRoundsWon = 0;
        string s = deck.oppsRoundsWon.ToString();
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            deck.oppsRounds.GetComponent<Text>().font = deck.yekta;
        }
        deck.oppsRounds.GetComponent<Text>().text = s;
        s = deck.friendsRoundsWon.ToString();
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            deck.yourRounds.GetComponent<Text>().font = deck.yekta;
        }
        deck.yourRounds.GetComponent<Text>().text = s;
        StartCoroutine(deck.chooseHakem());

    }

    public void rstartGame()
    {
        PlayerPrefs.SetInt("totalLose", 0);
        PlayerPrefs.SetInt("friendsRounds", 0);
        PlayerPrefs.SetInt("oppsRounds", 0);
        PlayerPrefs.SetInt("hakem", -1);
        SceneManager.LoadScene("1");
    }
    public void confirmRestart()
    {
        deck.click.Play();
        canvas2.SetActive(true);
        canvas1.SetActive(false);
        pausedBack.SetActive(true);
        restartBtn.SetActive(true);
        exitBtn.SetActive(false);
        noBtn.SetActive(true);
        if (!deck.handIsOver)
        {
            loseWarning.SetActive(true);
        }
        else
        {
            loseWarning.SetActive(false);
        }
    }
    public void exit()
    {
        deck.click.Play();
        if (!deck.handIsOver)
        {
            deck.oppsRoundsWon++;
            PlayerPrefs.SetInt("totalLose", deck.totalLose+deck.betAmount);
            if (deck.hakem % 2 == 0)
            {
                deck.hakem++;
            }
        }
        else
        {
            PlayerPrefs.SetInt("totalLose", deck.totalLose);
        }
        PlayerPrefs.SetInt("totalWin", deck.totalWin);
        PlayerPrefs.SetInt("friendsRounds", deck.friendsRoundsWon);
        PlayerPrefs.SetInt("oppsRounds", deck.oppsRoundsWon);
        PlayerPrefs.SetInt("hakem", deck.hakem);
        SceneManager.LoadScene("MainMenu");
        
    }
    public void confirmExit()
    {
        deck.click.Play();
        canvas2.SetActive(true);
        canvas1.SetActive(false);
        pausedBack.SetActive(true);
        restartBtn.SetActive(false);
        exitBtn.SetActive(true);
        noBtn.SetActive(true);
        if (!deck.handIsOver)
        {
            loseWarning.SetActive(true);
        }
        else
        {
            loseWarning.SetActive(false);
        }
    }
    public void no()
    {
        deck.click.Play();
        canvas1.SetActive(true);
        canvas2.SetActive(false);
        pausedBack.SetActive(false);
        restartBtn.SetActive(false);
        exitBtn.SetActive(false);
        noBtn.SetActive(false);
    }
    
}



