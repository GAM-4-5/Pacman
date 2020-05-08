using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ploca : MonoBehaviour {

	private static int boardWidth = 52;
	private static int boardHeight = 30;

	public int totalPellets = 0;
	public int score = 0;

	public GameObject[,] board = new GameObject[boardWidth, boardHeight];

	// Use this for initialization
	void Start () {

		Object[] objects = GameObject.FindObjectsOfType (typeof(GameObject));        //unutar zadanih veličina traži pozicije svih objekata

		foreach (GameObject o in objects) {

			Vector2 pos = o.transform.position;

			if (o.name != "pac_ot" && o.name != "zidovi" && o.name != "duhovi" && o.name != "zvjezdice" && o.name != "kugle" && o.name != "GameObject") {

//					if (o.GetComponent<Tile> () != null) {

//						if (o.GetComponent<Tile> ().Zvjezdica || o.GetComponent<Tile> ().Kugle) {

//							totalPellets++;
//						}
//					}
					board [(int) pos.x, (int) pos.y] = o;

					//Debug.Log (pos.x);
					//Debug.Log (pos.y);

    			} else {
				
					//Debug.Log ("Found PacmanMove at: " + pos);
   				}

			}
		}
	
	// Update is called once per frame

	void Update () {
				
	}
}
