using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(FieldOfView))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = fow._color;

        var position = fow.transform.position;

        Handles.DrawWireArc(position, fow.normal, fow.from, 360, fow.viewRadius);

        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(position, position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(position, position + viewAngleB * fow.viewRadius);
    }
}
#endif