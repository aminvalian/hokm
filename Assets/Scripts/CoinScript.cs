using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

    public float startForce;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 2f);
        GetComponent<Rigidbody2D>().AddForce(new Vector2( Random.Range(-100f, 100f), Random.Range(1f+startForce, 3f+startForce)));
        //
        
	}
}
