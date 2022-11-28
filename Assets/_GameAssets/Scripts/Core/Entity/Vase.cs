using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class Vase : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public Rigidbody body;
        public Collider col;

        [Header("EVENTS")]
        public VoidEventChannelSO gameInitEvent;
        public VoidEventChannelSO gameStartEvent;
        public VoidEventChannelSO kickVaseEvent;

        private void OnEnable()
        {
            gameInitEvent.Event += OnGameInit;
            gameStartEvent.Event += OnGameStart;
        }

        private void OnDisable()
        {
            gameInitEvent.Event -= OnGameInit;
            gameStartEvent.Event -= OnGameStart;
        }

        private void OnGameStart()
        {
            body.isKinematic = false;
        }

        private void OnGameInit()
        {
            body.isKinematic = true;
        }

        public void Kick(float power)
        {
            float force = Mathf.Lerp(3f, 8f, power);
            body.AddForce(force * Vector3.up, ForceMode.Impulse);
            kickVaseEvent.Raise();
        }
    }
}
