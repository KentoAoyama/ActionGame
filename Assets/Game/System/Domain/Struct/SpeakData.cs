using UnityEngine;

namespace Domain
{
    /// <summary>
    /// 発言の内容を格納する構造体
    /// </summary>
    [System.Serializable]
    public struct SpeakData
    {
        [Header("テキストの場所番号")]
        [SerializeField, ReadOnly]
        private int _textPlace;
        public readonly int TextPlace => _textPlace;

        [Header("キャラクターのタイプ")]
        [SerializeField, ReadOnly]
        private CharacterType _characterType;
        public readonly CharacterType CharacterType => _characterType;

        [Header("発言内容")]
        [SerializeField]
        [TextArea(3, 1)]
        private string _speakText;
        public readonly string SpeakText => _speakText;

        [Header("表示にかける時間")]
        [SerializeField, ReadOnly]
        private float _showTextTime;
        public readonly float ShowTextTime => _showTextTime;

        [Header("イベントコード")]
        [SerializeField, ReadOnly]
        private EventCodeType[] _eventType;
        public readonly EventCodeType[] EventType => _eventType;

        public SpeakData(int textPlace, string characterName, string speakText, float showTextTime, EventCodeType[] eventType)
        {
            // テキストの場所番号が不正な場合はエラー
            if (textPlace < 0)
            {
                throw new System.Exception("テキストの場所番号が不正です");
            }
            else
            {
                _textPlace = textPlace;
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
                _characterType = characterType;
            }


            // 発言内容が不正な場合はエラー
            if (speakText == null)
            {
                throw new System.Exception("発言内容が不正です");
            }
            else
            {
                _speakText = speakText;
            }


            // 表示にかける時間が不正な場合はエラー
            if (showTextTime < 0)
            {
                throw new System.Exception("表示時間が不正です");
            }
            else
            {
                _showTextTime = showTextTime;
            }


            // イベントコードが不正な場合はエラー
            if (eventType == null)
            {
                throw new System.Exception("イベントコードが不正です");
            }
            else
            {
                _eventType = eventType;
            }
        }
    }
}
