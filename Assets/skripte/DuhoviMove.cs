using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuhoviMove : MonoBehaviour {

	public float moveSpeed = 3.8f;

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


	private GameObject pac_ot;

	private Node currentNode, targetNode, previousNode;
	private Vector2 direction, nextDirection;


	// Use this for initialization
	void Start () {


		direction = Vector2.right;

		//Debug.Log (direction);

		pac_ot = GameObject.FindGameObjectWithTag ("Pacman");

		Node node = GetNodeAtPosition (transform.localPosition);

		targetNode = node;
		Debug.Log (targetNode);
		Debug.Log (currentNode);
		Debug.Log (previousNode);
		if (node != null) {

			currentNode = node;

		}
			

		previousNode = currentNode;
		Debug.Log (previousNode);

		Vector2 PacmanPosition = pac_ot.transform.position;
		Vector2 targetTile = new Vector2 (Mathf.RoundToInt (PacmanPosition.x), Mathf.RoundToInt (PacmanPosition.y));
		targetNode = GetNodeAtPosition (targetTile);
		//Debug.Log (PacmanPosition);
		//Debug.Log (targetNode);
		Debug.Log(targetTile);

	}

	//Update is called once per frame
	void Update () {

		//ModeUpdate ();

		Move ();
		Debug.Log (targetNode);

		//Debug.Log (OverShotTarget ());


	}

	void Move () {
		//Debug.Log (currentNode);
		//Debug.Log (targetNode);

		if (targetNode != currentNode && targetNode != null) {

			if (OverShotTarget ()) {

				Debug.Log ("OverShot");

				currentNode = targetNode;

				transform.localPosition = currentNode.transform.position;

				GameObject otherPortal = GetPortal (currentNode.transform.position);

				if (otherPortal != null) {

					transform.localPosition = otherPortal.transform.position;

					currentNode = otherPortal.GetComponent<Node> ();

				}

				targetNode = ChooseNextNode ();
				previousNode = currentNode;
				currentNode = null;

			} else {
				


				transform.localPosition += ((Vector3)direction * moveSpeed * Time.deltaTime);

				//Debug.Log (tst);
				//Debug.Log (direction);
				Debug.Log ("SupposeToMove");

			}
		} 
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

	Node ChooseNextNode () {

		Vector2 targetTile = Vector2.zero;

		Vector2 PacmanPosition = pac_ot.transform.position;
		targetTile = new Vector2 (Mathf.RoundToInt (PacmanPosition.x), Mathf.RoundToInt (PacmanPosition.y));

		Node moveToNode = null;

		Node[] foundNodes = new Node[4];
		Vector2[] foundNodesDirection = new Vector2[4];

		int nodeCounter = 0;

		for (int i = 0; i < currentNode.neighbors.Length; i++) {
			if (currentNode.validDirections [i] != direction * (-1)) {
				foundNodes [nodeCounter] = currentNode.neighbors [i];
				foundNodesDirection [nodeCounter] = currentNode.validDirections [i];
				nodeCounter++;
			}
		}

		if (foundNodes.Length == 1) {

			moveToNode = foundNodes [0];
			direction = foundNodesDirection [0];

		}

		if (foundNodes.Length > 1) {

			float leastDistance = 100000f;

			for (int i = 0; i < foundNodes.Length; i++) {

				if (foundNodesDirection [i] != Vector2.zero) {

					float distance = GetDistance (foundNodes [i].transform.position, targetTile);

					if (distance < leastDistance) {
						leastDistance = distance;
						moveToNode = foundNodes [i];
						direction = foundNodesDirection [i];
					}
				}
			}

		}
		return moveToNode;
	}

	Node GetNodeAtPosition (Vector2 pos) {
		
		GameObject tile = GameObject.Find ("Ploca").GetComponent<ploca> ().board [(int)pos.x, (int)pos.y];

		if (tile != null) {

			if (tile.GetComponent<Node> () != null) {

				return tile.GetComponent<Node> ();


			}
		}

		return null;

	}

	GameObject GetPortal (Vector2 pos) {

		GameObject tile = GameObject.Find ("Ploca").GetComponent<ploca> ().board [(int)pos.x, (int)pos.y];

		if (tile != null) {
			
			if (tile.GetComponent<Tile> ().Portal) {
				
				GameObject otherPortal = tile.GetComponent<Tile> ().PortalReceiver;

				return otherPortal;
			}
		}

		return null;
	}
	float LengthFromNode(Vector2 targetPosition) {
		Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
		return vec.sqrMagnitude;
	}

	bool OverShotTarget () {
		float nodeToTarget = LengthFromNode (targetNode.transform.position);
		float nodeToSelf = LengthFromNode (transform.localPosition);

		return nodeToSelf > nodeToTarget;
		
	}
	float GetDistance (Vector2 posA, Vector2 posB) {
		float dx = posA.x - posB.x;
		float dy = posA.y - posB.y;

		float distance = Mathf.Sqrt (dx * dx + dy * dy);

		return distance;
	}
}
	