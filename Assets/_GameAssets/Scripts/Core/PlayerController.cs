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

        public void FindClosestShoe(Vector3 pos)
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

        public void Move(LeanFinger finger)
        {
            Vector3 desPos = finger.GetWorldPosition(4f);
            desPos = new Vector3(Mathf.Clamp(desPos.x, -1f, 1f), Mathf.Clamp(desPos.y, .1f, 2f), 5f);
            closestShoe.transform.position = Vector3.Lerp(closestShoe.transform.position, desPos, 5f * Time.deltaTime);
        }

        public void Rotate(LeanFinger finger)
        {
            Vector3 desPos = finger.GetWorldPosition(4f);
            Quaternion rot = Quaternion.LookRotation(desPos - closestShoe.startPos).normalized;
            closestShoe.transform.rotation = Quaternion.Lerp(closestShoe.transform.rotation, rot, 5f * Time.deltaTime);
        }

        public void Stop(LeanFinger finger)
        {
            Tween move = closestShoe.transform.DOMove(closestShoe.startPos, .25f);
            Tween rotate = closestShoe.transform.DORotate(closestShoe.startRot.eulerAngles, .25f);
            DOTween.Sequence().Append(move).Join(rotate)
                .Play();
        }
    }
}
