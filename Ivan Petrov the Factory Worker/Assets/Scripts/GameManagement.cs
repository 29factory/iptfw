using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class GameManagement {
    public static GameData gameData {
        get;
        private set;
    }

    public static void Save (Location location, VectorData position) {
        gameData.betweener.appearAt = position;
        gameData.betweener.location = location;
        if (!Directory.Exists (Globals.savingPath))
            Directory.CreateDirectory (Globals.savingPath);
        {
            FileStream savingFile = File.Open (Path.Combine (Globals.savingPath, gameData.regime.ToString () + Globals.savingExt), FileMode.OpenOrCreate, FileAccess.Write);
            new BinaryFormatter ().Serialize (savingFile, gameData);
            savingFile.Close ();
        }
	}

    public static void Load (Regime r) {
        Load (r.ToString ());
    }

    public static void Load (string r) {
        if (File.Exists (Path.Combine (Globals.savingPath, r + Globals.savingExt))) {
            FileStream loadingFile = File.Open (Path.Combine (Globals.savingPath, r + Globals.savingExt), FileMode.Open, FileAccess.Read);
            gameData = (GameData) new BinaryFormatter ().Deserialize (loadingFile);
            loadingFile.Close ();
        } else
            gameData = GameData.Create (new UnityEngine.Vector3 (0, 0), (Regime) System.Enum.Parse(typeof(Regime), r));
    }

    public static void CreateGameData (Location l, VectorData p, Regime r = Regime.Zheleznov) {
        gameData = GameData.Create (p, r, l);
    }

    public static void Delete (string r) {
        File.Delete (Path.Combine (Globals.savingPath, r + Globals.savingExt));
    }
}
