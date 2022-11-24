using UnityEngine;

namespace JuggleMaster
{
    public class Shoe : MonoBehaviour
    {
        public Vector3 startPos;
        public Quaternion startRot;
        public float power;

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

        public void SetPower(Vector2 delta)
        {
            power = delta.y;
            power = Mathf.Clamp(power, 0f, 1f);
        }
    }
}
