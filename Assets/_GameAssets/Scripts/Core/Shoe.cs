using UnityEngine;

namespace JuggleMaster
{
    public class Shoe : MonoBehaviour
    {
        public Vector3 startPos;
        public Quaternion startRot;

        private void Awake()
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
    }
}
