using SimpleEvent;
using UnityEngine;

namespace HyperBase
{
	[DefaultExecutionOrder(-100)]
	public class UIManager : MonoBehaviour
	{
		public VoidEventChannelSO gameInitEvent;

		private void OnEnable()
		{
			gameInitEvent.Event += OnGameEvent;
        }

        private void OnDisable()
		{
			gameInitEvent.Event -= OnGameEvent;
        }

        private void OnGameEvent()
		{
			Debug.Log("Game init for ui");
		}
	}
}