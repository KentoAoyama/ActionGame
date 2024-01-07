using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "ScenarioDataListSO", menuName = "ScriptableObject/ScenarioDataListSO")]
public class ScenarioDataSOList : ScriptableObject
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";

    // ScenarioDataを格納するDictionary
    private Dictionary<string, ScenarioDataSO> _scenarioDataDictionary;
    public Dictionary<string, ScenarioDataSO> ScenarioDataDictionary => _scenarioDataDictionary;

    // Inspectorに表示するシナリオデータのリスト
    [SerializeField, ReadOnly]
    private List<string> _inspectorViewScenarioList;

    // このScriptableObjectが有効になったときに呼び出される
    private void Awake()
    {
        SetScenarioData();
    }

    /// <summary>
    /// シナリオデータを格納する
    /// </summary>
    public void SetScenarioData()
    {
        // ScenarioFileNameSOをResourcesフォルダから取得
        ScenarioFileNameSO scenarioFileNameSO =
            Resources.Load<ScenarioFileNameSO>("ScriptableObject/" + SCENARIOFILE_NAME);

        if (scenarioFileNameSO.FileNames == null) return;

        // シナリオデータを格納するDictionaryを初期化
        if (_scenarioDataDictionary == null)
        {
            _scenarioDataDictionary = new Dictionary<string, ScenarioDataSO>();
        }
        else
        {
            _scenarioDataDictionary.Clear();
        }

        // Inspectorに表示するシナリオデータのリストを初期化
        if (_inspectorViewScenarioList == null)
        {
            _inspectorViewScenarioList = new List<string>();
        }
        else
        {
            _inspectorViewScenarioList.Clear();
        }

        foreach (string fileName in scenarioFileNameSO.FileNames)
        {
            // ResourcesフォルダからCSVデータを取得
            TextAsset textAsset = Resources.Load<TextAsset>("TextData/" + fileName);

            if (textAsset == null)
                throw new System.Exception("CSVファイルが存在しません。");

            StringReader reader = new(textAsset.text);
            // StringReaderからScenarioDataを作成
            List<SpeakData> scenarioData = new(GetSpeakDataList(reader));
            // ScriptableObjectのScenarioDataSOを作成
            var scenarioDataSO = CreateScenarioDataSO(fileName, scenarioData);

            // Dictionaryに格納
            _scenarioDataDictionary.Add(fileName, scenarioDataSO);

            _inspectorViewScenarioList.Add(fileName);
        }

        Debug.Log("全てのシナリオデータの格納終了");
    }

    private ScenarioDataSO CreateScenarioDataSO(string fileName, List<SpeakData> scenarioData)
    {
        ScenarioDataSO scenarioDataSO = CreateInstance<ScenarioDataSO>();
        scenarioDataSO.Initialized(fileName, scenarioData);

        var path = "Assets/Game/Resources/ScriptableObject/ScenarioData";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        AssetDatabase.CreateAsset(scenarioDataSO, Path.Combine(path, $"{fileName}.asset"));

        Debug.Log($"シナリオデータを格納しました\n生成場所：{path}/{fileName}");

        return scenarioDataSO;
    }

    // StringReaderを受け取り、SpeakDataのリストを返す
    private List<SpeakData> GetSpeakDataList(StringReader reader)
    {
        // 発言データを格納するリスト
        List<SpeakData> speakDataList = new();

        for (int i = 0; i < 4; i++)
        {
            // ヘッダー行を読み飛ばす
            reader.ReadLine();
        }

        while (reader.Peek() != -1)
        {
            var data = reader.ReadLine().Split(',');

            // CSVデータをSpeakDataに変換してリストに格納
            SpeakData speakData
                = new(
                    int.Parse(data[0]),          // テキストの場所番号
                    data[1],                     // キャラクター名
                    data[2],                     // 発言内容
                    float.Parse(data[4]),        // 表示時間 TODO:ローカライズ対応をする
                    ParseToEventCodeType(data)); // イベントコード

            // リストに格納
            speakDataList.Add(speakData);
        }

        return speakDataList;
    }

    private EventCodeType[] ParseToEventCodeType(string[] data)
    {
        int eventCount = data.Length - 5;

        EventCodeType[] eventCodeTypes = new EventCodeType[eventCount];

        // イベントコードの文字列をEventCodeTypeに変換
        for (int i = 0; i < eventCount; i++)
        {
            switch (data[5 + i])
            {
                case "":
                    eventCodeTypes[i] = EventCodeType.None;
                    break;
                default:
                    throw new System.Exception("イベントコードが不正です");
            }
        }

        return eventCodeTypes;
    }
}