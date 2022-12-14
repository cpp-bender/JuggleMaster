using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class Shoe : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public Vector3 startPos;
        public Quaternion startRot;

        [Header("EVENTS")]
        public BoolEventChannelSO stallUpdateEvent;

        [Header("DEBUG")]
        public float power;

        private Vector3 vel = Vector3.zero;

        private void Awake()
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vase vase;
            if (collision.gameObject.TryGetComponent(out vase) && power > 0f)
            {
                vase.Kick(power);
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            Vase vase;
            if (collision.gameObject.TryGetComponent(out vase))
            {
                stallUpdateEvent.Raise(true);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            Vase vase;
            if (collision.gameObject.TryGetComponent(out vase))
            {
                stallUpdateEvent.Raise(false);
            }
        }

        public void Move(Vector3 wPos)
        {
            transform.position = Vector3.SmoothDamp(transform.position, wPos, ref vel, .1f);
        }

        public void Rotate(Quaternion rot)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, 5f * Time.deltaTime);
        }

        public void SetPower(Vector2 delta)
        {
            power = delta.y;
            power = Mathf.Clamp(power, 0f, 1f);
        }
    }
}
