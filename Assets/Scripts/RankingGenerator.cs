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

public class RankingGenerator : MonoBehaviour {

	private class playerIR{
		public string username, levelname;
		public float time;

		public playerIR(string u, string l, float t){
			username = u;
			levelname = l;
			time = t;
		}
	}

	private GameObject rankingListContent;
	public GameObject playerInRankingPrefab;

	public void Start(){
		gameObject.transform.GetChild(0).GetComponent<Text>().text = gameObject.name;
	}

	public void generateRankingOfLevel(){
		rankingListContent = GameObject.Find("RankListContent");

		foreach(Transform child in rankingListContent.transform){	// Clear the list
			GameObject.Destroy(child.gameObject);
		}

		string levelname = gameObject.name;

		MongoClient client = new MongoClient("mongodb://hankiou:admin@ds229790.mlab.com:29790/the_rolling_traveller");
		MongoServer server = client.GetServer();
		MongoDatabase db = server.GetDatabase("the_rolling_traveller");
		var profiles = db.GetCollection("profiles");

		var profilesList = profiles.FindAll();
		List<playerIR> pl = new List<playerIR>();
		int index = 0;
		foreach(var profile in profilesList){
			if(profile.Contains(levelname)){

				pl.Add( new playerIR(profile["username"].ToString(), levelname, float.Parse(profile[levelname].ToString())));
				index++;
			}
		}

		pl.Sort((x, y) => x.time.CompareTo(y.time));;
		int rank = 1;
		foreach(playerIR player in pl){
			GameObject clone = Instantiate(playerInRankingPrefab, rankingListContent.transform);
			var scriptInstance = clone.GetComponent<PlayerInRanking>();
			scriptInstance.Builder(rank, player.username, player.levelname, player.time);
			rank++;
		}
	}
}
