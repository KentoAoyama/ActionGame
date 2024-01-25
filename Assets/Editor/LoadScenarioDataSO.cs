using UnityEngine;
using UnityEditor;
using Domain;
using Infrastructure;
using System.Collections.Generic;
using System.IO;
using System;

public class LoadScenarioDataSO
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";
    private const string SCENARIOFILESO_NAME = "ScenarioDataListSO";

    // ヘッダー行を読み飛ばす行数
    private const int SKIP_LINE_COUNT = 4;

    [MenuItem("Project/Load Scenario Data SO")]
    static void LoadScenarioData()
    {
        // ScenarioFileNameSOをResourcesフォルダから取得
        ScenarioFileNameSO scenarioFileNameSO =
            Resources.Load<ScenarioFileNameSO>("ScriptableObject/" + SCENARIOFILE_NAME);

        if (scenarioFileNameSO.FileNames == null)
            throw new System.Exception("ScenarioFileNameSOが存在しません。");

        // ScenarioDataListSOをResourcesフォルダから取得
        ScenarioDataSOList scenarioDataSOList =
            Resources.Load<ScenarioDataSOList>("ScriptableObject/" + SCENARIOFILESO_NAME);

        if (scenarioDataSOList == null)
            throw new System.Exception("ScenarioDataListSOが存在しません。");

        // シナリオデータを格納するDictionaryとInspectorに表示するシナリオデータのリストを作成
        Dictionary<string, ScenarioDataSO> scenarioDataDictionary = new();
        List<ScriptableObject> inspectorViewScenarioSOList = new();


        foreach (string fileName in scenarioFileNameSO.FileNames)
        {
            // ResourcesフォルダからCSVデータを取得
            TextAsset textAsset = Resources.Load<TextAsset>("TextData/" + fileName);

            if (textAsset == null)
                throw new System.Exception("CSVファイルが存在しません。");

            StringReader reader = new(textAsset.text);
            // StringReaderからScenarioDataを作成
            List<SpeakData> scenarioData = new(ConvertSpeakDataList(reader));

            // ScriptableObjectのScenarioDataSOを作成
            var scenarioDataSO =
                Resources.Load<ScenarioDataSO>("ScriptableObject/ScenarioData/" + fileName);

            if (scenarioDataSO != null)
            {
                // 既にScriptableObjectが存在する場合は削除
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(scenarioDataSO));
            }

            scenarioDataSO = CreateScenarioDataSO(fileName, scenarioData);

            // Dictionaryに格納
            scenarioDataDictionary.Add(fileName, scenarioDataSO);
            // Inspectorに表示するシナリオデータのリストに格納
            inspectorViewScenarioSOList.Add(scenarioDataSO);
        }

        // ScenarioDataListSOにデータを格納
        scenarioDataSOList.SetData(scenarioDataDictionary, inspectorViewScenarioSOList);

        Debug.Log("全てのシナリオデータの格納終了");
    }

    private static ScenarioDataSO CreateScenarioDataSO(string fileName, List<SpeakData> scenarioData)
    {
        // ScriptableObjectのScenarioDataSOを作成
        var scenarioDataSO = ScriptableObject.CreateInstance<ScenarioDataSO>();
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
    private static List<SpeakData> ConvertSpeakDataList(StringReader reader)
    {
        // 発言データを格納するリスト
        List<SpeakData> speakDataList = new();

        for (int i = 0; i < SKIP_LINE_COUNT; i++)
        {
            // ヘッダー行を読み飛ばす
            reader.ReadLine();
        }

        while (reader.Peek() != -1)
        {
            var data = reader.ReadLine().Split(',');

            var data0 = ConvertToInt(data[0]);
            var data4 = float.Parse(data[4]);
            var data5 = ConvertToEventCodeType(data);

            // CSVデータをSpeakDataに変換してリストに格納
            SpeakData speakData
                = new(
                    data0,      // テキストの場所番号
                    data[1],    // キャラクター名
                    data[2],    // 発言内容   // TODO:ローカライズ対応をする
                    data4,      // 表示時間 
                    data5);     // イベントコード

            // リストに格納
            speakDataList.Add(speakData);
        }

        return speakDataList;
    }

    private static EventCodeType[] ConvertToEventCodeType(string[] data)
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

    private static int ConvertToInt(string data)
    {
        Boolean isOk = Double.TryParse(data, out Double temp);
        int value = isOk ? (int)temp : 0;

        return value;
    }
}
