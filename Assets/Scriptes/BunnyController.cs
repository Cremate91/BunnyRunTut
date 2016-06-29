using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

	public float bunnyJumpForce = 500f;

	private float bunnyHurtTime = -1f;
	private Rigidbody2D rb;
	private Animator myAnim;
	private Collider2D myCol;
	public Text scoreText;
	private float startTime;
	private int jumpsLeft = 2;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myCol = GetComponent<Collider2D> ();

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
		}

		if (bunnyHurtTime == -1) {
			if ((Input.GetButtonUp ("Jump") && jumpsLeft > 0) || (Input.GetButtonUp ("Fire1")  && jumpsLeft > 0)) {

				if (rb.velocity.y < 0) {
					rb.velocity = Vector2.zero;
				}
				if (jumpsLeft == 1) {
					rb.AddForce (transform.up * bunnyJumpForce * 0.75f);

				} else {
					
					rb.AddForce (transform.up * bunnyJumpForce);
				}
				jumpsLeft--;

				jumpSfx.Play ();

			}
			myAnim.SetFloat ("vVelocity", Mathf.Abs (rb.velocity.y));
			scoreText.text = (Time.time - startTime).ToString ("0.0");
		} else {
			if(Time.time > bunnyHurtTime + 2){
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			}
		}


	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {

			foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>()) {
				spawner.enabled = false;
			}
			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			bunnyHurtTime = Time.time;
			myAnim.SetBool ("bunnyHurt", true);
			rb.velocity = Vector2.zero;
			rb.AddForce (transform.up * bunnyJumpForce);
			myCol.enabled = false;

			deathSfx.Play ();
		}
		else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			jumpsLeft = 2;
		}
	}
}
