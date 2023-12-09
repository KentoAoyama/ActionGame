using UnityEngine;

/// <summary>
/// 発言の内容を格納する構造体
/// </summary>
public readonly struct SpeakData
{
    public readonly int TextPlace;

    public readonly CharacterType CharacterType;

    public readonly string SpeakText;

    public readonly float ShowTextTime;

    public readonly EventCodeType[] EventType;

    public SpeakData(int textPlace, string characterName, string speakText, float showTextTime, EventCodeType[] eventType)
    {
        // テキストの場所番号が不正な場合はエラー
        if (textPlace < 0)
        {
            throw new System.Exception("テキストの場所番号が不正です");
        }
        else
        {
            TextPlace = textPlace;
        }


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


        // 表示時間が不正な場合はエラー
        if (showTextTime < 0)
        {
            throw new System.Exception("表示時間が不正です");
        }
        else
        {
            ShowTextTime = showTextTime;
        }


        // イベントコードが不正な場合はエラー
        if (eventType == null)
        {
            throw new System.Exception("イベントコードが不正です");
        }
        else
        {
            EventType = eventType;
        }
    }
}
