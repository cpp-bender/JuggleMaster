using UnityEngine;
using SimpleEvent;
using DG.Tweening;
using TMPro;

namespace JuggleMaster
{
    public class StartMenu : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public TextMeshProUGUI tapToPlayText;

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
            gameObject.SetActive(true);
        }

        private void OnGameStart()
        {
            tapToPlayText.GetComponent<DOTweenAnimation>().DOPlay();
        }
    }
}
