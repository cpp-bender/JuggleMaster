using UnityEngine.UI;
using SimpleEvent;
using UnityEngine;
using DG.Tweening;

namespace JuggleMaster
{
    public class FailMenu : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public CanvasGroup canvasGroup;

        [Header("DEPENDENCIES")]
        public Image bg;
        public Transform failPopup;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO levelFailEvent;
        public VoidEventChannelSO tryAgainClickEvent;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            levelFailEvent.Event += OnLevelFailed;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            levelFailEvent.Event -= OnLevelFailed;
        }

        private void OnLevelFailed()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            failPopup.DOScale(5f, .25f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad).Play();
        }

        private void OnGameInit()
        {
            canvasGroup.alpha = 0f;
        }
        
        public void OnTryAgainClicked()
        {
            tryAgainClickEvent.Raise();
        }
    }
}
