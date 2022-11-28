using SimpleEvent;
using UnityEngine;
using System;

namespace JuggleMaster
{
    public class SoundManager : MonoBehaviour
    {
        [Header("DEPENDENCIES")]
        public AudioSource audioSource;

        [Header("EVENTS")]
        public VoidEventChannelSO kickVaseEvent;
        public VoidEventChannelSO levelFailEvent;
        public VoidEventChannelSO completeAchivementEvent;

        [Header("SOUNDS")]
        public Sound bounce;
        public Sound levelFail;
        public Sound success;
        public Sound hold;

        private void OnEnable()
        {
            kickVaseEvent.Event += OnVaseKicked;
            levelFailEvent.Event += OnLevelFailed;
            completeAchivementEvent.Event += OnAchivementComplete;
        }

        private void OnDisable()
        {
            kickVaseEvent.Event -= OnVaseKicked;
            levelFailEvent.Event -= OnLevelFailed;
            completeAchivementEvent.Event -= OnAchivementComplete;
        }

        private void OnAchivementComplete()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(success.clip);
        }

        private void OnVaseKicked()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(bounce.clip);
        }

        private void OnLevelFailed()
        {
            audioSource.pitch = 1f;
            audioSource.PlayOneShot(levelFail.clip);
        }

        [Serializable]
        public struct Sound
        {
            public AudioClip clip;
            public float pitch;
            public bool loop;
        }
    }
}
