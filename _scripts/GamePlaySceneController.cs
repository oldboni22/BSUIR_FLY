using Pryanik.Checkpoint;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Pryanik
{
    public interface IStartable
    {
        void OnStart(bool practice);
        void OnFail(bool practice);
    }
    public interface IPauseable
    {
        void Pause();
        void UnPause();
    }
    public interface IGamePlaySceneController
    {
        void StartSubscribe(IStartable startable);
        void PauseSubscribe(IPauseable pauseable);
        void Fail();
    }

    public class GamePlaySceneController : MonoBehaviour, IGamePlaySceneController, IToggleSubscriber
    {

        [SerializeField] private float _timeToRestart;
        [Inject] private ISettingsController _settingsController;
        [Inject] private ICheckPointManager _checkPointManager;
        [Inject] private ISceneController _sceneController;

        private bool _checkpoint = false;

        private event Action<bool> _onStart;
        private event Action<bool> _onFail;

        private event Action _onPause;
        private event Action _onUnPause;


        public void Fail()
        {
            StopAllCoroutines();
            StartCoroutine(Restart());
        }

        public void PauseSubscribe(IPauseable pauseable)
        {
            _onPause += pauseable.Pause;
            _onUnPause += pauseable.UnPause;
        }

        IEnumerator Restart()
        {
            yield return new WaitForSeconds(.01f);
            _onFail(_checkpoint);
            yield return new WaitForSeconds(_timeToRestart);
            _onStart(_checkpoint);
        }


        void Start()
        {
            _sceneController.OnSceneChanged += StopAllCoroutines;
            _onStart(false);

            _checkPointManager.CheckpointToggleSuscribe(this);
            _settingsController.OnOpened += _onPause;
            _settingsController.OnClosed += _onUnPause;
        }
        private void OnDisable ()
        {
            _checkPointManager.CheckpointToggleUnSuscribe(this);
            _settingsController.OnOpened -= _onPause;
            _settingsController.OnClosed -= _onUnPause;

            _sceneController.OnSceneChanged -= StopAllCoroutines;
        }

        public void StartSubscribe(IStartable startable)
        {
            _onStart += startable.OnStart;
            _onFail += startable.OnFail;
        }

        public void OnChechpointToggle(bool val)
        {
            _checkpoint = val;
            Fail();
        }
    }
}