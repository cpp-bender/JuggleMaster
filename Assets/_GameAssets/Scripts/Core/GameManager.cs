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
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;

        private IEnumerator Start()
        {
            Application.targetFrameRate = 60;
            InitDOTween();
            gameInitEvent.Raise();
            yield return new WaitForMouseDown();
            gameStartEvent.Raise();
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
    }
}