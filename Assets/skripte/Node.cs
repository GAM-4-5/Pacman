﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Node[] neighbors;
	public Vector2[] validDirections;

	// Use this for initialization
	void Start () {

		validDirections = new Vector2[neighbors.Length];         //sprema pozicije nodova i susjednih nodova

		for (int i = 0; i < neighbors.Length; i++) {

			Node neighbor = neighbors [i];
			Vector2 tempVector = neighbor.transform.localPosition - transform.localPosition;

			validDirections [i] = tempVector.normalized;
		}
	}
		

	
	//Update is called once per frame
	void Update () {
		
	}
}
