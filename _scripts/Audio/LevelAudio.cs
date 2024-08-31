using Pryanik.Audio;
using UnityEngine;
using Zenject;


namespace Pryanik
{
    public class LevelAudio : MonoBehaviour, IPauseable
    {
        [Inject] private IAudioController _audio;
        private string _id;


        [Inject]
        private void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.OnStart += OnStart;
            gamePlaySceneController.OnFail += OnFail;
            gamePlaySceneController.PauseSubscribe(this);
            gamePlaySceneController.OnSceneClosed += () => _audio.StopAudio(_id);
        }

        

        void OnStart()
        {
            _id ??= PlayerPrefsManager.LevelId;

            _audio.StopAudio(null);
            _audio.PlayAudio(new AudioControllerPlayParams
            {
                id = _id,
                prio = 0,
            }) ;
        }

        void OnFail()
        {
            _audio.StopAudio(null);
            _audio.PlayAudio(new AudioControllerPlayParams
            {
                id = "fail",
                prio = 0,
            });
        }

        public void Pause()
        {
            _audio.SetPause(_id,true);
        }

        public void UnPause()
        {
            _audio.SetPause(_id, false);
        }
    }
}