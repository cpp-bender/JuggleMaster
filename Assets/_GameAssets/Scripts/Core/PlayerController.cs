using SimpleEvent;
using UnityEngine;
using DG.Tweening;
using Lean.Touch;

namespace JuggleMaster
{
    public class PlayerController : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Shoe rightShoe;
        public Shoe leftShoe;

        [Header("EVENTS")]
        public VoidEventChannelSO levelWinEvent;
        public VoidEventChannelSO levelFailEvent;

        [Header("DEBUG")]
        public Shoe closestShoe;

        private bool takeInput = true;

        private void OnEnable()
        {
            levelWinEvent.Event += OnLevelWin;
            levelFailEvent.Event += OnLevelFail;
        }

        private void OnDisable()
        {
            levelWinEvent.Event -= OnLevelWin;
            levelFailEvent.Event -= OnLevelFail;
        }

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        private void OnLevelWin()
        {
            if (closestShoe != null)
            {
                closestShoe.transform.DORotate(closestShoe.startRot.eulerAngles, .25f).Play();
                closestShoe.transform.DOMove(closestShoe.startPos, .25f).Play();
            }
            takeInput = false;
        }

        private void OnLevelFail()
        {
            if (closestShoe != null)
            {
                closestShoe.transform.DOMove(closestShoe.startPos, .25f).Play();
                closestShoe.transform.DORotate(closestShoe.startRot.eulerAngles, .25f).Play();
            }
            takeInput = false;
        }

        public void OnFingerDown(Vector3 pos)
        {
            if (!takeInput) return;

            float f1 = (rightShoe.transform.position - pos).sqrMagnitude;
            float f2 = (leftShoe.transform.position - pos).sqrMagnitude;

            if (f1 < f2)
            {
                closestShoe = rightShoe;
            }
            else
            {
                closestShoe = leftShoe;
            }
        }

        public void OnFingerUpdate(LeanFinger finger)
        {
            if (closestShoe == null) return;

            if (!takeInput) return;

            Vector3 desPos = finger.GetWorldPosition(5f);
            desPos = new Vector3(Mathf.Clamp(desPos.x, -1f, 1f), Mathf.Clamp(desPos.y, .1f, 2f), 5f);
            closestShoe.Move(desPos);

            Quaternion rot = Quaternion.LookRotation(desPos - closestShoe.startPos).normalized;
            closestShoe.Rotate(rot);
        }

        public void OnFingerUp(LeanFinger finger)
        {
            if (closestShoe == null) return;

            if (!takeInput) return;

            Tween move = closestShoe.transform.DOMove(closestShoe.startPos, .25f);
            Tween rotate = closestShoe.transform.DORotate(closestShoe.startRot.eulerAngles, .25f);
            DOTween.Sequence().Append(move).Join(rotate)
                .Play();
            closestShoe = null;
        }
    }
}
