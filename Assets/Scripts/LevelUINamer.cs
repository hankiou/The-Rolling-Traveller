using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUINamer : MonoBehaviour {

	public void Start(){
		gameObject.GetComponent<Text>().text = transform.parent.name;
	}
}
