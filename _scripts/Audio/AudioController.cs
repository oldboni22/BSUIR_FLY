using Pryanik.Res;
using UnityEngine;
using Zenject;

namespace Pryanik.Audio
{
    public struct AudioControllerPlayParams 
    {
        public string id;
        public int prio;
    }
    public interface IAudioController
    {
        void PlayAudio(AudioControllerPlayParams @params);
        void StopAudio(string id); 
    }
    public class AudioController : MonoBehaviour, IAudioController
    {
        [Inject] private AudioPool _pool;
        [Inject] private AudioStorage _storage;

        public void PlayAudio(AudioControllerPlayParams @params)
        {
            AudioClip clip = _storage.GetById(@params.id).Clip;
            _pool.Spawn().PlayAudio(new AudioPlayParams(@params,clip));
        }

#nullable enable
        public void StopAudio(string? id)
        {
            _pool.CancellAudio(id);
        }
#nullable disable
    }
}