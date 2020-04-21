using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB;


public class create_profile : MonoBehaviour {

	public Text usernameField;
	public Text passwordField;
	public Text errorText;

	public void submitCreation(){
		string user = usernameField.text;
		string pass = passwordField.text;

		if(user != "" && pass != "")
		{
			MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
			MongoServer server = client.GetServer();
			MongoDatabase db = server.GetDatabase("the_rolling_traveller");
			var profiles = db.GetCollection("profiles");
	
			
			var query = Query.EQ("username", user);
	        var userFound = profiles.FindOne(query);
	        if(userFound == null){ // Success
	        	var documnt = new BsonDocument
	            {
	                {"username",user},
	                {"password",pass},
	                {"unlocked",1},
	                {"chests", 10},
	                {"skins", new BsonArray{"skin_0"}},
	                {"selected_skin", "skin_0"}
	            };
	            profiles.Insert(documnt);
	            Global.connectedUser = user;
	            SceneManager.LoadScene("main_menu");
	        }
	        else{
	        	errorText.text = "Username already taken";
	        }
		}
		else errorText.text = "Some fields are empty";
	}
}
 