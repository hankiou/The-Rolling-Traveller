using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float time;
	public bool chronoIsActive;

	public Text timeText;
	public void Start () {
		time = -3f;
	}
	
	public void FixedUpdate () {
		if(chronoIsActive) time+=Time.deltaTime;
		write();
	}

	public void write(){
		if(time > 0){
			timeText.text = Global.adaptTime(time);
		}
		else{
			timeText.text = ""+Mathf.Abs((int)(time%60)-1);
		}
	}
}
