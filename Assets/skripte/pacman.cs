using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pacman : MonoBehaviour {
	// brzina i smjerovi kretanja
	public CharacterController2D kretanje;

	public float brzina = 3f;  

	float lijevodesno = 0f;

	float goredolje = 0f;

	// Use this for initialization
	//void Start () {
		
	//}
	
	// Update is called once per frame
	// kretanje 
	public void Update () {
		lijevodesno = Input.GetAxisRaw("Horizontal") * brzina;
		goredolje = Input.GetAxisRaw("Vertical") * brzina;
		
	}
	public void FixedUpdate () {
		kretanje.Move(lijevodesno * Time.fixedDeltaTime, false, false);
		kretanje.Move1(false, goredolje * Time.fixedDeltaTime, false);
	}

}
