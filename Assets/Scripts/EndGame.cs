using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public void QuitGame () {
		Application.Quit();
	}

	public void BackToMain(){
		SceneManager.LoadScene("main_menu");
	}
}
