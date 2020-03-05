using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {

	public bool isMoving = false;
	public GameObject cam;
	private Vector2 mouseDownPos;
	// Use this for initialization
	void Start () {
		mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetMouseButtonDown(0)){
			mouseDownPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			isMoving = true;
		}
		if (Input.GetMouseButtonUp(0)){
			isMoving = false;
		}
		if (isMoving){
			float x = 0 ;
			if (Mathf.Abs(cam.transform.position.x+mouseDownPos.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x)<8)
				x = mouseDownPos.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x; 
			 
			cam.transform.position = new Vector3(cam.transform.position.x+x,0,-10);
		}

	}
}
