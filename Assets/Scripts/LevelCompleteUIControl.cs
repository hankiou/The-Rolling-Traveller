using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteUIControl : MonoBehaviour {

	public void tryAgain(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void nextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
	}

	public void mainMenu(){
		SceneManager.LoadScene("main_menu");
	}
}
