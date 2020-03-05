using UnityEngine;
using System.Collections;

public class CardDownScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x>12 || transform.position.x<-12 || transform.position.y >14 || transform.position.y < -14)
			Destroy(this.gameObject);
	}
}
