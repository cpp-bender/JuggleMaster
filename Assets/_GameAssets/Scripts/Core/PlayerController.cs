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

        [Header("DEBUG")]
        public Shoe closestShoe;

        private void OnDestroy()
        {
            DOTween.Kill(transform);
        }

        public void OnFingerDown(Vector3 pos)
        {
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

            Vector3 desPos = finger.GetWorldPosition(5f);
            desPos = new Vector3(Mathf.Clamp(desPos.x, -1f, 1f), Mathf.Clamp(desPos.y, .1f, 2f), 5f);
            closestShoe.Move(desPos);

            Quaternion rot = Quaternion.LookRotation(desPos - closestShoe.startPos).normalized;
            closestShoe.Rotate(rot);
        }

        public void OnFingerUp(LeanFinger finger)
        {
            if (closestShoe == null) return;

            Tween move = closestShoe.transform.DOMove(closestShoe.startPos, .25f);
            Tween rotate = closestShoe.transform.DORotate(closestShoe.startRot.eulerAngles, .25f);
            DOTween.Sequence().Append(move).Join(rotate)
                .Play();
            closestShoe = null;
        }
    }
}
