using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaSpawn : MonoBehaviour {

	public GameObject Chara;
	GameObject DayTimer;
	GameObject[] characters;

	bool spawn = true;
	bool[] freeBed = new bool[6];

	int max = 6; // max patients

	Patient patient;

	void Start() { 
		// assign valuse for each boolean
		for (int i = 0; i < freeBed.Length; i++) freeBed[i] = true;
		DayTimer = GameObject.Find("DayTimer");
	}

	void Update () {
		generateChara();
	}

	// note: used in timerController
	// destroy previous characters before spawning
	public void setSpawn(bool type) {
		destroyChara();
		reset();
		spawn = type;
	}

	void destroyChara() {
		GameObject [] charas = GameObject.FindGameObjectsWithTag("Chara");
		foreach (GameObject chara in charas) {
			Destroy(chara);
		}
	}

	void reset() {
		// make the bed free again
		for (int i = 0; i < freeBed.Length; i++) {
			freeBed[i] = true;
		}
	}

	public void generateChara(){
		//yield return new WaitForSeconds(5);

		if (spawn){
			Vector3 pos = new Vector3();

			// if first day, only spawn 3 characters
			if (DayTimer.GetComponent<TimerController>().getDay() == 1) max = 3;
			else max = 6;
				
			for (int i = 0; i < max; i++){
				int num = checkPos();               // check if there's free bed
				if (num != -1) pos = getPos(num);   // return the number and get postion
				else spawn = false;                 // else no free bed, do not spawn character

				// inverse the characters if the bed postion is greater than and equal to 3
				if (i >= 3) Instantiate(Chara, pos, Quaternion.Inverse(Chara.transform.rotation));
				else Instantiate(Chara, pos, Chara.transform.rotation);
			}
			spawn = false;
		}
	}

	public int checkPos(){
		for (int i = 0; i < freeBed.Length; i++){
			if (freeBed[i] == true){
				return i; //found free bed, return the number
			}
		}
		return -1; // no free beds
	}

	public Vector3 getPos(int num){
		/* position:
		 * 1. -8.93, 0, -14 	| 2. -8.93, 0, -4 	| 3. -8.93, 0, 7
		 * 4. 5.83, -9.33, -14 	| 5. 5.83, -9.33, -4.26 | 6. 5.83, -9.33, 6.9
		*/

		float x = 0;
		float y = 0;
		float z = 0;
		//int num = freeBed.Length;

		switch (num){
			case 0:
				x = -8.93f;
				z = -14f;
				break;
			case 1:
				x = -8.93f;
				z = -4f;
				break;
			case 2:
				x = -8.93f;
				z = 7f;
				break;
			case 3:
				x = 5.83f;
				z = -14f;
				break;
			case 4:
				x = 5.83f;
				z = -4.26f;
				break;
			case 5:
				x = 5.83f;
				z = 6.9f;
				break;
		}
		freeBed[num] = false; // set bed not free
		return new Vector3(x, y, z);
	}
}
