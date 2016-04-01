using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {
	public void LoadScene (string s) {
		SceneManager.LoadScene (s);
	}
}
