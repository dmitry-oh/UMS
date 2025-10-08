/*
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnityMS))]

public class UnityMS_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        UnityMS data = (UnityMS)target;

        // 일반 필드
        data.characterName = EditorGUILayout.TextField("Name", data.characterName);
        data.health = EditorGUILayout.IntField("Health", data.health);

        // Sprite 크게 보여주기
        EditorGUILayout.LabelField("Character Sprite", EditorStyles.boldLabel);

        data.characterSprite = (Sprite)EditorGUILayout.ObjectField(
            "Sprite",
            data.characterSprite,
            typeof(Sprite),
            allowSceneObjects: false
        );

        if (data.characterSprite != null)
        {
            // Sprite Preview
            Rect rect = GUILayoutUtility.GetRect(100, 300, 100, 300); // 가로/세로 최대 300px
            EditorGUI.DrawPreviewTexture(rect, data.characterSprite.texture);
        }

        // 변경사항 저장
        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }
}
*/