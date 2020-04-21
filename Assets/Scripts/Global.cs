using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

	public static string connectedUser = "zoo"; // DEBUG
	public static string selectedSkin = "skin_0";
	public static int amountOfChests = 0;

	// Rarity Colors
	public static Color[] rarityColors = {
		new Color(115f/255f, 115f/255f, 115f/255f),
		new Color(51f/255f, 102f/255f, 255f/255f),
		new Color(140f/255f, 26f/255f, 255f/255f),
		new Color(255f/255f, 77f/255f, 204f/255f),
		new Color(255f/255f, 51f/255f, 51f/255f),
		new Color(255f/255f, 204f/255f, 0f/255f)
	};

	public static string adaptTime(float time){
		string adapted;
		string m="", s="", cs="";
		if((int)(time/60) < 10 && (int)(time/60) > 0) {m = "0";}
		else {m = "";}
		if((int)(time%60) < 10 && (int)(time%60) > 0) {s = "0";}
		else {s = "";}
		if((int)((time * 100)%100) < 10 && (int)((time * 100)%100) > 0) {cs = "0";}
		else {cs = "";}
		adapted = m+(int)(time/60)+":"+s+(int)(time%60)+":"+cs+(int)((time * 100)%100);

		return adapted;
	}

}
