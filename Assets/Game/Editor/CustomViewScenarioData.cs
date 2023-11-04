using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ScenarioDataSO))] // MyComponentは、リストをメンバーに持つ構造体を含むコンポーネントです
public class CustomViewScenarioData : Editor
{
    public override void OnInspectorGUI()
    {
        ScenarioDataSO scenarioDataSO = (ScenarioDataSO)target;

        // リストをメンバーに持つ構造体内のリストを取得
        List<ScenarioDataSO.ViewScenarioData> scenarioDataList = scenarioDataSO.ScenarioDataList;

        // リストをエディタ内で表示
        for (int i = 0; i < scenarioDataList.Count; i++)
        {
            //TODO: ここで表示する内容を変更する
            //scenarioDataList[i] = EditorGUILayout.TextField((i + 1).ToString(), scenarioDataList[i]);
        }

        // リストの変更をコミット
        EditorUtility.SetDirty(target);
    }
}


