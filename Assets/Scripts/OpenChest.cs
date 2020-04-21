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

public class OpenChest : MonoBehaviour {

	public GameObject ChestBarPrefab;
	public GameObject SkinCardPrefab;
	public void openChest(){
		Global.amountOfChests --;
		GameObject.Find("OpenButton").GetComponent<Button>().interactable = false;

		foreach(Transform child in transform){
			if(child.tag == "Skin_card"){
				Destroy(child.gameObject);
			}
		}

		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var skins = db.GetCollection("skins");
		var profiles = db.GetCollection("profiles");
		var openchestinfo = db.GetCollection("openchestinfo");
        var chestInfo = openchestinfo.FindOne();
		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);
        var updateChests = Update.Set("chests", Global.amountOfChests);
        profiles.Update(query, updateChests);

		List<float> chances = new List<float>();
		for(int i=1; i<6; i++){
			chances.Add(float.Parse(chestInfo["chances_"+i].ToString()));
		}
		float pick = pickNumber();
		int value = valueOfObject(chances, pick);
		string pickedSkin = chooseItem(value);

        bool alreadyIn = false;
        foreach(var item in userFound["skins"].AsBsonArray){
        	if(pickedSkin == item.ToString()){
        		alreadyIn = true;
        		break;
        	}
        }
        if(!alreadyIn){
        	var update = Update.Push("skins", pickedSkin);
   			profiles.Update(query, update);
        }

        Display(pick, pickedSkin);

	}

	public float pickNumber(){
		float test = Random.value;
		return test;
	}

	public int valueOfObject(List<float> chances, float pick){
		float total = 0f;
		int value = 0;
		
		for(int i=0; i<5; i++){
			total+= chances[i];
			if(pick > total) value++;
		}
		return value+1;
	}

	public string chooseItem(int value){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var skins = db.GetCollection("skins");
		var openchestinfo = db.GetCollection("openchestinfo");
        var chestInfo = openchestinfo.FindOne();

        int amountOfItems = (int)chestInfo["amount_"+value];
        int item = (int)Random.Range(1, amountOfItems+1);
		string item_id = "skin_"+(value*100 + item);
		return item_id;
	}

	public void Display(float pick, string pickedSkin){
		GameObject chestBar = Instantiate(ChestBarPrefab, transform);
		chestBar.GetComponent<ChestBarDisplay>().valueOfPicked = pick;
		GameObject card = Instantiate(SkinCardPrefab, transform);
		card.name = pickedSkin;
		card.SetActive(false);
	}
}
