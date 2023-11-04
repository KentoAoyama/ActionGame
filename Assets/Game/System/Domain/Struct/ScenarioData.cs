using System.Collections.Generic;

/// <summary>
/// シナリオのデータを格納する構造体
/// </summary>
public readonly struct ScenarioData
{
    public ScenarioData(List<SpeakEventData> scenarioData)
    {
        // シナリオデータが空の場合はエラー
        if (scenarioData.Count == 0)
        {

            throw new System.Exception("シナリオデータが不正です");
        }
        else
        {
            SpeakData = scenarioData;
        }
    }

    public readonly List<SpeakEventData> SpeakData;
}
