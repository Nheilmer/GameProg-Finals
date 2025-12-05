using System.Collections.Generic;
using UnityEngine;

public class PathList : MonoBehaviour
{
    [SerializeField] private List<Transform> paths;
    [SerializeField] private Color gizmoColor;
    [SerializeField] private bool autoFill = false;
    [SerializeField] private bool drawGizmo = false;

    private void Start() {
        if (autoFill) initAutoFill();
    }

    public void initAutoFill() {
        paths.Add(gameObject.transform);
        foreach (Transform t in gameObject.transform) {
            paths.Add(t);
        }
    }

    public List<Transform> getPathList() {
        return paths;
    }

    // Can be removed
    public void OnDrawGizmos() {
        if (!drawGizmo || paths == null || paths.Count < 2)
            return;

        Gizmos.color = gizmoColor;

        for (int i = 0; i < paths.Count - 1; i++) {
            Gizmos.DrawLine(paths[i].position, paths[i + 1].position);
        }
    }

}
