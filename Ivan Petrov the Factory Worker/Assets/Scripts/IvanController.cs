using UnityEngine;
using System.Collections;

public class IvanController : MonoBehaviour {
	public float speed = 16;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
	}
}
