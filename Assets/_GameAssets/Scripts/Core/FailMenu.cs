using UnityEngine.UI;
using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class FailMenu : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public CanvasGroup canvasGroup;

        [Header("DEPENDENCIES")]
        public Image bg;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO levelFailEvent;

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
            canvasGroup.alpha = 1f;
        }

        private void OnGameInit()
        {
            canvasGroup.alpha = 0f;
        }
    }
}
