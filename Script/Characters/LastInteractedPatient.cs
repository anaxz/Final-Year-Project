using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastInteractedPatient : MonoBehaviour {

	GameObject patient;
	int num;

	public void setChara(GameObject patient) {
		this.patient = patient;
	}

	public GameObject getChara() {
		return patient;
	}
}
