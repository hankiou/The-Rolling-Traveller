using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB;

public class GameManager : MonoBehaviour {

	public Transform Player;
	private Vector3 Cp;
	public int deathCount = 0;
	public GameObject completeLevelUI;
	public Text congratsText;
	public Timer timer;
	public GameObject PausePanel;

	public float WR;
	public string WRdude;
	public float PB;

	 void Awake () {
	     QualitySettings.vSyncCount = 0;  // VSync must be disabled
	     Application.targetFrameRate = 100;
 	}

	void Start(){
		Player.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/"+Global.selectedSkin);
		getRecords();
		Cp = new Vector3(Player.position.x, Player.position.y, Player.position.z);
		Rigidbody rb = Player.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		timer.chronoIsActive = true;
	}

	void FixedUpdate () {

		if(Player.position.y < Cp.y -1){
			deathCount++;
			Respawn();
		}

		Rigidbody rb = Player.GetComponent<Rigidbody>();
		if(timer.time > 0 && timer.time < 0.5f){
			rb.constraints = RigidbodyConstraints.None;
		}
	}

	void Respawn(){

		Rigidbody rb = Player.GetComponent<Rigidbody>();
		Player.position = Cp;
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		rb.constraints = RigidbodyConstraints.None;
	}

	public void setCheckpoint(Vector3 pos){
		Cp = new Vector3(pos.x, pos.y + 0.5f, pos.z);
	}

	public void levelComplete(){
		timer.chronoIsActive = false;
		submitTime();

		completeLevelUI.SetActive(true);
		if(timer.time < PB){
			congratsText.text = "NEW PERSONAL BEST";
			congratsText.color = Color.cyan;
		}
		if(timer.time < WR){
			congratsText.text = "NEW WORLD RECORD";
			congratsText.color = Color.yellow;
		}
		Rigidbody rb = Player.GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	public void loadNext(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void submitTime(){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		
		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);

        ///// PB /////

        if(!userFound.Contains(SceneManager.GetActiveScene().name)){ // Test if the player had the level done
        	var createLevelEntry = Update.Set(SceneManager.GetActiveScene().name, timer.time);
        	profiles.Update(query, createLevelEntry);
        	var unlockNextLevel = Update.Set("unlocked", (int)userFound["unlocked"]+1);
        	profiles.Update(query, unlockNextLevel);

        	congratsText.text = "NEW PERSONAL BEST";
			congratsText.color = Color.cyan;
        }
		else if(userFound[SceneManager.GetActiveScene().name] > timer.time){ // Level Already done and improved
        	var update = Update.Set(SceneManager.GetActiveScene().name, timer.time);
       		profiles.Update (query, update);
       	}

       	////// WR ////
   		var world_records = db.GetCollection("world_records");
   		var query2 = Query.EQ("levelname", SceneManager.GetActiveScene().name);
   		var recordFound = world_records.FindOne(query2);

   		if(float.Parse(recordFound["time"].ToString()) > timer.time){ // IF time better than WR
   			var update2 = Update.Set("time", timer.time);
   			world_records.Update(query2, update2);
   			var update3 = Update.Set("username", Global.connectedUser);
   			world_records.Update(query2, update3);
       		
    	}
	}

	public void getRecords(){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");
		var world_records = db.GetCollection("world_records");

		var query = Query.EQ("username", Global.connectedUser); //Global.connectedUser
        var userFound = profiles.FindOne(query);


        if(userFound.Contains(SceneManager.GetActiveScene().name)){
        	PB = float.Parse(userFound[SceneManager.GetActiveScene().name].ToString());
        }

        var query2 = Query.EQ("levelname", SceneManager.GetActiveScene().name);
       	var recordFound = world_records.FindOne(query2);
        WR = float.Parse(recordFound["time"].ToString());
        WRdude = recordFound["username"].ToString();
        
	}
}
