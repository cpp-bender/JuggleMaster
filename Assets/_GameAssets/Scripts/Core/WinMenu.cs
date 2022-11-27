using UnityEngine.UI;
using DG.Tweening;
using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class WinMenu : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public CanvasGroup canvasGroup;
        public Image image;

        [Header("DEPENDENCIES")]
        public Transform excellentPopup;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO levelWinEvent;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            levelWinEvent.Event += OnLevelWin;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            levelWinEvent.Event -= OnLevelWin;
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        private void OnLevelWin()
        {
            canvasGroup.alpha = 1f;
            image.color = new Color(1f, 1f, 1f, .5f);
            excellentPopup.DOScale(3f, .25f).SetEase(Ease.OutQuad).Play();
        }

        private void OnGameInit()
        {
            canvasGroup.alpha = 0f;
        }
    }
}
