using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLogin : MonoBehaviour {

	public void back(){
		SceneManager.LoadScene("login_screen");
	}
}
