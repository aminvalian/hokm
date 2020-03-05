using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using ArabicSupport;
public class MainMenuScrpit : MonoBehaviour {

    public GameObject startGameBtn;
    public GameObject shopBtn;
    public GameObject loading;
    public GameObject canvas;
    public GameObject canvas2;

    public GameObject decks;
    public GameObject backgrounds;
    public GameObject logo;
    public GameObject coinSmall;
    public GameObject coinstack1;
    public GameObject coinStack2;

    public string currentWindow = "0";

    public GameObject watchBtn1;
    public GameObject watchBtn2;
    public GameObject languageBtn;

    public Text Money1;
    public Text Money2;
    public Text Money3;
    public Text langText;
    public AudioSource click;
    public AudioSource buySound;
    public AudioSource watchSound;
    public Font yekta;
    public Font myriad;

    // Use this for initialization
    void Start () {
        
        PlayerPrefs.SetInt("deck1", 1);
        PlayerPrefs.SetInt("back2", 1);
        if (PlayerPrefs.GetInt("firstTime", 0) == 0)
        {
            PlayerPrefs.SetInt("deck0", 1);
            PlayerPrefs.SetInt("usingdeck", 1);
            PlayerPrefs.SetInt("cover0", 1);
            PlayerPrefs.SetInt("usingcover", 0);
            PlayerPrefs.SetInt("back0", 1);
            PlayerPrefs.SetInt("usingback", 0);
            PlayerPrefs.SetInt("firstTime", 1);


        }
        loading.SetActive(false);
        string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if(PlayerPrefs.GetInt("usinglang",0) == 1)
        {
            s = ArabicFixer.Fix(s);
            Money1.font = yekta;
            langText.text = "English";
            langText.font = myriad;
        }
        else
        {
            Money1.font = myriad;
            langText.text = "ﯽﺳﺭﺎﻓ";
            langText.font = yekta;
        }
        Money1.text = s;
    }
	
	// Update is called once per frame
	void Update () {
        
        
    }
    public void exit()
    {
        Application.Quit();
    }
    public void startGameBtnClicked()
    {
        loading.SetActive(true);
        canvas.SetActive(false);
        canvas2.SetActive(true);
        click.Play();
        SceneManager.LoadScene("1");
    }

    public void ThemeClicked()
    {
        click.Play();
        startGameBtn.SetActive(false);
        languageBtn.SetActive(false);
        shopBtn.SetActive(true);
        shopBtn.GetComponent<Animator>().SetTrigger("awake");
        string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            Money2.font = yekta;
            Money3.font = yekta;
            s = ArabicFixer.Fix(s);
        }
        else
        {
            Money2.font = myriad;
            Money3.font = myriad;
        }
        Money2.text = s;
        Money3.text = s;

    }

    public void BackClicked()
    {
        click.Play();
        logo.SetActive(true);
        shopBtn.SetActive(true);
        decks.SetActive(false);
        backgrounds.SetActive(false);
        currentWindow = "0";
        string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            Money2.font = yekta;
            Money3.font = yekta;
        }
        else
        {
            Money2.font = myriad;
            Money3.font = myriad;
        }
        Money2.text = s;
        Money3.text = s;
    }

    public void BackToMenuClicked()
    {
        click.Play();
        startGameBtn.SetActive(true);
        shopBtn.SetActive(false);
        languageBtn.SetActive(true);
        string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            Money1.font = yekta;
        }
        else
        {
            Money1.font = myriad;
        }
        Money1.text = s;
    }

    public void BackgroundClicked()
    {
        click.Play();
        backgrounds.SetActive(true);
        shopBtn.SetActive(false);
        logo.SetActive(false);
        currentWindow = "2";

    }
    public void CardsClicked()
    {
        click.Play();
        logo.SetActive(false);
        shopBtn.SetActive(false);
        decks.SetActive(true);
        currentWindow = "1";
    }
    public void RestartDecks()
    {
        decks.SetActive(false);
        decks.SetActive(true);
    }
    public void RestartBackgrounds()
    {
        backgrounds.SetActive(false);
        backgrounds.SetActive(true);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("reset");
    }

    public void addMoney()
    {
        Debug.Log("added 10000");
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin","100"))+10000).ToString());
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
            StartCoroutine(add100());

        }
    }

    public IEnumerator add100()
    {
        GameObject watchBtn;
        GameObject coinStack;
        Text money;
        if (currentWindow == "1")
        {
            money = Money2;
            watchBtn = watchBtn1;
            coinStack = coinstack1;
        }
        else 
        {
            money = Money3;
            coinStack = coinStack2;
            watchBtn = watchBtn2;
        }
        for (int i = 0; i < 5; i++)
        {
            GameObject g = Instantiate(coinSmall, watchBtn.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
            g.GetComponent<RectTransform>().localPosition = watchBtn.GetComponent<RectTransform>().position;
            g.transform.SetParent(GameObject.Find("Canvas").transform);
            g.GetComponent<CoinNavigatorScript>().des = coinStack;
            
            yield return new WaitForSeconds(0.05f);
        }

        int startMoney = int.Parse(PlayerPrefs.GetString("coin", "100"));
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) + 100).ToString());
        int jump = (int)(100 / 30f);
        int endMoney = startMoney + 100;
        watchSound.Play();

        while (startMoney < endMoney)
        {
            string st = AdjustedNumber((startMoney + jump).ToString());
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
                st = ArabicFixer.Fix(st);
            money.text = st;
            startMoney += jump;
            yield return new WaitForSeconds(0.01f);
        }
        string s = AdjustedNumber(endMoney.ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
        {
            s = ArabicFixer.Fix(s);
            money.font = yekta;

        }
        else
        {
            money.font = myriad;

        }
        money.text = s;

    }
    public IEnumerator SpendMoney(GameObject item , int value)
    {
        GameObject coinStack;
        Text money;
        if (currentWindow == "1")
        {
            money = Money2;
            coinStack = coinstack1;
        }
        else
        {
            money = Money3;
            coinStack = coinStack2;
        }
        for (int i = 0; i < 5; i++)
        {
            
            GameObject g = Instantiate(coinSmall, coinStack.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
            g.GetComponent<RectTransform>().localPosition = coinStack.GetComponent<RectTransform>().position;
            g.transform.SetParent(GameObject.Find("Canvas").transform);
            g.GetComponent<CoinNavigatorScript>().des = item;
            yield return new WaitForSeconds(0.05f);
        }

        int startMoney = int.Parse(PlayerPrefs.GetString("coin", "100"));
        PlayerPrefs.SetString("coin", (int.Parse(PlayerPrefs.GetString("coin", "100")) - value).ToString());
        int jump = (int)(value / 30f);
        int endMoney = startMoney - value;
        while (startMoney > endMoney)
        {
            string str = AdjustedNumber((startMoney - jump).ToString());
            if (PlayerPrefs.GetInt("usinglang", 0) == 1)
                str = ArabicFixer.Fix(str);
            money.text = str;
            startMoney -= jump;
            yield return new WaitForSeconds(0.01f);
        }
        string st = AdjustedNumber(endMoney.ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 1)
            st = ArabicFixer.Fix(st);
        money.text = st;
        money.text = st;
        Money1.text = st;
        Money2.text = st;
        Money3.text = st;

        buySound.Play();
        if (currentWindow == "1")
        {
            RestartDecks();
        }
        else
        {
            Debug.Log("res");
            RestartBackgrounds();
        }
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
                num += "0";
            }
        }
        num += r.ToString();
        return num;
    }
    public void FarsiEngl()
    {
        string s = AdjustedNumber(PlayerPrefs.GetString("coin", "100").ToString());
        if (PlayerPrefs.GetInt("usinglang", 0) == 0)
        {
            PlayerPrefs.SetInt("usinglang", 1);
            s = ArabicFixer.Fix(s);
            Money1.font = yekta;
            Money2.font = yekta;
            Money3.font = yekta;
            langText.text = "English";
            langText.font = myriad;
        }
        else
        {
            PlayerPrefs.SetInt("usinglang", 0);
            Money1.font = myriad;
            Money2.font = myriad;
            Money3.font = myriad;
            langText.text = "ﯽﺳﺭﺎﻓ";
            langText.font = yekta;
        }
        logo.SetActive(false);
        logo.SetActive(true);
        GetComponent<LanguageManager>().adjustLang();
        Debug.Log("Language Changed");
        Money2.text = s;
        Money3.text = s;
        Money1.text = s;
        
    }
}
