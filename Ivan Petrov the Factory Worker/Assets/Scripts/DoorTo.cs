using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {
	public string doorTo;
    public Vector2 appearAt;

    void OnTriggerEnter2D () {
        GameManagement.gameData.betweener.appearAt = appearAt;
		SceneManager.LoadScene (doorTo);
	}
}
