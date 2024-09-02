using System.Linq;
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
        internal string _id;
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
        internal void PlayAudio(AudioPlayParams @params, bool loop)
        {
            _audioSource.loop = loop;
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
        private readonly List<AudioPlayer> _list = new List<AudioPlayer>(2);
        protected override void OnCreated(AudioPlayer item)
        {
            item._audioSource.volume = PlayerPrefsManager.Volume;
            item._pool = this;

            _list.Add(item);
        }
        public void CancellAudio(string id) { foreach(var audio in _list) audio.OnCancelCall(id); }
        public void ChangeVolume(float val) { foreach (var audio in _list) audio.OnVolumeChanged(val); }
        public void SetPause(string id, bool val) { foreach(var audio in _list) audio.OnPauseCall(id,val); }
        public bool IsPlaying(string id) => _list.Any(audio => audio._id == id);

}
}