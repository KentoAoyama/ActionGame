using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "ScenarioDataListSO", menuName = "ScriptableObject/ScenarioDataListSO")]
    public class ScenarioDataSOList : ScriptableObject
    {
        // ScenarioDataを格納するDictionary
        private Dictionary<string, ScenarioDataSO> _scenarioDataDictionary;
        public Dictionary<string, ScenarioDataSO> ScenarioDataDictionary => _scenarioDataDictionary;

        // Inspectorに表示するシナリオデータのリスト
        [SerializeField, ReadOnly]
        private List<ScriptableObject> _inspectorViewScenarioSOList;

        /// <summary>
        /// シナリオデータを格納するDictionaryとInspectorに表示するシナリオデータのリストを設定
        /// </summary>
        public void SetData(Dictionary<string, ScenarioDataSO> dic, List<ScriptableObject> list)
        {
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
            if (_inspectorViewScenarioSOList == null)
            {
                _inspectorViewScenarioSOList = new List<ScriptableObject>();
            }
            else
            {
                _inspectorViewScenarioSOList.Clear();
            }

            // データを格納
            _scenarioDataDictionary = dic;
            _inspectorViewScenarioSOList = list;
        }
    }
}