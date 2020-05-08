using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class jedenjePacmana : MonoBehaviour {

	public GameObject pacm;
	public GameObject duhc;
	public GameObject duhsp;
	public GameObject duhlj;
	public GameObject duhz;

	public Vector3 pac;
	public Vector3 duhcr;
	public Vector3 duhspl;
	public Vector3 duhljub;
	public Vector3 duhzel;

	public TextMeshProUGUI GameOvertext;
//	public int broj = 5;
	public GameObject spojnica;
	// Use this for initialization
	void Start () {

//		spojnica.SetActive (false);
//
//		brojac ();

		GameOvertext.gameObject.SetActive (false);
		pacm.SetActive (true);

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pac = pacm.transform.position;
		Vector3 duhcr = duhc.transform.position;
		Vector3 duhspl = duhsp.transform.position;
		Vector3 duhljub = duhlj.transform.position;
		Vector3 duhzel = duhz.transform.position;

		if (Mathf.Round(pac.x) == Mathf.Round(duhcr.x) && Mathf.Round(pac.y) == Mathf.Round(duhcr.y)) {    //provjerava jesu li pacman i duh na istoj poziciji

			Debug.Log ("GameOver");

			StartCoroutine (GameOver ());            //ako jesu, javlja kraj igre

		}

		else if (Mathf.Round(pac.x) == Mathf.Round(duhspl.x) && Mathf.Round(pac.y) == Mathf.Round(duhspl.y)) {

			Debug.Log ("GameOver");

			StartCoroutine (GameOver ());
		}

		else if (Mathf.Round(pac.x) == Mathf.Round(duhljub.x) && Mathf.Round(pac.y) == Mathf.Round(duhljub.y)) {

			Debug.Log ("GameOver");

			StartCoroutine (GameOver ());
		}

		else if (Mathf.Round(pac.x) == Mathf.Round(duhzel.x) && Mathf.Round(pac.y) == Mathf.Round(duhzel.y)) {

			Debug.Log ("GameOver");

			StartCoroutine (GameOver ());
		}
//		Debug.Log (Mathf.Round (pac.x));
//		Debug.Log (Mathf.Round (pac.y));
//		Debug.Log (Mathf.Round (duh.x));
//		Debug.Log (Mathf.Round (duh.y));

		
	}

	IEnumerator GameOver () {        //ispisivanje "Game over" na ekranu nakon dodira pacmana i duha

		GameOvertext.text = "Game Over";
		pacm.SetActive (false);            //pacman je pojeden
		GameOvertext.gameObject.SetActive (true);
		yield return new WaitForSeconds (5f);
		GameOvertext.gameObject.SetActive (false);
		SceneManager.LoadScene ("Menu");
	}

//	IEnumerator brojac () {            //provjeravanje dodira duhova i pacmana se pokrece nakon 5. sekunde igre
//
//		while (broj > 0) {
//
//			yield return new WaitForSeconds (1f);
//			broj--;
//		}
//
//		yield return new WaitForSeconds (1f);
//		spojnica.SetActive (true);
//	}
}
