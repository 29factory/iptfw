using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {
	public string doorTo;
    public float x, y;

	void OnCollisionEnter2D (Collision2D c) {
        GameManagement.gameData.betweener.appearAt = new Vector3 (x, y, 0);
		SceneManager.LoadScene (doorTo);
	}
}
