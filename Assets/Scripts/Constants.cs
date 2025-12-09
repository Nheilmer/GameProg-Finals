using UnityEngine;

public class Constants : MonoBehaviour
{

    public static void DebugWarning(GameObject gameObjName, string className, string text) {
        Debug.LogWarning($"[{gameObjName.name}] : [{className}] :> {text} -<?>-");
    }
}
