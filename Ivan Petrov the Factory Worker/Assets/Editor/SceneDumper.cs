// SceneDumper.cs
//
// History:
// version 1.0 - December 2010 - Yossarian King

using UnityEngine;
using UnityEditor;
 
public static class SceneDumper
{
    [MenuItem("Debug/Dump Scene")]
    public static void DumpScene()
    {
        if ((Selection.gameObjects == null) || (Selection.gameObjects.Length == 0))
        {
            Debug.LogError("Please select the object(s) you wish to dump.");
            return;
        }
        Debug.Log("Dumping scene");
		foreach (GameObject gameObject in Selection.gameObjects)
		{
			DumpGameObject(gameObject, "");
		}
        Debug.Log("Scene dumped");
    }
 
    private static void DumpGameObject(GameObject gameObject, string indent)
    {
		if (gameObject.name.StartsWith("Wall") && gameObject.name != "Walls")
		Debug.Log(indent + (gameObject.name = gameObject.name.Substring(0, 4)));
        /*foreach (Component component in gameObject.GetComponents<Component>())
        {
            DumpComponent(component, indent + "  ");
        }*/
        foreach (Transform child in gameObject.transform)
        {
            DumpGameObject(child.gameObject, indent + "  ");
        }
    }
 
    private static void DumpComponent(Component component, string indent)
    {
		Debug.Log(indent + (component == null ? "(null)" : component.GetType().Name));
    }
}