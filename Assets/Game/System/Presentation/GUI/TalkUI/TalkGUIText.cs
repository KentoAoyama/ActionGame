using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using TMPro;

namespace Presentation
{
    /// <summary>
    /// �I�[�o�[���C�\���̃e�L�X�g��\������N���X
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

        // �񓯊�(UniTask)�Ńe�L�X�g�̕\�����s��
        public async UniTask SetTextAsync(string text, float duration)
        {
            _tmp.text = string.Empty;
            await _tmp.DOText(text, duration);
        }
    }
}
