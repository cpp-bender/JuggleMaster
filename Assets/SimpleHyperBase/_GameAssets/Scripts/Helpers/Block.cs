using DG.Tweening;
using UnityEngine;

namespace HyperBase
{
    public class Block : MonoBehaviour
    {
        private void Awake()
        {
            transform.DOMoveX(5f, 1f)
                .SetRelative(true)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .Play();
        }
    }
}
