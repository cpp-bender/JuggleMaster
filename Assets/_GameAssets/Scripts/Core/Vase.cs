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
            body.AddForce(5f * Vector3.up, ForceMode.Impulse);
            Debug.Log("Kicked");
        }
    }
}
