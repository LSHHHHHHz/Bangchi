using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class StageEditor : EditorWindow
    {
        [MenuItem("MyTools/StageEditor")]
        public static void OpenStageEditor()
        {
            GetWindow<StageEditor>();
        }

        private int stageId;
        private List<GameObject> monsterList = new();
        private void OnGUI()
        {
            GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 20;
            GUILayout.Label("StageEditor", titleStyle);
            stageId = EditorGUILayout.IntField("StageId", stageId);

            GUILayout.Space(10);

            ++EditorGUI.indentLevel;
            for (int i = 0; i < monsterList.Count; ++i)
            {
                GameObject monsterPrefab = monsterList[i];
                monsterPrefab = EditorGUILayout.ObjectField(monsterPrefab, typeof(GameObject), allowSceneObjects: false) as GameObject;
                monsterList[i] = monsterPrefab;
            }
            --EditorGUI.indentLevel;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                monsterList.Add(null);
            }

            if (GUILayout.Button("Remove"))
            {
                if (monsterList.Count > 0)
                {
                    monsterList.RemoveAt(monsterList.Count - 1);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            if (GUILayout.Button("Save"))
            {

            }
        }
    }
}