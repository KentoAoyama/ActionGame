using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
public class ScenarioDataSO : ScriptableObject
{
    private const string SCENARIOFILE_NAME = "ScenarioFileNameSO";

    public Dictionary<string, List<string>> scenarioData = new ();


    private void OnEnable()
    {
        ScenarioFileNameSO scenarioFileNameSO = Resources.Load<ScenarioFileNameSO>(SCENARIOFILE_NAME);

        if (scenarioFileNameSO.FileNames == null) return;

        foreach (string fileName in scenarioFileNameSO.FileNames)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(fileName);

            // CSVデータを解析してリストに格納
            if (textAsset == null) return;

            List<string> scenarioList = new ();
            StringReader reader = new (textAsset.text);
            while (reader.Peek() != -1)
            {
                scenarioList.Add(reader.ReadLine());
            }

            scenarioData.Add(fileName, scenarioList);
        }
    }
}
