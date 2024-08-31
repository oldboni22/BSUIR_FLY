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
        void SetPause(string id,bool val);
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

        public void SetPause(string id, bool val)
        {
            _pool.SetPause(id,val);
        }

#nullable enable
        public void StopAudio(string? id)
        {
            _pool.CancellAudio(id);
        }
#nullable disable
    }
}