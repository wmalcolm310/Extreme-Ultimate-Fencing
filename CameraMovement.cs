using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;
	private GameObject self;
	private float maxEdge = 5;
	private float minEdge = -5;
	float dif;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		dif = Mathf.Abs(player1.transform.position.x - player2.transform.position.x);
		if (dif <= 1.4) {
			if (dif >= 1.2)
				Camera.main.orthographicSize = dif/2;
		}
		//else 
		//	Camera.main.orthographicSize = 10;
		if (player1.transform.position.x <= minEdge || player2.transform.position.x <= minEdge)
			Camera.main.transform.position = new Vector3(minEdge/2,
			                                             0,
			                                             Camera.main.transform.position.z);
		else if (player1.transform.position.x >= maxEdge || player2.transform.position.x >= maxEdge)
			Camera.main.transform.position = new Vector3(maxEdge/2,
			                                             0,
			                                             Camera.main.transform.position.z);
		else
			Camera.main.transform.position = new Vector3((player1.transform.position.x + player2.transform.position.x)/2,
			                                             0,
			                                             Camera.main.transform.position.z);

		}
}
