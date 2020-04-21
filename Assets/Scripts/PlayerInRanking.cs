using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInRanking : MonoBehaviour {

	public Text Rank, Username, Levelname, Time;

	public void Builder(int rank, string username, string levelname, float time){
		Rank.text = ""+rank;
		Username.text = username;
		Levelname.text = levelname;
		Time.text = Global.adaptTime(time);
	}
}
