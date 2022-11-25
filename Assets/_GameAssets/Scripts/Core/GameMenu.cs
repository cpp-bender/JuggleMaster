using SimpleEvent;
using UnityEngine;
using DG.Tweening;
using System.Text;
using TMPro;

namespace JuggleMaster
{
    public class GameMenu : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public CanvasGroup canvasGroup;

        [Header("DEPENDENCIES")]
        public Transform counter;
        public TextMeshProUGUI currentStallsText;
        public TextMeshProUGUI kickText;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;
        public VoidEventChannelSO kickVaseEvent;

        private int currentKick = 0;
        private int desKick = 10;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            gameStartEvent.Event += OnGameStart;
            kickVaseEvent.Event += OnVaseKicked;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            gameStartEvent.Event -= OnGameStart;
            kickVaseEvent.Event -= OnVaseKicked;
        }

        private void OnVaseKicked()
        {
            if (currentKick >= desKick) return;

            currentKick++;

            StringBuilder s = new StringBuilder();
            s.Append(currentKick);
            s.Append("/");
            s.Append(desKick);

            kickText.SetText(s.ToString());

            if (currentKick == desKick)
            {
                kickText.color = Color.green;
                kickText.transform.DOScale(1f, .25f)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .Play();
            }
        }

        public void OnGameInit()
        {
            canvasGroup.alpha = 0f;
        }

        public void OnGameStart()
        {
            DOVirtual.Float(canvasGroup.alpha, 1f, .25f, x => canvasGroup.alpha = x).Play();
            counter.GetComponent<TextMeshProUGUI>().SetText("0");
        }
    }
}
