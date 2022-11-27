using System.Collections;
using SimpleEvent;
using UnityEngine;
using DG.Tweening;
using System.Text;
using System;
using TMPro;

namespace JuggleMaster
{
    public class GameMenu : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public CanvasGroup canvasGroup;

        [Header("DEPENDENCIES")]
        public Transform counter;
        public TextMeshProUGUI stallsCounter;
        public TextMeshProUGUI kickCounter;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;
        public VoidEventChannelSO kickVaseEvent;
        public BoolEventChannelSO stallUpdateEvent;
        public VoidEventChannelSO levelWinEvent;
        public VoidEventChannelSO levelFailEvent;

        [Header("DEBUG")]
        public bool completeStall = false;
        public bool completeKick = false;
        public int currentKick = 0;
        public int desKick = 10;
        public bool updateStall;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            gameStartEvent.Event += OnGameStart;
            kickVaseEvent.Event += OnVaseKicked;
            levelFailEvent.Event += OnLevelFailed;
            stallUpdateEvent.Event += (bool arg) => updateStall = arg;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            gameStartEvent.Event -= OnGameStart;
            kickVaseEvent.Event -= OnVaseKicked;
            levelFailEvent.Event -= OnLevelFailed;
            stallUpdateEvent.Event -= (bool arg) => updateStall = arg;
        }

        private IEnumerator UpdateStallRoutine()
        {
            float secs = 0f;
            float speed = .1f;
            while (true)
            {
                if (secs >= .5f)
                {
                    completeStall = true;
                    CheckLevelEnd();
                    stallsCounter.color = Color.green;
                    stallsCounter.transform.DOScale(1f, .25f)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .Play();
                    yield break;
                }

                if (updateStall)
                {
                    secs += Time.deltaTime * speed;
                    StringBuilder s = new StringBuilder();
                    s.Append(TimeSpan.FromSeconds(secs).ToString(@"s\.ff"));
                    s.Append("s");
                    s.Append("/");
                    s.Append("0.50s");
                    stallsCounter.SetText(s.ToString());
                }
                yield return null;
            }
        }

        private void OnVaseKicked()
        {
            if (currentKick >= desKick) return;

            currentKick++;

            StringBuilder s = new StringBuilder();
            s.Append(currentKick);
            s.Append("/");
            s.Append(desKick);

            kickCounter.SetText(s.ToString());

            if (currentKick == desKick)
            {
                completeKick = true;
                CheckLevelEnd();
                kickCounter.color = Color.green;
                kickCounter.transform.DOScale(1f, .25f)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .Play();
            }
        }

        public void OnGameInit()
        {
            HandleKickText();
            HandleStallsText();
            StartCoroutine(UpdateStallRoutine());
            canvasGroup.alpha = 0f;


            void HandleStallsText()
            {
                StringBuilder s = new StringBuilder();
                s.Append("0.00s");
                s.Append("/");
                s.Append("0.50s");
                stallsCounter.SetText(s.ToString());
            }

            void HandleKickText()
            {
                StringBuilder s = new StringBuilder();
                s.Append(currentKick);
                s.Append("/");
                s.Append(desKick);
                kickCounter.SetText(s.ToString());
            }
        }

        public void OnGameStart()
        {
            DOVirtual.Float(canvasGroup.alpha, 1f, .25f, x => canvasGroup.alpha = x).Play();
            counter.GetComponent<TextMeshProUGUI>().SetText("0");
        }
        
        private void OnLevelFailed()
        {
            canvasGroup.alpha = 0f;
        }

        public void CheckLevelEnd()
        {
            if (completeKick && completeStall)
            {
                DOVirtual.Float(canvasGroup.alpha, 0f, .25f, x => canvasGroup.alpha = x).Play();
                levelWinEvent.Raise();
            }
        }
    }
}
