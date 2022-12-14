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
        public VoidEventChannelSO nextButtonClickEvent;

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
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            excellentPopup.DOScale(5f, .25f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad).Play();
        }

        private void OnGameInit()
        {
            canvasGroup.alpha = 0f;
        }
        
        public void OnNextButtonClicked()
        {
            nextButtonClickEvent.Raise();
        }
    }
}
