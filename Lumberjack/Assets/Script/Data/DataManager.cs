
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    private static GameObject container;
    private static DataManager instance; //싱글톤으로 선언
    private string GameDataFileName = "GameData.json"; //게임 데이터 파일 이름을 설정
    public GameData data = new GameData(); //저장용 클래스 변수

    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    //불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        if (File.Exists(filePath)) //저장 파일이 존재한다면
        {
            //저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당한다
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
            print("저장 파일 불러오기 완료!");
        }
    }

    //저장하기
    public void SaveGameData()
    {
        //클래스를 Json 형식으로 전환 (가독성 좋도록!)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        //이미 저장된 파일이 있다면 덮어쓰고, 없다면 새롭게 만들어서 저장한다
        File.WriteAllText(filePath, ToJsonData);
        //올바르게 저장되었는지 확인해준다
        print("데이터 저장 완료");

        for (int i = 0; i < data.skillActive.Count; i++)
        {
            print($"{i} 번째 유닛 오브젝트 버튼 잠금 해제 여부 : " + data.skillActive[i]);
        }

    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}
