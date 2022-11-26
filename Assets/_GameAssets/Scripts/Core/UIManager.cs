using DG.Tweening;
using SimpleEvent;
using UnityEngine;
using TMPro;

namespace HyperBase
{
    [DefaultExecutionOrder(-100)]
    public class UIManager : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Transform gemImage;
        public TextMeshProUGUI gemCounter;
        public GameObject gemPrefab;

        [Header("EVENTS")]
        public Vector3EventChannelSO collectGemEvent;

        private int gemCount;

        private void OnEnable()
        {
            collectGemEvent.Event += OnGemCollected;
        }

        private void OnDisable()
        {
            collectGemEvent.Event -= OnGemCollected;
        }

        private void OnGemCollected(Vector3 pos)
        {
            GameObject gem = Instantiate(gemPrefab, transform);
            gem.transform.position = Camera.main.WorldToScreenPoint(pos);
            gem.transform.DOMove(gemImage.position, .5f)
                .OnComplete(() =>
                {
                    Destroy(gem.gameObject);

                    gemCount += 20;
                    gemCounter.SetText(gemCount.ToString());
                    gemCounter.GetComponent<Transform>().DOScale(1f, .25f)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .Play();
                })
                .Play();
        }
    }
}