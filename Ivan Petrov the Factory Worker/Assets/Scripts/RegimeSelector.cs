using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class RegimeSelector : MonoBehaviour {
    public void Select (string r) {
        GameManagement.Load (r);
        SceneManager.LoadScene (GameManagement.gameData.betweener.location.ToString ());
    }

    public void Delete(string r) {
        GameManagement.Delete (r);
        GameObject.Find ("/Canvas/paper/" + r + "Delete").GetComponent<Button> ().interactable = false;
    }

    void Awake () {
        foreach (Regime r in Enum.GetValues(typeof(Regime)))
            if (File.Exists (Path.Combine (Globals.savingPath, r.ToString () + Globals.savingExt)))
                GameObject.Find ("/Canvas/paper/" + r.ToString () + "Delete").GetComponent<Button> ().interactable = true;
    }
}
