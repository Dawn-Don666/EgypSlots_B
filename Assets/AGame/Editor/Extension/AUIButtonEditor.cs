using UnityEditor;

[CustomEditor(typeof(AUIButton))]
public class AUIButtonEditor : UnityEditor.UI.ButtonEditor
{
    AUIButton uiButton;

    protected override void OnEnable()
    {
        base.OnEnable();
        uiButton = (AUIButton)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        serializedObject.Update();

        uiButton.ScaleAnim = EditorGUILayout.Toggle("ScaleAnim", uiButton.ScaleAnim);
        uiButton.Sound = EditorGUILayout.Toggle("Sound", uiButton.Sound);
        
        serializedObject.ApplyModifiedProperties();
    }
}