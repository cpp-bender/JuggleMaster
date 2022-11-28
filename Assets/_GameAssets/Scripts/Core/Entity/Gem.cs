using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class Gem : MonoBehaviour
    {
        [Header("EVENTS")]
        public Vector3EventChannelSO collectGemEvent;

        private void FixedUpdate()
        {
            transform.Rotate(Vector3.up, Space.Self);
        }

        private void OnTriggerEnter(Collider other)
        {
            Vase vase;
            if (other.gameObject.TryGetComponent(out vase))
            {
                collectGemEvent.Raise(transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}
