using System;
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
        private bool _isPaused = false;
        internal AudioPool _pool;

#nullable enable
        internal void OnCancelCall(string? id)
        {
            if(gameObject.activeInHierarchy)
                if (id == null ^ _id == id)
                    Reset();
        }
        internal void OnPauseCall(string? id,bool pause)
        {
            if (gameObject.activeInHierarchy)
                if (id == null ^ _id == id)
                    if (pause)
                    {
                        _isPaused = true;
                        _audioSource.Pause();
                    }
                    else
                    {
                        _isPaused = false;
                        _audioSource.UnPause();
                    }
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
        internal void OnVolumeChanged(float val) => _audioSource.volume = val;
        private void Update()
        {
            if (gameObject.activeInHierarchy && _audioSource.isPlaying is false && _isPaused is false)
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
        private event Action<string,bool> _onPauseCall;
        private event Action<float> _onVolChangedCall;
        protected override void OnCreated(AudioPlayer item)
        {
            item._audioSource.volume = PlayerPrefsManager.Volume;
            item._pool = this;

            _onPauseCall += item.OnPauseCall;
            _onVolChangedCall += item.OnVolumeChanged;
            _onCancellCall += item.OnCancelCall;
        }
        public void CancellAudio(string id) => _onCancellCall.Invoke(id);
        public void ChangeVolume(float val) => _onVolChangedCall.Invoke(val);
        public void SetPause(string id, bool val) => _onPauseCall.Invoke(id, val);
    }
}