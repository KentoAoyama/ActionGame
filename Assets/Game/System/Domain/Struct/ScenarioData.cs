using System.Collections.Generic;

/// <summary>
/// シナリオのデータを格納する構造体
/// </summary>
public struct ScenarioData
{
    public ScenarioData(SpeakData[] scenarioData)
    {
        SpeakData = scenarioData;
    }

    public SpeakData[] SpeakData;
}
