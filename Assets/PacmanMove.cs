using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour {
	public Rigidbody2D rb;
	public float brzina = 0f;
	private bool m_FacingRight = true;
	private bool m_FacingUp = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 x = new Vector3(Input.GetAxis ("Horizontal") * brzina * Time.deltaTime, Input.GetAxis ("Vertical") * brzina * Time.deltaTime, 0);
		rb.velocity = x;

		if (Input.GetAxis ("Vertical") > 0 && !m_FacingUp) {
			FlipUp ();
		} else if (Input.GetAxis ("Vertical") < 0 && m_FacingUp) {
			FlipUp ();
		}

	
		if (Input.GetAxis ("Horizontal") > 0 && !m_FacingRight) {
			Flip ();
		}
	
		else if (Input.GetAxis("Horizontal") < 0 && m_FacingRight)
		{
			Flip();
		}

	}
	private void Flip()
	{
		m_FacingRight = !m_FacingRight;

		Vector3 y = transform.localScale;
		y.x *= -1;
		transform.localScale = y;
	}

	private void FlipUp()
	{
		m_FacingUp = !m_FacingUp;

		Vector3 x = transform.localScale;
		x.z *= -0.5f;
		transform.localScale = x;
	}
}
