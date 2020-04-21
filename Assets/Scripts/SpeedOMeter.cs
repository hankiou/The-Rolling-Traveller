using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOMeter : MonoBehaviour {

	public GameObject player;
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Text>().text = ((int)player.GetComponent<Rigidbody>().velocity.z) +" km/h";
	}
}
