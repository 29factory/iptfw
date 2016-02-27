using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {

	public string doorTo = "Home";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D c) {
		SceneManager.LoadScene (doorTo);
	}
}
