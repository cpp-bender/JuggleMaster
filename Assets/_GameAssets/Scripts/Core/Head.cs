using UnityEngine;

namespace JuggleMaster
{
    public class Head : MonoBehaviour
    {
        public Vase vase;

        private void Start()
        {
            vase = FindObjectOfType<Vase>();
        }

        private void FixedUpdate()
        {
            var lookDir = (vase.transform.position - transform.position).normalized;
            Quaternion dir = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, 3f * Time.fixedDeltaTime);
        }
    }
}
