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


        /// <param name="fileName">SO�������Ƀt�@�C������ݒ肷��p</param>
        /// <param name="speakDataList">�V�i���I�̃f�[�^</param>
        public void Initialized(string fileName, List<SpeakData> speakDataList)
        {
            name = fileName;

            // �V�i���I�f�[�^����̏ꍇ�̓G���[
            if (speakDataList.Count == 0)
            {

                throw new System.Exception($"ScenarioDataSO�������ɓn�����f�[�^���s���ł�\n�t�@�C�����F{fileName}");
            }
            else
            {
                _speakDataList = speakDataList;
            }
        }
    }
}
