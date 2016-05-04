using UnityEngine;
using System.Collections;

public class IvanController : MonoBehaviour {
    public float speed;

	void FixedUpdate () {
        GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
		GetComponent<Animator> ().SetBool ("IsWalking", GetComponent<Rigidbody2D> ().velocity != new Vector2 (0f, 0f));
	}
}
