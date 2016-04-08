using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void LoadScene (string s) {
        SceneManager.LoadScene (s);
    }

    public void Exit () {
        Application.Quit ();
    }

    public void Continue () {
        SceneManager.LoadScene (GameManagement.gameData.betweener.location.ToString ());
    }
}
