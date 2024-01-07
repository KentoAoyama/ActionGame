using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Domain;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
    public class ScenarioDataSO : ScriptableObject
    {
        [SerializeField]
        private List<SpeakData> _speakData;
        public List<SpeakData> SpeakData => _speakData;

        /// <param name="fileName">SO生成時にファイル名を設定する用</param>
        /// <param name="speakDataList">シナリオのデータ</param>
        public void Initialized(string fileName, List<SpeakData> speakDataList)
        {
            string path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, fileName);

            // シナリオデータが空の場合はエラー
            if (speakDataList.Count == 0)
            {

                throw new System.Exception("シナリオデータが不正です");
            }
            else
            {
                _speakData = speakDataList;
            }
        }
    }
}
