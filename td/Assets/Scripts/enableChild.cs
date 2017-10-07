using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChild : MonoBehaviour {
	/* Denne klassen gjør ikke annet enn å aktivere det første childobjektet,
	 * nyttig om man vil skjule objekter når man designer,
	 * slik at man slipper å drive å skru objektene av og på mellom hver test manuelt */
	void Start () {
		this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
	}
}