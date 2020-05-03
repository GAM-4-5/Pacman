using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ploca : MonoBehaviour {

	private static int boardWidth = 20;
	private static int boardHeight = 18;

	public GameObject[,] board = new GameObject[boardWidth, boardHeight];

	// Use this for initialization
	void Start () {

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));

		foreach (GameObject o in objects) {

			Vector2 pos = o.transform.position;
			if (o.name != "pac_ot") {
				board [Mathf.Abs((int) pos.x), Mathf.Abs((int) pos.y)] = o;
			} else {
				Debug.Log ("Found PacmanMove at: " + pos);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

	}
}
