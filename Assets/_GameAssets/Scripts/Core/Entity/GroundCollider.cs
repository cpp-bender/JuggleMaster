using SimpleEvent;
using UnityEngine;

namespace JuggleMaster
{
    public class GroundCollider : MonoBehaviour
    {
        [Header("COMPONENTS")]
        public Collider col;

        [Header("EVENTS")]
        public VoidEventChannelSO levelFailEvent;
        public VoidEventChannelSO levelWinEvent;

        private bool canCollide = true;

        private void OnEnable()
        {
            levelWinEvent.Event += OnLevelWin;
        }

        private void OnDisable()
        {
            levelWinEvent.Event -= OnLevelWin;
        }

        private void OnLevelWin()
        {
            canCollide = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vase vase;
            if (canCollide && collision.gameObject.TryGetComponent(out vase))
            {
                vase.gameObject.SetActive(false);
                levelFailEvent.Raise();
                col.enabled = false;
            }
        }
    }
}
