using Assets.Item1;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor
{
    public class StageEditor : EditorWindow
    {
        private const string stageInfoDirectoryPath = "Assets/Making/Stage/";
        private const string testObjectName = "StageTestRoot";

        // Window를 띄우기 위한 메뉴를 추가한다.
        [MenuItem("MyTools/StageEditor")]
        public static void OpenStageEditor()
        {
            //Window를 생성하는 기능.
            GetWindow<StageEditor>();
        }


        // 현재 선택한 스테이지 정보.
        private StageInfo currentStageInfo;
        // 프로젝트에 있는 모든 스테이지 정보들. 이게 있어야 툴에서 스테이지들을 보여줄 수 있다.
        private List<StageInfo> stageInfos = null;

        private GUIStyle titleStyle;
        private GameObject testObjectRoot;

        private void CreateGUI()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/StageEditorScene.unity");
        }

        private void InitTestScene()
        {
            if (testObjectRoot == null)
            {
                testObjectRoot = GameObject.Find(testObjectName);
                if (testObjectRoot == null)
                    new GameObject(testObjectName);
            }
        }

        private void OnGUI()
        {
            // 코드가 호출된 순서대로 UI가 위에서부터 아래로 그려진다.

            InitTestScene();
            if (titleStyle == null)
            {
                titleStyle = new GUIStyle(GUI.skin.label);
                titleStyle.fontSize = 20;
            }
            
            GUILayout.Label("StageEditor", titleStyle); // titleStyle을 써서 글씨가 크게 나온다.

            DrawStageList();

            GUILayout.Space(10);

            ++EditorGUI.indentLevel;
            DrawCurrentStage();

            --EditorGUI.indentLevel;

            

            GUILayout.Space(10);

            if (GUILayout.Button("Save"))
            {

            }

            NewStageMenu();
        }

        private void DrawStageList()
        {
            if (stageInfos == null)
                stageInfos = GetStageList();

            GUILayout.BeginHorizontal();

            foreach (StageInfo stageInfo in stageInfos)
            {
                bool isThisSelected = stageInfo == currentStageInfo;
                Color originalColor = GUI.backgroundColor;
                GUI.backgroundColor = isThisSelected ? Color.green : originalColor;
                if (GUILayout.Button($"{stageInfo.name} : {stageInfo.StageNumber}")) // 스테이지 버튼
                {
                    currentStageInfo = stageInfo;
                    RefreshStagePreview();
                }
                GUI.backgroundColor = originalColor;
            }

            GUILayout.EndHorizontal();
        }

        private List<StageInfo> GetStageList()
        {
            // Find all assets labelled with 'architecture' :
            string[] guids1 = AssetDatabase.FindAssets(filter: null, new string[] { stageInfoDirectoryPath });
            var result = new List<StageInfo>();
            foreach (string guid1 in guids1)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid1);
                var stageInfo = AssetDatabase.LoadAssetAtPath<StageInfo>(path);
                if (stageInfo == null)
                    continue;

                result.Add(stageInfo);
            }

            return result;
        }

        private void DrawCurrentStage()
        {
            if (currentStageInfo == null)
                return;

            GUILayout.Label("Spawn", titleStyle);
            for (int i = 0; i < currentStageInfo.monsterSpawnInfos.Count; ++i)
            {
                GUILayout.BeginHorizontal();
                using (var changeCheckScope = new EditorGUI.ChangeCheckScope())
                {
                    MonsterSpawnInfo spawnInfo = currentStageInfo.monsterSpawnInfos[i];

                    spawnInfo.prefab = EditorGUILayout.ObjectField(spawnInfo.prefab, typeof(GameObject), allowSceneObjects: false) as GameObject;
                    spawnInfo.position = EditorGUILayout.Vector3Field("Position", spawnInfo.position);
                    if (GUILayout.Button("Delete"))
                    {
                        currentStageInfo.monsterSpawnInfos.RemoveAt(i);
                        --i;
                    }

                    if (changeCheckScope.changed)
                    {
                        RefreshStagePreview();
                    }
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add"))
            {
                var lastOne = currentStageInfo.monsterSpawnInfos.LastOrDefault();
                var newSpawnInfo = new MonsterSpawnInfo();
                if (lastOne != null)
                {
                    newSpawnInfo.prefab = lastOne.prefab;
                    newSpawnInfo.position = lastOne.position + new Vector3(1, 0, 0);
                }

                currentStageInfo.monsterSpawnInfos.Add(newSpawnInfo);
                RefreshStagePreview();
            }
            GUILayout.EndHorizontal();
        }

        private void NewStageMenu()
        {
            if (GUILayout.Button("NewStage"))
            {
                // 새로운 스테이지를 생성한다.
                var newStageInfo = ScriptableObject.CreateInstance<StageInfo>();

                string path = stageInfoDirectoryPath + "StageInfo.asset";

                // 이름이 중복이 안되게 고유한 이름을 얻어낸다.
                path = AssetDatabase.GenerateUniqueAssetPath(path);

                // 스테이지를 저장한다.
                AssetDatabase.CreateAsset(newStageInfo, path);
                currentStageInfo = newStageInfo;
                RefreshStagePreview();

                stageInfos.Add(currentStageInfo);
            }
        }

        // 스테이지 정보에 들어있는 몬스터 소환 정보를 씬에 출력한다.
        private void RefreshStagePreview()
        {
            // 이미 소환되어 있는 몬스터를 모두 파괴한다.
            while (testObjectRoot.transform.childCount > 0)
            {
                DestroyImmediate(testObjectRoot.transform.GetChild(0).gameObject);
            }

            if (currentStageInfo == null)
                return;

            // 현재 스테이지 정보의 몬스터 소환 정보를 바탕으로 몬스터를 소환한다.
            foreach (var spawnInfo in currentStageInfo.monsterSpawnInfos)
            {
                GameObject spawnedMonster = PrefabUtility.InstantiatePrefab(spawnInfo.prefab, testObjectRoot.transform) as GameObject;
                spawnedMonster.transform.position = spawnInfo.position;
            }
        }
    }
}