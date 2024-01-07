using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

namespace Presentation
{
    /// <summary>
    /// オーバーレイ表示のテキストを表示するクラス
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TalkGUIText : MonoBehaviour, IUIComponent
    {
        [SerializeField]
        private TextMeshProUGUI _tmp;

        public void Initialized()
        {
            _tmp.text = string.Empty;
        }

        // 非同期(UniTask)でテキストの表示を行う
        public async UniTask SetTextAsync(string text, float duration)
        {
            _tmp.text = string.Empty;
            await _tmp.DOText(text, duration);
        }
    }
}
