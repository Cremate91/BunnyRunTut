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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myCol = GetComponent<Collider2D> ();

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (bunnyHurtTime == -1) {
			if (Input.GetButtonUp ("Jump")) {
				rb.AddForce (transform.up * bunnyJumpForce);

			}
			myAnim.SetFloat ("vVelocity", Mathf.Abs (rb.velocity.y));
			scoreText.text = (Time.time - startTime).ToString ("0.0");
		} else {
			if(Time.time > bunnyHurtTime + 2){
				SceneManager.LoadScene (0);
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
		}
	}
}
