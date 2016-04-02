using UnityEngine;
using UnityEngine.SceneManagement;

public class RegimeSelector : MonoBehaviour {
    public void Select(string r) {
        GameManagement.Load (r);
        SceneManager.LoadScene (GameManagement.gameData.betweener.location.ToString ());
    }
}
