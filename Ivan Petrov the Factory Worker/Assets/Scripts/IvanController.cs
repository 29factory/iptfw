using UnityEngine;
using System.Collections;

public class IvanController : MonoBehaviour {
	public float speed = 20;

    void Awake () {
        GetComponent<Transform> ().position = GameObject.Find ("GameManagement").GetComponent<GameManagement> ().playOptions.appearAt;
    }

	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
		GetComponent<Animator> ().SetBool ("IsWalking", GetComponent<Rigidbody2D> ().velocity != new Vector2 (0f, 0f));
        if (Input.GetButtonDown ("Cancel"))
            GameObject.Find ("GameManagement").GetComponent<GameManagement> ().Save ();
	}
}
