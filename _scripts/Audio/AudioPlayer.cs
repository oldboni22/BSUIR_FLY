using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pryanik.Audio
{
    [System.Serializable]
    public struct AudioPlayParams
    {
        public AudioClip clip;
        public int prio;
        public string id;

        internal AudioPlayParams(AudioControllerPlayParams @params, AudioClip clip)
        {
            this.clip = clip;
            id = @params.id;
            prio = @params.prio;
        }
    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] internal AudioSource _audioSource;
        private string _id;
        internal AudioPool _pool;

#nullable enable
        internal void OnCancelCall(string? id)
        {
            if (id == null ^ _id == id)
                Reset();
        }
#nullable disable
        internal void PlayAudio(AudioPlayParams @params)
        {
            
            _audioSource.clip = @params.clip;
            _audioSource.priority = @params.prio;
            _id = @params.id;
            _audioSource.Play();
            gameObject.SetActive(true);

        }
        private void Update()
        {
            if (_audioSource.isPlaying is false && gameObject.activeInHierarchy)
                Reset();
        }
        private void Reset()
        {
            gameObject.SetActive(false);
            _id = null;
            _pool.Despawn(this);
        }
    }

    public class AudioPool : MonoMemoryPool<AudioPlayer>
    {
        private event Action<string> _onCancellCall;
        protected override void OnCreated(AudioPlayer item)
        {
            item._audioSource.volume = PlayerPrefsManager.Volume;
            item._pool = this;
            _onCancellCall += item.OnCancelCall;
        }
        public void CancellAudio(string id) => _onCancellCall.Invoke(id);
    }
}