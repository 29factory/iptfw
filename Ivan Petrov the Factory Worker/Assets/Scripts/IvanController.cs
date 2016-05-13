using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IvanController : MonoBehaviour {
	public float speed;

    void Awake () {
        if (GameManagement.gameData != null)
            GetComponent<Transform> ().position = GameManagement.gameData.betweener.appearAt;
        else
            GameManagement.CreateGameData ((Location) System.Enum.Parse(typeof(Location), SceneManager.GetActiveScene().name), GetComponent<Transform> ().position);
    }

	void FixedUpdate () {
        GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
		GetComponent<Animator> ().SetBool ("IsWalking", GetComponent<Rigidbody2D> ().velocity != new Vector2 (0f, 0f));
        if (Input.GetButtonDown ("Cancel")) {
            GameManagement.Save ((Location) System.Enum.Parse(typeof(Location), SceneManager.GetActiveScene().name), GetComponent<Transform> ().position);
            SceneManager.LoadScene ("Pause");
        }
	}
}
