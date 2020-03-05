using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour {

    bool played = false;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    void Update()
    {
        if (!played && !Application.isShowingSplashScreen)
        {
            Debug.Log("a");
            anim.speed = 1;

            played = true;
            StartCoroutine(load());
        }
    }
	
    public IEnumerator load()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
