using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataListSO")]
public class ScenarioDataListSO : ScriptableObject
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";

    // ScenarioDataを格納するDictionary
    private Dictionary<string, ScenarioData> _scenarioDataDictionary;
    public Dictionary<string, ScenarioData> ScenarioDataDictionary => _scenarioDataDictionary;

    private void OnEnable()
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
            _scenarioDataDictionary = new Dictionary<string, ScenarioData>();
        }
        else
        {
            _scenarioDataDictionary.Clear();
        }

        // シナリオデータを格納するListを初期化
        if (_scenarioData == null)
        {
            _scenarioData = new List<InspectorViewScenarioData>();
        }
        else
        {
            _scenarioData.Clear();
        }

        foreach (string fileName in scenarioFileNameSO.FileNames)
        {
            // ResourcesフォルダからCSVデータを取得
            TextAsset textAsset = Resources.Load<TextAsset>("TextData/" + fileName);

            if (textAsset == null)
                throw new System.Exception("CSVファイルが存在しません。");

            // CSVデータを格納
            StringReader reader = new(textAsset.text);

            ScenarioData scenarioData = new(GetSpeakDataList(reader));

            // Dictionaryに格納
            _scenarioDataDictionary.Add(fileName, scenarioData);
            // View用のデータを格納
            _scenarioData.Add(new InspectorViewScenarioData(fileName, talkData));
        }
    }

    // StringReaderを受け取り、SpeakDataのリストを返す
    private List<SpeakData> GetSpeakDataList(StringReader reader)
    {
        // 発言データを格納するリスト
        List<SpeakData> speakDataList = new();

        while (reader.Peek() != -1)
        {
            var data = reader.ReadLine().Split(',');

            // CSVデータをSpeakDataに変換してリストに格納
            SpeakData speakData = new(data[0], data[1]);

            // リストに格納
            speakDataList.Add(speakData);
        }

        return speakDataList;
    }
}