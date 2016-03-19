using UnityEngine;
using System.Collections;

public class IvanController : MonoBehaviour {
	public float speed = 20;

	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
		GetComponent<Animator> ().SetBool ("IsWalking", GetComponent<Rigidbody2D> ().velocity != new Vector2 (0f, 0f));
	}
}
