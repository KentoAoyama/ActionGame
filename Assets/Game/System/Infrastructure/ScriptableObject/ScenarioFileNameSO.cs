using UnityEngine;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "ScenarioFileNameSO", menuName = "ScriptableObject/ScenarioFileNameSO")]
    public class ScenarioFileNameSO : ScriptableObject
    {
        public string[] FileNames;
    }
}
