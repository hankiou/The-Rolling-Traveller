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


public class SkinCardSetup : MonoBehaviour {

	private string skin_id;
	private Transform skinImage;
	private Transform skinName;

	void Start () {
		skin_id = gameObject.name;
		int r = skin_id[5] - '0';
		gameObject.GetComponent<Image>().color = Global.rarityColors[r];

		skinImage = gameObject.transform.GetChild(0);
		skinName = gameObject.transform.GetChild(1);
		skinImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Showcases/"+skin_id);

		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var skins = db.GetCollection("skins");
		if(skin_id == Global.selectedSkin){
			skinImage.GetChild(0).gameObject.SetActive(true);
		}

        var skinsReferences = skins.FindOne();
        skinName.GetComponent<Text>().text = skinsReferences[skin_id].ToString();
	}

	public void SelectSkin(){
		GameObject.Find(Global.selectedSkin).transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
		Global.selectedSkin = gameObject.name;
		gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);

        var update = Update.Set("selected_skin", gameObject.name);
       	profiles.Update(query, update);
	}
}
