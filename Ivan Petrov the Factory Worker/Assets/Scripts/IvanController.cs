using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class IvanController : MonoBehaviour {
	public float speed;

    void Awake () {
        if (GameManagement.gameData != null)
            GetComponent<Transform> ().position = GameManagement.gameData.betweener.appearAt;
        else
            GameManagement.CreateGameData ((Location) System.Enum.Parse(typeof(Location), SceneManager.GetActiveScene().name), GetComponent<Transform> ().position);

        InvokeRepeating ("CalculateStats", 0, 1);
    }

	void FixedUpdate () {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
		GetComponent<Animator> ().SetBool ("IsWalking", GetComponent<Rigidbody2D> ().velocity != new Vector2 (0f, 0f));
        if (Input.GetButtonDown ("Cancel")) {
            GameManagement.Save ((Location) System.Enum.Parse(typeof(Location), SceneManager.GetActiveScene().name), GetComponent<Transform> ().position);
            SceneManager.LoadScene ("Pause");
        }
	}

    void CalculateStats () {
        foreach (int s in Enum.GetValues(typeof(Stat)))
            GameManagement.gameData.oldStats [s] = GameManagement.gameData.stats [s];

        foreach (Stat s in Enum.GetValues(typeof(Stat))) {
            if (Globals.statEffectGraph.ContainsKey (s))
            if (Globals.statEffectGraph [s].ContainsKey (GameData.GetCondition (GameManagement.gameData.oldStats [(int)s])))
                foreach (var p in Globals.statEffectGraph[s][GameData.GetCondition(GameManagement.gameData.oldStats[(int) s])]) {
                    GameManagement.gameData.stats [(int) p.Key] = p.Value (GameManagement.gameData.stats [(int) p.Key]);
                }
        }
        
        foreach (int s in Enum.GetValues(typeof(Stat)))
            GameManagement.gameData.stats [s] -= .2f;

        foreach (Stat s in Enum.GetValues(typeof(Stat)))
            GameObject.Find (s.ToString ()).transform.GetChild (0).GetComponent<Slider> ().value = GameManagement.gameData.stats [(int) s];
    }
}
