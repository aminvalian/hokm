using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ShopBtnManager : MonoBehaviour {

    public int index;
    public string kind;
    public AudioSource click;

    public GameObject lockObject;
    public GameObject tikObject;
    public GameObject buyBtn;
    public GameObject useBtn;
    public int price;
    public Button buy;

    public MainMenuScrpit main;


    // Use this for initialization
    void OnEnable()
     {

        tikObject.SetActive(false);
        useBtn.SetActive(false);
        lockObject.SetActive(false);
        buyBtn.SetActive(false);
        if  (price > int.Parse(PlayerPrefs.GetString("coin", "100")))
        {
            buy.GetComponent<Button>().interactable = false;
        }
        else
            buy.GetComponent<Button>().interactable = true;

        
        
        if (PlayerPrefs.GetInt(kind+index.ToString(), -1) == -1)
        {
            lockObject.SetActive(true);
            buyBtn.SetActive(true);
            useBtn.SetActive(false);
        }else if (PlayerPrefs.GetInt(kind + index.ToString(), -1) == 1)
        {
            lockObject.SetActive(false);
            buyBtn.SetActive(false);
            
            if (PlayerPrefs.GetInt("using" + kind, -1) == index)
            {
                useBtn.SetActive(false);
                tikObject.SetActive(true);
            }
            else 
            {

                useBtn.SetActive(true);
                tikObject.SetActive(false);
            }
            
        }
        
	}

    public void UseClicked()
    {
        click.Play();
        if (PlayerPrefs.GetInt("using" + kind, -1) != index)
        {
            PlayerPrefs.SetInt("using" + kind, index);
            if(kind == "deck" || kind == "cover")
            {
                main.RestartDecks();
            }
            else
            {
                main.RestartBackgrounds();
            }
        }

    }

    

    public void BuyClicked()
    {
        if(price < int.Parse(PlayerPrefs.GetString("coin", "100")))
        {
            PlayerPrefs.SetInt(kind + index.ToString(), 1);
            //UseClicked();
            StartCoroutine( main.SpendMoney(gameObject, price));
            
        }
    }
}
	
	

