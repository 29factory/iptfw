using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {
	public string doorTo;
    public float x, y;

	void OnCollisionEnter2D (Collision2D c) {
        GameObject.Find ("GameManagement").GetComponent<GameManagement> ().playOptions.appearAt = new Vector3 (x, y, 0);
		SceneManager.LoadScene (doorTo);
	}
}
