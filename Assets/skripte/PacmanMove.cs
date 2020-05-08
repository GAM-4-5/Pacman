using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PacmanMove : MonoBehaviour {

	private Node currentNode, previousNode, targetNode;

	public float speed = 0.1f;

	public int pojedeneZvjezdice = 0;

	private Vector2 direction = Vector2.zero;
	private Vector2 nextDirection;

	public GameObject pac;
	public GameObject du;

	public TextMeshProUGUI Score;

	// Use this for initialization
	void Start () {

		Node node = GetNodeAtPosition (transform.localPosition);

		if (node != null) {

			currentNode = node;
			//Debug.Log (currentNode);
		}

		direction = Vector2.up;
		ChangePosition (direction);

	}

	// Update is called once per frame
	void Update () {

		//Debug.Log ("SCORE: " + GameObject.Find ("Ploca").GetComponent<ploca> ().score);
		if (du == null) {     //pozivanje funkcija za kretanje pacmana
			CheckInput ();
			MovePlayer ();
			UpdateOrientation ();

			jedenjeZvjezdica ();
		}
		if (pac == null) {   //pozivanje funkcija za kretanje duhova
			MovePlayer ();
			//Debug.Log (currentNode);
			//Debug.Log (targetNode);
			if (currentNode == targetNode) {
				CheckSmjer ();
			}
		}

		Score.text = pojedeneZvjezdice.ToString ();


	}

	void CheckSmjer () {       //random odabir smjera kratnja duhova na svakom raskrižju

		var number = Random.Range (0, 4);

		if (number == 0) {
			ChangePosition (Vector2.left);

		} else if (number == 1) {
			ChangePosition (Vector2.right);

		} else if (number == 2) {
			ChangePosition (Vector2.up);

		} else if (number == 3) {
			ChangePosition (Vector2.down);
		}

	}


	void CheckInput(){    //kretanje lika pomoću tipki strelica
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			ChangePosition (Vector2.left);

		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			ChangePosition (Vector2.right);

		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			ChangePosition (Vector2.up);

		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			ChangePosition (Vector2.down);
		}
	
	}

	void ChangePosition (Vector2 d) {    //kad pacman dode do nodea, čeka naredbu za daljnje kretanje

		if (d != direction)
			nextDirection = d;

		if (currentNode != null) {

			Node moveToNode = CanMove (d);

			if (moveToNode != null) {

				direction = d;
				targetNode = moveToNode;
				previousNode = currentNode;
				currentNode = null;
			}
		}
	}

	void MovePlayer(){         //kretanje likova

		if (targetNode != currentNode && targetNode != null) {          

			if (nextDirection == direction * -1) {                      //provjerava mogućnost kretanja u nekom smjeru

				direction *= -1;
				Node tempNode = targetNode;
				targetNode = previousNode;
				previousNode = tempNode;
			}

			if (OverShotTarget ()) {

				currentNode = targetNode;

				transform.localPosition = currentNode.transform.position;

				GameObject otherPortal = GetPortal(currentNode.transform.position);    //prolazak kroz portal

				if (otherPortal != null) {

					transform.localPosition = otherPortal.transform.position;

					currentNode = otherPortal.GetComponent<Node> ();
				}

				Node moveToNode = CanMove (nextDirection);

				if (moveToNode != null)
					direction = nextDirection;

				if (moveToNode == null)
					moveToNode = CanMove (direction);

				if (moveToNode != null) {

					targetNode = moveToNode;
					previousNode = currentNode;
					currentNode = null;
				} else {

					direction = Vector2.zero;
				}

			} else {

				transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;    //kreće se neprekidno

			}
		}
	}
	void MoveToNode (Vector2 d){         //omogućava pacmanu kretanje po zvjezdicama
		Node moveToNode = CanMove (d);

		if (moveToNode != null) {
			transform.localPosition = moveToNode.transform.position;
			currentNode = moveToNode;
		}
			
	}

	void jedenjeZvjezdica () {                                //skupljanje zvjezdica
		GameObject o = GetTileAtPosition (transform.position);
		//Debug.Log (transform.position);
		//Debug.Log (o);

		if (o != null) {
			Tile tile = o.GetComponent<Tile> ();
//			Debug.Log (tile);
			if (tile != null) {
				if (!tile.didConsume && tile.Zvjezdica){
					o.GetComponent<SpriteRenderer> ().enabled = false;
					tile.didConsume = true;
					GameObject.Find ("Ploca").GetComponent<ploca> ().score += 1;
					pojedeneZvjezdice++;
				}
			}
		}
	}		


	void UpdateOrientation(){     //rotiranje lika ovisno o smjeru kretanja
		if (direction == Vector2.left) {
			transform.localScale = new Vector3 (-1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (direction == Vector2.right) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 0);
		} else if (direction == Vector2.up) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 90);
		} else if (direction == Vector2.down) {
			transform.localScale = new Vector3 (1, 1, 1);
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		}
	}

	Node CanMove (Vector2 d){     //kretanje lika od jednog nodea do drugog, ako je moguće
		Node moveToNode = null;

		for (int i = 0; i < currentNode.neighbors.Length; i++) {
			if (currentNode.validDirections [i] == d) {
				moveToNode = currentNode.neighbors [i];
				break;
			}
		}
		return moveToNode;
	}

	GameObject GetTileAtPosition (Vector2 pos) {      //traženje pozicija zvjezdica

		int tileX = Mathf.RoundToInt (pos.x);
		int tileY = Mathf.RoundToInt (pos.y);
		//Debug.Log (tileX);
		//Debug.Log (tileY);
		//Debug.Log (pos);


		GameObject tile = GameObject.Find ("Ploca").GetComponent<ploca> ().board [(int) pos.x, (int) pos.y];      //lista u kojoj su spremljeni svi objekti u labirintu

		if (tile != null)
			return tile;
		return null;
	}

	Node GetNodeAtPosition (Vector2 pos){       //uzimanje pozicija nodea
		
		GameObject tile = GameObject.Find ("Ploca").GetComponent<ploca> ().board [(int) pos.x, (int) pos.y];

		if (tile != null){
			return tile.GetComponent<Node> ();
		}

		return null;
	}

	bool OverShotTarget () {        //provjerava je li pacman došao do nodea, odnosno ciljne pozicije

		float nodeToTarget = LengthFromNode (targetNode.transform.position);   
		float nodeToSelf = LengthFromNode (transform.position);

		return nodeToSelf > nodeToTarget;
	}

	float LengthFromNode (Vector2 targetPosition) {

		Vector2 vec = targetPosition - (Vector2)previousNode.transform.position;
		return vec.sqrMagnitude;
	}

	GameObject GetPortal (Vector2 pos) {         //stvaranje portala

		GameObject tile = GameObject.Find ("Ploca").GetComponent<ploca> ().board [(int) pos.x, (int) pos.y];

		if (tile != null) {

			if (tile.GetComponent<Tile> () != null) {

				if (tile.GetComponent<Tile> ().Portal) {

					GameObject otherPortal = tile.GetComponent<Tile> ().PortalReceiver;

					return otherPortal;
				
				}
			}

		} return null;
	}
}
