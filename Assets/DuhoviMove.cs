using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuhoviMove : MonoBehaviour {

	public float speed = 3.8f;

	public Node startingPosition;

	public int scatterModeTimer1 = 8;
	public int chaseModeTimer1 = 15;
	public int scatterModeTimer2 = 8;
	public int chaseModeTimer2 = 15;
	public int scatterModeTimer3 = 5;
	public int chaseModeTimer3 = 15;
	public int scatterModeTimer4 = 5;

	private int modeChangeIteration = 1;
	private float modeChangeTimer = 0;

	public enum Mode {

		Chase,
		Scatter,
		Frightened
	}

	Mode currentMode = Mode.Scatter;
	Mode previousMode;

	private GameObject Pacman;

	private Node currentNode, targetNode, previousNode;
	private Vector2 direction, nextDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		ModeUpdate ();
		
	}

	void ModeUpdate () {

		if (currentMode != Mode.Frightened) {
			modeChangeTimer += Time.deltaTime;
			if (modeChangeIteration == 1) {
				if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer1) {
					ChangeMode (Mode.Chase);
					modeChangeTimer = 0;
				}

				if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer1) {
					modeChangeIteration = 2;
					ChangeMode (Mode.Scatter);
					modeChangeTimer = 0;
				}
			} else if (modeChangeIteration == 2) {
				if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer2) {
					ChangeMode (Mode.Chase);
					modeChangeTimer = 0;
				}
				if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer2) {
					modeChangeIteration = 3;
					ChangeMode (Mode.Scatter);
					modeChangeTimer = 0;
				}
			
			} else if (modeChangeIteration == 3) {
				if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer3) {
					ChangeMode (Mode.Chase);
					modeChangeTimer = 0;
				}
				if (currentMode == Mode.Chase && modeChangeTimer > chaseModeTimer3) {
					modeChangeIteration = 4;
					ChangeMode (Mode.Scatter);
					modeChangeTimer = 0;
				}

			} else if (modeChangeIteration == 4) {
				if (currentMode == Mode.Scatter && modeChangeTimer > scatterModeTimer4) {
					ChangeMode (Mode.Chase);
					modeChangeTimer = 0;
				}
			}
		} else if (currentMode == Mode.Frightened) {
		}
	}
	
	void ChangeMode (Mode m) {
		
		currentMode = m;
	}

	 Node GetNodeAtPosition (Vector2 position) {
		
	    GameObject title = GameObject.Find ("Game").GetComponent<GameBoard> ().board [(int)position.x, (int)position.y];

	    if (title != null) {
			
			if (title.GetComponent<Node> != null) {
				
				return title.GetComponent<Node> ();
			}
		}

		return null;
	}

	GameObject GetPortal (Vector2 position) {

		GameObject title = GameObject.Find ("Game").GetComponent<GameBoard> ().board [(int)position.x, (int)position.y];

		if (title != null) {

		}

	}

}
