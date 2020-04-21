using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDB;

public class StartGame : MonoBehaviour {

	public Text Username;
	public GameObject loadingText;
	public GameObject mainMenu;	// PANELS
	public GameObject levelSelection;
	public GameObject credits;
	public GameObject Ranking;
	public GameObject SkinsPanel;
	public GameObject OpenChestScreen;
	public GameObject backButton;
	public GameObject levelListContent;
	public GameObject levelButtonPrefab;
	public GameObject levelsInRankingContent;
	public GameObject levelButtonInRankingPrefab;
	public GameObject skinListContent;
	public GameObject skinCardPrefab;
	public GameObject OpenButton;
	public GameObject ChestAvailable;
	public GameObject ButtonToChests;

	public void Start(){
		generateLevelList();
		generateLevelListInRanking();
		generateSkinList();
		mainMenu.SetActive(true);
		loadingText.SetActive(false);

		Username.text = "Connected as "+Global.connectedUser;
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);
        Global.amountOfChests = (int)userFound["chests"];
        if(Global.amountOfChests == 0){
			OpenButton.GetComponent<Button>().interactable = false;
		}
		else{
			OpenButton.GetComponent<Button>().interactable = true;
		}
	}

	public void FixedUpdate(){
		ChestAvailable.GetComponent<Text>().text = Global.amountOfChests + " CHESTS AVAILABLE";
		ButtonToChests.transform.GetChild(0).GetComponent<Text>().text = "OPEN CHESTS ("+Global.amountOfChests+")";
	}

	public void goToLevelSelection(){
		mainMenu.SetActive(false);
		backButton.SetActive(true);
		levelSelection.SetActive(true);
	}

	public void goToRanking(){
		mainMenu.SetActive(false);
		backButton.SetActive(true);
		Ranking.SetActive(true);
	}

	public void goToSkinsPanel(){
		mainMenu.SetActive(false);
		OpenChestScreen.SetActive(false);
		backButton.SetActive(true);
		SkinsPanel.SetActive(true);
	}

	public void goToChestOpening(){
		SkinsPanel.SetActive(false);
		OpenChestScreen.SetActive(true);
		backButton.SetActive(false);
	}

	public void backToMain(){
		mainMenu.SetActive(true);
		Ranking.SetActive(false);
		backButton.SetActive(false);
		levelSelection.SetActive(false);
		credits.SetActive(false);
		SkinsPanel.SetActive(false);
	}

	public void generateLevelList(){
		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);
        int unlocked = (int)userFound["unlocked"];

		string path = "Assets/Scenes/Levels";
		string[] files = Directory.GetFiles(path, "*.unity");
		for(int i=0; i<files.Length; i++){
			string lname = Path.GetFileNameWithoutExtension(files[i]);

			GameObject clone = Instantiate(levelButtonPrefab, levelListContent.transform);
			clone.name = lname;

			if(i>=unlocked){
				clone.GetComponent<Button>().interactable = false;
			}
			
		}
	}

	public void generateLevelListInRanking(){
		string path = "Assets/Scenes/Levels";
		string[] files = Directory.GetFiles(path, "*.unity");
		for(int i=0; i<files.Length; i++){
			string lname = Path.GetFileNameWithoutExtension(files[i]);

			GameObject clone = Instantiate(levelButtonInRankingPrefab, levelsInRankingContent.transform);
			clone.name = lname;
		}
	}

	public void generateSkinList(){

		clearSkinList();

		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");
		var skinReferences = db.GetCollection("skins");

		var query = Query.EQ("username", Global.connectedUser);
        var userFound = profiles.FindOne(query);

		Global.selectedSkin = userFound["selected_skin"].ToString();

        foreach(var skin in userFound["skins"].AsBsonArray){
        	
        	GameObject clone = Instantiate(skinCardPrefab, skinListContent.transform);
			clone.name = skin.ToString();
        }
	}

	public void clearSkinList(){
		foreach(Transform skin in skinListContent.transform){
			GameObject.Destroy(skin.gameObject);
		}
	}

	public void PlayGame () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void Credits(){
		credits.SetActive(true);
		backButton.SetActive(true);
		mainMenu.SetActive(false);
	}

}
