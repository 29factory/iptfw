using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {
	public string doorTo;

	void OnCollisionEnter2D (Collision2D c) {
		SceneManager.LoadScene (doorTo);
	}
}
