using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(HearingSystem))]
public class HearingRangeEditor : Editor
{
    private void OnSceneGUI()
    {
        HearingSystem hearingSystem = (HearingSystem)target;
        Handles.color = hearingSystem.color;

        var position = hearingSystem.transform.position;

        Handles.DrawWireArc(position, hearingSystem.normal, hearingSystem.from, 360, hearingSystem.hearingRange);
    }
}
#endif