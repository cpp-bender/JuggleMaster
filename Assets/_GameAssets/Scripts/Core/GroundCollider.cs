using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class GroundCollider : MonoBehaviour
    {
        [Header("EVENTS")]
        public VoidEventChannelSO levelFailEvent;

        private void OnCollisionEnter(Collision collision)
        {
            Vase vase;
            if (collision.gameObject.TryGetComponent(out vase))
            {
                levelFailEvent.Raise();
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
