using UnityEngine;
using System.Collections;

public class jetpackTrailer : MonoBehaviour {


	GameObject jetpackLight;
	public bool Moving;
	public bool toDestruct = false;
	public bool toEnable = false;

	// Use this for initialization
	void Start () {
	
		jetpackLight = GameObject.FindGameObjectWithTag("jetpackLight");
		Moving = true;
		TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
		trailRenderer.autodestruct = toDestruct;
		trailRenderer.enabled = toEnable;
	}
	// Update is called once per frame
	void Update () {

		if(Moving){
		transform.localPosition = jetpackLight.transform.TransformPoint(jetpackLight.transform.localPosition);
		}
	}
}
