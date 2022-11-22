using SimpleEvent;
using UnityEngine;

namespace HyperBase
{
	public class CharacterController : MonoBehaviour
	{
		public VoidEventChannelSO gameInitEvent;

		private void OnEnable()
		{
			gameInitEvent.Event += OnGameInit;
		}

		private void OnDisable()
		{
			gameInitEvent.Event -= OnGameInit;
        }

        private void OnGameInit()
		{
			Debug.Log("Game init for fatman");
		}
	}
}
