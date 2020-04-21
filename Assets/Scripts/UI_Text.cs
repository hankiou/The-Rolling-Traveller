using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Text : MonoBehaviour {

	public Text DcText;
	public Text levelName;
	public Text WRtext;
	public Text WRdudeText;
	public Text PBtext;
	public GameManager gameManager;


	public void Update () {
		levelName.text = SceneManager.GetActiveScene().name;
		DcText.text = "Deaths: "+gameManager.deathCount;
		displayRecords();
	}

	public void displayRecords(){
		WRtext.text = "WR: "+Global.adaptTime(gameManager.WR);
		WRdudeText.text = "by "+ gameManager.WRdude;
		if(gameManager.PB > 0){
			PBtext.text = "PB: "+Global.adaptTime(gameManager.PB);
		}
		else PBtext.text = "PB: --:--:--";
	}
}
