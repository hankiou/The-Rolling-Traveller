using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scrLevelSelect : MonoBehaviour {

	public void selectLevel(){
		SceneManager.LoadScene(gameObject.name);
	}
}
