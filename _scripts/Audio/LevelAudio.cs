using Pryanik.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Pryanik
{
    public class LevelAudio : MonoBehaviour
    {
        [Inject] private IAudioController _audio;

        [Inject]
        private void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.OnStart += OnStart;
            gamePlaySceneController.OnFail += OnFail;
        }


        void OnStart()
        {
            _audio.StopAudio(null);
            _audio.PlayAudio(new AudioControllerPlayParams
            {
                id = PlayerPrefsManager.LevelId,
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
    }
}