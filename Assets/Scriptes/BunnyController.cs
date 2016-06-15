using UnityEngine;
using System.Collections;

public class BunnyController : MonoBehaviour {

	public float bunnyJumpForce = 500f;

	private Rigidbody2D rb;
	private Animator myAnim;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Jump")) {
			rb.AddForce (transform.up * bunnyJumpForce);

		}
		myAnim.SetFloat ("vVelocity", Mathf.Abs(rb.velocity.y));
	}
}
