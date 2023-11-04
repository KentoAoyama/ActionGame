using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScenarioDataSO))]//拡張するクラスを指定
public class ScenarioDataSOLoadButton : Editor
{
    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        ScenarioDataSO scenarioSO = target as ScenarioDataSO;

        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("Load"))
        {
            scenarioSO.SetScenarioData();
        }
    }
}