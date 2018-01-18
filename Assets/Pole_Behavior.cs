using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.IO;

public class Pole_Behavior : MonoBehaviour {

	public float shoulder_width;
	public string pairnum;

	GameObject pole1;
	GameObject pole2;
	Vector3 pole1_position;
	Vector3 pole2_position;
	float[] widths;
	int array_pos = 0;
	float tmp;
    public static bool bt_loaded = false;


	void Awake () {

		pole1 = GameObject.Find ("Pole.001");
		pole2 = GameObject.Find ("Pole.002");

		shoulder_width = ItoM (shoulder_width);
		widths = new float[39];
		CalculateWidths ();

		var ipaddress = Network.player.ipAddress;
		Debug.Log (ipaddress);
		//for(int i = 0; i < widths.Length; i++)
		//	Debug.Log(widths[i]);
		
	}

	void Update () {

		if(Input.GetKeyDown("space")){


			if (array_pos == 39 && !bt_loaded) {
				
				SceneManager.LoadScene ("between-trials", LoadSceneMode.Single);

			} else if (array_pos == 39 && bt_loaded) {



			}else {

				tmp = widths [array_pos] * 0.5f;

				pole1_position = pole1.transform.position;
				pole2_position = pole2.transform.position;
				pole1_position.z = -tmp;
				pole2_position.z = tmp;

				pole1.transform.position = pole1_position;
				pole2.transform.position = pole2_position;

				array_pos++;
			}
		}

	}

	// converts from inches to meters
	float ItoM(float inches){

		return inches * 0.0254f;

	}

	// calculates trials
	void CalculateWidths(){

		float[] percentages = {0.15f,0.25f,0.45f,0.50f,0.65f,0.75f,1f,1.25f,1.35f,1.5f,1.65f,1.85f,2f};
		int widths_position = 0;

		for (int i = 0; i < 13; i++) {
			for (int j = 0; j < 3; j++) {
				widths [widths_position] = shoulder_width * percentages [i];
				widths_position++;
			}
		}

		Shuffle (widths);
		WriteFile ();

	}
	 
	// shuffles an array
	void Shuffle(float[] array){

		for (int i = 0; i < array.Length; i++) {
			int r = Random.Range (0, array.Length);
			float tmp = array [i];
			array [i] = array [r];
			array [r] = tmp;
		}

	}

	void WriteFile(){
	
		if (!bt_loaded) {

			var logfile = File.CreateText ("judged_" + pairnum);
			logfile.WriteLine (System.DateTime.Now + System.Environment.NewLine + "Pair Number: " + pairnum);

			for(int i = 0; i < 39; i++){
				int t = i + 1;
				logfile.WriteLine ("Trial " + t + " width: " + widths [i] + " meters");
			}

			logfile.Close ();

		} else {
		
			var logfile2 = File.CreateText ("actualized_" + pairnum);
			logfile2.WriteLine (System.DateTime.Now + System.Environment.NewLine + "Pair Number: " + pairnum);

			for(int i = 0; i < 39; i++){
				int t = i + 1;
				logfile2.WriteLine ("Trial " + t + " width: " + widths [i] + " meters");
			}

			logfile2.Close ();

		}
	
	}

}
