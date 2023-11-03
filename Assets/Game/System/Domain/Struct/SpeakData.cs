/// <summary>
/// 発言の内容を格納する構造体
/// </summary>
public class SpeakData
{
    public SpeakData(CharacterType characterType, string speakText)
    {
        CharacterType = characterType;
        SpeakText = speakText;
    }

    public readonly CharacterType CharacterType;

    public readonly string SpeakText;
}
