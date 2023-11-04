using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
public class ScenarioDataSO : ScriptableObject
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";

    // ScenarioDataを格納するDictionary
    public Dictionary<string, ScenarioData> _scenarioData = new();

    public readonly struct ViewScenarioData
    {
        public ViewScenarioData(string fileName, List<string> talkData)
        {
            FileName = fileName;
            TalkData = talkData;
        }

        public readonly string FileName;
        public readonly List<string> TalkData;
    }

    public List<ViewScenarioData> ScenarioDataList;


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
        if (ScenarioDataList == null)
        {
            ScenarioDataList = new List<ViewScenarioData>();
        }
        else
        {
            ScenarioDataList.Clear();
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
            List<string> talkData = new();

            while (reader.Peek() != -1)
            {
                var data = reader.ReadLine().Split(',');

                // CSVデータをSpeakDataに変換してリストに格納
                SpeakEventData speakData = new(data[0], data[1]);
                speakDataList.Add(speakData);

                // インスペクターにScenerioDataを表示するためのリストに格納
                talkData.Add(
                    speakData.CharacterType.ToString() + "\n" + speakData.SpeakText);
            }

            ScenarioData scenarioData = new(speakDataList);

            // Dictionaryに格納
            _scenarioData.Add(fileName, scenarioData);
            // View用のデータを格納
            ScenarioDataList.Add(new ViewScenarioData(fileName, talkData));
        }
    }
}