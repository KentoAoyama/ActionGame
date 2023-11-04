using UnityEngine;

/// <summary>
/// 発言イベントの内容を格納する構造体
/// </summary>
public readonly struct SpeakEventData
{
    public SpeakEventData(string characterName, string speakText)
    {
        CharacterType characterType = CharacterType.None;

        // キャラクター名に応じてキャラクターのTypeを設定
        switch (characterName)
        {
            case "P":
                characterType = CharacterType.P;
                break;
            case "U":
                characterType = CharacterType.U;
                break;
        }

        // キャラクター名が不正な場合はエラー
        if (characterType == CharacterType.None)
        {
            throw new System.Exception("キャラクター名が不正です");
        }
        else
        {
            CharacterType = characterType;
        }

        // 発言内容が不正な場合はエラー
        if (speakText == null)
        {
            throw new System.Exception("発言内容が不正です");
        }
        else
        {
            SpeakText = speakText;
        }
    }

    public readonly CharacterType CharacterType;

    public readonly string SpeakText;
}
