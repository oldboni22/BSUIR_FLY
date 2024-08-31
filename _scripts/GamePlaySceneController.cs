using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Pryanik
{
    public interface IPauseable
    {
        void Pause();
        void UnPause();
    }
    public interface IGamePlaySceneController
    {
        event Action OnStart;
        event Action OnFail;
        event Action OnSceneClosed;
        void PauseSubscribe(IPauseable pauseable);
        void Fail();
    }

    public class GamePlaySceneController : MonoBehaviour, IGamePlaySceneController
    {

        [SerializeField] private float _timeToRestart;
        [Inject] private ISettingsController _settingsController;

        public event Action OnStart;
        public event Action OnFail;
        public event Action OnSceneClosed;

        private event Action _onPause;
        private event Action _onUnPause;
        

        public void Fail()
        {
            StartCoroutine(Restart());
        }

        public void PauseSubscribe(IPauseable pauseable)
        {
            _onPause += pauseable.Pause;
            _onUnPause += pauseable.UnPause;
        }

        IEnumerator Restart()
        {
            OnFail();
            yield return new WaitForSeconds(_timeToRestart);
            OnStart();
        }


        void Start()
        {
            OnStart();

            _settingsController.OnOpened += _onPause;
            _settingsController.OnClosed += _onUnPause;
        }
        private void OnDisable ()
        {
            _settingsController.OnOpened -= _onPause;
            _settingsController.OnClosed -= _onUnPause;
            OnSceneClosed?.Invoke();
        }

    }
}