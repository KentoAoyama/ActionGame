using System.Collections.Generic;
using UnityEngine;
using Domain;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
    public class ScenarioDataSO : ScriptableObject
    {
        [SerializeField]
        private List<SpeakData> _speakDataList;
        public List<SpeakData> SpeakDataList => _speakDataList;


        /// <param name="fileName">SO生成時にファイル名を設定する用</param>
        /// <param name="speakDataList">シナリオのデータ</param>
        public void Initialized(string fileName, List<SpeakData> speakDataList)
        {
            name = fileName;

            // シナリオデータが空の場合はエラー
            if (speakDataList.Count == 0)
            {

                throw new System.Exception($"ScenarioDataSO生成時に渡されるデータが不正です\nファイル名：{fileName}");
            }
            else
            {
                _speakDataList = speakDataList;
            }
        }
    }
}
