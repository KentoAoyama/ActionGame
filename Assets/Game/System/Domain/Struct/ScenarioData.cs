using System.Collections.Generic;

[System.Serializable]
public readonly struct ScenarioData
{
    public readonly List<SpeakData> SpeakData;

    public ScenarioData(List<SpeakData> speakData)
    {
        // シナリオデータが空の場合はエラー
        if (speakData.Count == 0)
        {

            throw new System.Exception("シナリオデータが不正です");
        }
        else
        {
            SpeakData = speakData;
        }
    }
}
