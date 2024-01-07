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

        /// <param name="fileName">SO�������Ƀt�@�C������ݒ肷��p</param>
        /// <param name="speakDataList">�V�i���I�̃f�[�^</param>
        public void Initialized(string fileName, List<SpeakData> speakDataList)
        {
            string path = AssetDatabase.GetAssetPath(this);
            AssetDatabase.RenameAsset(path, fileName);

            // �V�i���I�f�[�^����̏ꍇ�̓G���[
            if (speakDataList.Count == 0)
            {

                throw new System.Exception("�V�i���I�f�[�^���s���ł�");
            }
            else
            {
                _speakData = speakDataList;
            }
        }
    }
}
