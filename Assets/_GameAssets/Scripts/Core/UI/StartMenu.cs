using UnityEngine;
using SimpleEvent;
using DG.Tweening;

namespace JuggleMaster
{
    public class StartMenu : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public CanvasGroup canvasGroup;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            gameStartEvent.Event += OnGameStart;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            gameStartEvent.Event -= OnGameStart;
        }

        private void OnGameInit()
        {
            canvasGroup.alpha = 1f;
        }

        private void OnGameStart()
        {
            DOVirtual.Float(canvasGroup.alpha, 0f, .25f, x => canvasGroup.alpha = x).Play();
        }
    }
}
