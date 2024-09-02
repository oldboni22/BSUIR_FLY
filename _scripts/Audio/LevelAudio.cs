using Pryanik.Audio;
using UnityEngine;
using Zenject;


namespace Pryanik
{
    public class LevelAudio : MonoBehaviour, IPauseable, IStartable
    {
        [Inject] private IAudioController _audio;
        private string _id;
        private bool _practice;

        [Inject]
        private void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.StartSubscribe(this);
            gamePlaySceneController.PauseSubscribe(this);
        }
        public void Pause()
        {
            if(_practice is false)
                _audio.SetPause(_id,true);
        }

        public void UnPause()
        {
            if (_practice is false)
                _audio.SetPause(_id, false);
        }

        public void OnStart(bool practice)
        {
            _id ??= PlayerPrefsManager.LevelId;

            _practice = practice;
            _audio.LoopAudio(new AudioControllerPlayParams
            {
                id = _id,
                prio = 0,
            }, false);

        }

        public void OnFail(bool practice)
        {
            if(practice is false)
                _audio.StopAudio(_id);

            _audio.PlayAudio(new AudioControllerPlayParams
            {
                id = "fail",
                prio = 0,
            });
        }
    }
}