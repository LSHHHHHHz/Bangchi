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
        private const string stageInfoDirectoryPath = "Assets/Making/Stage/"; //경로 설정
        private const string testObjectName = "StageTestRoot"; //게임오브젝트 이름 설정

        // Window를 띄우기 위한 메뉴를 추가한다.
        [MenuItem("MyTools/StageEditor")]
        public static void OpenStageEditor() //StageEditor을 호출함
        {
            //Window를 생성하는 기능.
            GetWindow<StageEditor> (); //StageEditor 클래스의 인스턴스를 가져옴 -> 그 이후 OnGUI()가 호출됨
        }


        // 현재 선택한 스테이지 정보.
        private StageInfo currentStageInfo;
        // 프로젝트에 있는 모든 스테이지 정보들. 이게 있어야 툴에서 스테이지들을 보여줄 수 있다.
        private List<StageInfo> stageInfos = null;

        private GUIStyle titleStyle;
        private GameObject testObjectRoot;

        private void CreateGUI()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/StageEditorScene.unity"); //GUI가 실행되면 해당 경로로 Scene를 생성함
        }

        private void InitTestScene()
        {
            if (testObjectRoot == null) //testObjectRoot이 비었다면 if문 실행(처음에 실행되는 것 같음)
            {
                testObjectRoot = GameObject.Find(testObjectName); //GameObject에서 testObjectName이름을 찾아서 testObjectRoot에 넣음
                                                                  //맨 처음에 'StageTestRoot' 이 이름을 갖은 GameObject가 없는데 어떻게 testObjectRoot에 값을 넣는거지?
                                                                  
                if (testObjectRoot == null) //testObjectRoot가 비었다면 testObjectName을 갖은 게임 오브젝트를 생성함!!
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


            //★EditorGUI.indentLevel이 뭐지? --를 지워도 똑같음
            ++EditorGUI.indentLevel;
            DrawCurrentStage();

            --EditorGUI.indentLevel;

            

            GUILayout.Space(10);

            if (GUILayout.Button("Save"))
            {

            }

            NewStageMenu();
        }

        private void DrawStageList() // 버튼을 클릭했는지 안했는지 확인 시켜주는 매서드 (currentStageInfo이게 선택되는 최초의 매서드)
        {
            if (stageInfos == null)
                stageInfos = GetStageList(); // StageInfo 에셋들을 stageInfos에 넣음

            GUILayout.BeginHorizontal(); //위에 있는 것들을 수평으로 GUI에 보이게 해줌

            foreach (StageInfo stageInfo in stageInfos)
            {
                bool isThisSelected = stageInfo == currentStageInfo; //현재 선택한 Stage정보가 stageInfo와 같다면 ture
                Color originalColor = GUI.backgroundColor; // GUI의 배경색을 originalColor에 저장
                GUI.backgroundColor = isThisSelected ? Color.green : originalColor; //backgroundColor은 선택이 됐다면 초록, 안됐다면 orignalColor
                if (GUILayout.Button($"{stageInfo.name} : {stageInfo.StageNumber}")) // stageInfo의 이름과 숫자를 생성하는 버튼을 생성
                {                                                                    // 이 버튼을 누르면 ture 안누르면 false 이게 맞나? 생성도하고 true인지 아닌지 확인도하나???
                    currentStageInfo = stageInfo;
                    RefreshStagePreview();
                }
                GUI.backgroundColor = originalColor;
            }

            GUILayout.EndHorizontal(); // GUILayout.BeginHorizontal();가 있다면 반드시 해야함
        }

        private List<StageInfo> GetStageList() //이건 StagelInfo 에셋을 불러오는 함수라 생각하면 됨
        {
            // Find all assets labelled with 'architecture' :
            string[] guids1 = AssetDatabase.FindAssets(filter: null, new string[] { stageInfoDirectoryPath }); // filter: null <-- 모든 에셋 검색!!, new string[] { stageInfoDirectoryPath }<-- 검색할 폴더의 경로 지정
            var result = new List<StageInfo>();
            foreach (string guid1 in guids1)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid1); //guid1을 GUID로 파일 경로 변경 후 path에 저장 ★ A to B 라 했을때 여긴 B에서 A로 읽으면 되나??
                var stageInfo = AssetDatabase.LoadAssetAtPath<StageInfo>(path); //경로에서 path를 불러온 후 StageInfo타입으로 stageInfo에 넣음
                if (stageInfo == null) //stageInfo가 null이 아닐 때 result에 stageInfo를 넣음
                    continue;

                result.Add(stageInfo);
            }

            return result;
        }

        private void DrawCurrentStage()
        {
            if (currentStageInfo == null) //currentStageInfo가 비어있으면 리턴, currnentStageInfo가 null이 아니려면 버튼이 클릭 되어야함!
                return;

            GUILayout.Label("Spawn", titleStyle); //null이 아니면 Spawn이라는 라벨이 나옴
            for (int i = 0; i < currentStageInfo.monsterSpawnInfos.Count; ++i)
            {
                GUILayout.BeginHorizontal();
                using (var changeCheckScope = new EditorGUI.ChangeCheckScope())  //★이게 뭐지?
                {
                    //이 사이는 prefab이나 position을 설정할 수 있게 만들어 줌//
                    MonsterSpawnInfo spawnInfo = currentStageInfo.monsterSpawnInfos[i];

                    spawnInfo.prefab = EditorGUILayout.ObjectField(spawnInfo.prefab, typeof(GameObject), allowSceneObjects: false) as GameObject;
                    spawnInfo.position = EditorGUILayout.Vector3Field("Position", spawnInfo.position);

                    //이 사이는 prefab이나 position을 설정할 수 있게 만들어 줌//


                    if (GUILayout.Button("Delete"))
                    {
                        currentStageInfo.monsterSpawnInfos.RemoveAt(i); //Delete를 누르면 monsterSpawnInfos가 삭제됨
                        --i;
                    }

                    if (changeCheckScope.changed)
                    {
                        RefreshStagePreview();
                    }
                }
                GUILayout.EndHorizontal(); //이게 없으니 save랑 new stage가 없어짐
                                           //GUILayout.BeginHorizontal()을 쓰면 반드시 GUILayout.EndHorizontal() 써야함.
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
        private void RefreshStagePreview() //★이부분 잘 모르겠음//
        {
            // 이미 소환되어 있는 몬스터를 모두 파괴한다.
            while (testObjectRoot.transform.childCount > 0) //testObjectRoot의 자식 개수가 0보다 크면 계속 실행
            {
                DestroyImmediate(testObjectRoot.transform.GetChild(0).gameObject); //계속 파괴
            }

            if (currentStageInfo == null) //currentsStageInfo는 버튼을 누르면 null이 아니게 됨 (DrawStageList에서 누름)?
                return;

            // 현재 스테이지 정보의 몬스터 소환 정보를 바탕으로 몬스터를 소환한다.
            foreach (var spawnInfo in currentStageInfo.monsterSpawnInfos)
            {
                GameObject spawnedMonster = PrefabUtility.InstantiatePrefab(spawnInfo.prefab, testObjectRoot.transform) as GameObject;
                                                          //Instantiate (spawnInfo.prefab, testObjectRoot.transform) 무슨 차이지..?
                spawnedMonster.transform.position = spawnInfo.position;
            }
        }
    }
}