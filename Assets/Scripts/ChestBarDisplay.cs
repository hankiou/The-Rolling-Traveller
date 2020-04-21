using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB;

public class ChestBarDisplay : MonoBehaviour {

	private List<float> chances;
	public float valueOfPicked = 0f;

	public void Start(){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var openchestinfo = db.GetCollection("openchestinfo");
        var chestInfo = openchestinfo.FindOne();

		chances = new List<float>();
		for(int i=1; i<6; i++){
			chances.Add(float.Parse(chestInfo["chances_"+i].ToString()));
		}
	}
	void FixedUpdate () {
		Slider slider = gameObject.GetComponent<Slider>();
		if(slider.value < valueOfPicked) slider.value += Time.deltaTime/3;
		else{
			Invoke("FinalDisplay()", 0.5f);
			foreach(Transform child in transform.parent){
				if(child.tag == "Skin_card"){
					child.gameObject.SetActive(true);
					
					if(Global.amountOfChests == 0){
						GameObject.Find("OpenButton").GetComponent<Button>().interactable = false;
					}
					else{
						GameObject.Find("OpenButton").GetComponent<Button>().interactable = true;
					}
					
					Destroy(gameObject);
				}
			}
		}
		int value = valueOf(slider.value);
		Image bar = GameObject.Find("ChestOpenerSliderFill").GetComponent<Image>();
		bar.color = Global.rarityColors[value];
	}

	public int valueOf(float pick){
		float total = 0f;
		int value = 0;
		
		for(int i=0; i<5; i++){
			total+= chances[i];
			if(pick > total) value++;
		}
		return value+1;
	}
}
