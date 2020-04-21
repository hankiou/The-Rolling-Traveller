using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;


public class login : MonoBehaviour {

	public Text usernameField;
	public Text passwordField;
	public Text errorText;

	public void submitConnection(){
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
	        if(userFound == null){
	        	errorText.text = "Unknown user, make sure you created a profile";
	        }
	        else{
	        	if(pass != userFound["password"]){
	        		errorText.text = "Incorrect password";
	        	}
	        	else{ // Success
	        		Global.connectedUser = user;
	        		SceneManager.LoadScene("main_menu");
	        	}
	        }
		}
		else errorText.text = "Some fields are empty";
	}

	public void getToProfileCreation(){
		SceneManager.LoadScene("create_profile");
	}
}
 