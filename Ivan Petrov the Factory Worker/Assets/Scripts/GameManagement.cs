using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {
    static string savePath = null;
    public static string saveExt = ".whyitshouldbesomethingshort";

	public static GameManagement instance = null;

	public PlayOptions playOptions = null;

    void Start () {
        savePath = Application.persistentDataPath +
                   Path.DirectorySeparatorChar +
                   "Saves";
    }

	void Awake () {
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		} else Destroy (gameObject);
	}

	public void Save () {
        playOptions.appearAt = GameObject.FindWithTag ("Player").GetComponent<Transform> ().position;
        playOptions.location = (Location) Enum.Parse (typeof(Location), SceneManager.GetActiveScene ().name);
        if (!Directory.Exists (savePath))
            Directory.CreateDirectory (savePath);
        {
            FileStream saveFile = File.Open (Path.Combine (savePath, playOptions.regime.ToString () + saveExt), FileMode.OpenOrCreate, FileAccess.Write);
            new BinaryFormatter ().Serialize (saveFile, playOptions);
            saveFile.Close ();
        }
	}

    public void Load (Regime r) {
        Load (r.ToString ());
    }

    public void Load(string r) {
        if (File.Exists (Path.Combine (savePath, r + saveExt))) {
            FileStream saveFile = File.Open (Path.Combine (savePath, r + saveExt), FileMode.Open, FileAccess.Read);
            playOptions = (PlayOptions)new BinaryFormatter ().Deserialize (saveFile);
            saveFile.Close ();
        } else
            playOptions = new PlayOptions ((Regime) Enum.Parse(typeof(Regime), r));
    }

    public void Play(string r) {
        Load (r);
        SceneManager.LoadScene (playOptions.location.ToString());
    }
}
