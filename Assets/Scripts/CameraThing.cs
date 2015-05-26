using UnityEngine;
using System.Collections;

public class CameraThing : MonoBehaviour {

	public GameObject player;
	Vector3 toLooky;
	Vector3 playerlooky;
	Vector3 playerposold;
	Camera theCamera;
	Player thePlayer;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		theCamera = GetComponent<Camera>();
		thePlayer = player.GetComponent<Player>();
	}

	// Update is called once per frame
	void Update () {	

		playerlooky = Vector3.Lerp (playerposold, new Vector3(player.transform.position.x, player.transform.position.y, -10f), .3f);

		transform.localPosition = Vector3.Lerp (transform.localPosition, new Vector3 (player.transform.position.x, player.transform.position.y, -22f), .2f);

		//transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, -10f));

		playerposold = playerlooky;

		//print (velx);
	}
}
