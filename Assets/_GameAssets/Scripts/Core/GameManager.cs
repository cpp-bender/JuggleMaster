using UnityEngine.SceneManagement;
using DG.Tweening.Core.Enums;
using System.Collections;
using DG.Tweening;
using SimpleEvent;
using UnityEngine;

namespace HyperBase
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public GameObject gem;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;
        public VoidEventChannelSO levelFailEvent;
        public VoidEventChannelSO levelWinEvent;
        public VoidEventChannelSO tryAgainClickEvent;

        private IEnumerator Start()
        {
            Application.targetFrameRate = 60;
            InitDOTween();
            gameInitEvent.Raise();
            yield return new WaitForMouseDown();
            gameStartEvent.Raise();
            StartCoroutine(CreateGemRoutine());
        }

        private void OnEnable()
        {
            levelFailEvent.Event += OnLevelFailed;
            levelWinEvent.Event += OnLevelWin;
            tryAgainClickEvent.Event += OnLevelLoad;
        }

        private void OnDisable()
        {
            levelFailEvent.Event -= OnLevelFailed;
            levelWinEvent.Event -= OnLevelWin;
            tryAgainClickEvent.Event -= OnLevelLoad;
        }

        private void OnLevelWin()
        {
            StopCoroutine(CreateGemRoutine());
        }

        private void OnLevelFailed()
        {
            StopCoroutine(CreateGemRoutine());
        }

        private void OnLevelLoad()
        {
            DOTween.Clear();
            SceneManager.LoadScene(0);
        }

        private void InitDOTween()
        {
            //Default All DOTween Global Settings
            DOTween.Init(true, true, LogBehaviour.Default);
            DOTween.defaultAutoPlay = AutoPlay.None;
            DOTween.maxSmoothUnscaledTime = .15f;
            DOTween.nestedTweenFailureBehaviour = NestedTweenFailureBehaviour.TryToPreserveSequence;
            DOTween.showUnityEditorReport = false;
            DOTween.timeScale = 1f;
            DOTween.useSafeMode = true;
            DOTween.useSmoothDeltaTime = false;
            DOTween.SetTweensCapacity(200, 50);

            //Default All DOTween Tween Settings
            DOTween.defaultAutoKill = true;
            DOTween.defaultEaseOvershootOrAmplitude = 1.70158f;
            DOTween.defaultEasePeriod = 0f;
            DOTween.defaultEaseType = Ease.Linear;
            DOTween.defaultLoopType = LoopType.Restart;
            DOTween.defaultRecyclable = true;
            DOTween.defaultTimeScaleIndependent = false;
            DOTween.defaultUpdateType = UpdateType.Normal;
        }

        private IEnumerator CreateGemRoutine()
        {
            int i = 0;
            while (i < 3)
            {
                yield return new WaitForSeconds(Random.Range(1f, 3f));

                float rndX = Random.Range(.2f, .8f);
                float rndY = Random.Range(.4f, .8f);

                Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(rndX, rndY, 5f));
                pos.z = 5f;
                GameObject obj = Instantiate(gem);
                obj.transform.position = pos;
                i++;
                yield return null;
            }
        }
    }
}