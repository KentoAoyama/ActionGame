using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Infrastructure
{
    [CustomEditor(typeof(ScenarioDataSOList))]//拡張するクラスを指定
    public class ScenarioDataLoadButton : Editor
    {
        /// <summary>
        /// InspectorのGUIを更新
        /// </summary>
        public override void OnInspectorGUI()
        {
            //元のInspector部分を表示
            base.OnInspectorGUI();

            //targetを変換して対象を取得
            ScenarioDataSOList scenarioSO = target as ScenarioDataSOList;

            //PublicMethodを実行する用のボタン
            if (GUILayout.Button("Load"))
            {
                scenarioSO.SetScenarioData();
            }
        }
    }
}