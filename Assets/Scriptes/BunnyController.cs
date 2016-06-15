﻿using UnityEngine;
using System.Collections;

public class BunnyController : MonoBehaviour {

	public float bunnyJumpForce = 500f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Jump")) {
			rb.AddForce (transform.up * bunnyJumpForce);
		}
	}
}
