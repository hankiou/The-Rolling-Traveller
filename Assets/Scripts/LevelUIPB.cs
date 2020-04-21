using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB;

public class LevelUIPB : MonoBehaviour {


	public void Start(){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);

        if(userFound.Contains(transform.parent.name)){
	        string raw = userFound[transform.parent.name].ToString();
	        float time = float.Parse(raw);

			string adapted = Global.adaptTime(time);
			gameObject.GetComponent<Text>().text = "PB: "+adapted;
		}
	}


}
