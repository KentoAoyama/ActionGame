using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioDataSO", menuName = "ScriptableObject/ScenarioDataSO")]
public class ScenarioDataSO : ScriptableObject
{
    [SerializeField, ReadOnly(true)]
    private ScenarioData _scenarioData;
    public ScenarioData ScenarioData => _scenarioData;

    public ScenarioDataSO(ScenarioData scenarioData)
    {
        _scenarioData = scenarioData;
    }
}
