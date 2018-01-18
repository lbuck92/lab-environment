using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BT_Loader : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
		// c is pressed to continue trials
		if (Input.GetKeyDown ("c")) {
			Pole_Behavior.bt_loaded = true;
			SceneManager.LoadScene ("lab-scene", LoadSceneMode.Single);
		}

	}
}
