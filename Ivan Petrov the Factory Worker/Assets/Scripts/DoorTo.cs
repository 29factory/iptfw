using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTo : MonoBehaviour {
	public string doorTo;
    public Vector2 appearAt;

    void OnTriggerEnter2D (Collider2D c) {
        GameManagement.gameData.betweener.appearAt = appearAt;
        GameManagement.gameData.betweener.direction = new VectorData (c.GetComponent<Animator> ().GetFloat ("DirectionX"), c.GetComponent<Animator> ().GetFloat ("DirectionY"));
        SceneManager.LoadScene (doorTo);
	}
}
