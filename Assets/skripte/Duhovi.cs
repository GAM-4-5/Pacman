using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duhovi : MonoBehaviour {

	public Rigidbody2D rb;
	public GameObject duh;
	public float brzina = 3.8f;
	private int smjerHorizontal;
	private int smjerVertikal; 
	int number;



	// Use this for initialization
	void Start () {
		//Vector3 smjer = new Vector3 (x, y, 0);
		smjerHorizontal = 1;
		smjerVertikal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 kretanje = new Vector3(smjerHorizontal * brzina * Time.deltaTime, smjerVertikal * brzina * Time.deltaTime, 0);
		rb.velocity = kretanje;
		//Debug.Log (smjer);
	}
	void OnCollisionEnter2D (Collision2D collisionInfo){
		Debug.Log (collisionInfo.collider.tag);
		var number = Random.Range (0, 2);
		Debug.Log (number);
		if (smjerHorizontal != 0){
			smjerHorizontal = 0;
			if (number == 0) {
				smjerVertikal = -1;
			}
			if (number == 1) {
				smjerVertikal = 1;
			}
		}
		else if (smjerVertikal != 0) {
			smjerVertikal = 0;
			if (number == 0) {
				smjerHorizontal = -1;
			}
			if (number == 1) {
				smjerHorizontal = 1;
			}
		}

	}
}
