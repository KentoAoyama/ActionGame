using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
public class ScenarioDataSO : ScriptableObject
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";

    // ScenarioDataを格納するDictionary
    public Dictionary<string, ScenarioData> _scenarioData = new();

    // インスペクターにScenerioDataを表示するための構造体
    [System.Serializable]
    public struct InspectorViewScenarioData
    {
        public InspectorViewScenarioData(string fileName, string talkData)
        {
            FileName = fileName;
            TalkData = talkData;
        }

        // シナリオファイル名
        public string FileName;

        // インスペクターにScenerioDataを表示するための文字列
        [TextArea(3, 10)]
        public string TalkData;
    }

    [SerializeField]
    private List<InspectorViewScenarioData> _scenarioDataList;
    public List<InspectorViewScenarioData> ScenarioDataList => _scenarioDataList;


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
        ScenarioFileNameSO scenarioFileNameSO = Resources.Load<ScenarioFileNameSO>("ScriptableObject/" + SCENARIOFILE_NAME);

        if (scenarioFileNameSO.FileNames == null) return;

        // シナリオデータを格納するDictionaryを初期化
        if (_scenarioDataList == null)
        {
            _scenarioDataList = new List<InspectorViewScenarioData>();
        }
        else
        {
            _scenarioDataList.Clear();
        }

        foreach (string fileName in scenarioFileNameSO.FileNames)
        {
            // ResourcesフォルダからCSVデータを取得
            TextAsset textAsset = Resources.Load<TextAsset>("TextData/" + fileName);

            if (textAsset == null)
                throw new System.Exception("CSVファイルが存在しません。");

            // CSVデータを格納
            StringReader reader = new(textAsset.text);

            // 2行目までヘッダーなので読み飛ばす
            reader.ReadLine();
            reader.ReadLine();

            // 発言データを格納するリスト
            List<SpeakEventData> speakDataList = new();

            // インスペクターにScenerioDataを表示するためのリスト
            string talkData = "";

            while (reader.Peek() != -1)
            {
                var data = reader.ReadLine().Split(',');

                // CSVデータをSpeakDataに変換してリストに格納
                SpeakEventData speakData = new(data[0], data[1]);

                // リストに格納
                speakDataList.Add(speakData);

                // インスペクターにScenerioDataを表示するための文字列に格納
                talkData += $"{speakData.CharacterType} : {speakData.SpeakText}\n";
            }

            ScenarioData scenarioData = new(speakDataList);

            // Dictionaryに格納
            _scenarioData.Add(fileName, scenarioData);
            // View用のデータを格納
            _scenarioDataList.Add(new InspectorViewScenarioData(fileName, talkData));
        }
    }
}