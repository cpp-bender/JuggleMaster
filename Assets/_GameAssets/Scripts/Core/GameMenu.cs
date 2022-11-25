using SimpleEvent;
using TMPro;
using UnityEngine;

namespace JuggleMaster
{
    public class GameMenu : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Transform gem;
        public Transform counter;

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

        public void OnGameInit()
        {
            gem.gameObject.SetActive(false);
            counter.gameObject.SetActive(false);
        }

        public void OnGameStart()
        {
            gem.gameObject.SetActive(true);
            counter.gameObject.SetActive(true);
            counter.GetComponent<TextMeshProUGUI>().SetText("0");
        }
    }
}
