using Pryanik.Audio;
using Pryanik.Checkpoint;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Pryanik
{
    public interface ISettingsController
    {
        event Action OnOpened;
        event Action OnClosed;
        void SubscribeCheckPoint(ICheckPointManager checkPointManager);
    }
    public class SettingsController : MonoBehaviour, ISettingsController
    {
        [SerializeField] private Toggle _checkpointsToggle;
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _gameplayOnly;

        private bool _showCursor;
        private ICheckPointManager _checkPointManager;

        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private VolumeSlider _volumeSlider;

        private ISceneController _sceneController;

        [Inject]
        public void Inject(ISceneController sceneController, AudioPool audio)
        {
            _volumeSlider.SetPool(audio);
            _sceneController = sceneController; 
        }

        private void Start()
        {
            Debug.Log("start");
            _exitButton.onClick.AddListener(_sceneController.CloseGame);

            _menuButton.onClick.AddListener(_sceneController.OpenMenu);
            
            _sceneController.OnMenuOpened += () =>
            {
                _gameplayOnly.SetActive(false);
                _checkpointsToggle.isOn = false;
            };
            _sceneController.OnGameplayOpened += () =>
            {

                _gameplayOnly.SetActive(true);
            };
            _sceneController.OnSceneChanged += () =>
            { 
                _checkPointManager = null;
                _menu.SetActive(false);
                _isOpened = false;
            };
        }


        [SerializeField] private bool _isOpened = false;

        public event Action OnOpened;
        public event Action OnClosed;

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            if (_sceneController.IsLoading)
                return;

            if (ctx.performed)
            {
                SwitchState();
            }
        }

        public void OnCheckpointToggle(bool val)
        {
            _checkPointManager?.ToggleCheckpointMode(val);
        }


        void SwitchState()
        {

            if (_isOpened)
                Close();
            else
                Open();
        }

        void Open()
        {
            _showCursor = Cursor.visible;
            Cursor.visible = true;

            _menu.SetActive(true);
            Time.timeScale = 0;

            _isOpened = true;
            OnOpened?.Invoke();
        }
        void Close()
        {
            Cursor.visible = _showCursor;

            _menu.SetActive(false);
            Time.timeScale = 1;

            _isOpened = false;
            OnClosed?.Invoke();
        }

        public void SubscribeCheckPoint(ICheckPointManager checkPointManager)
        {
            _checkPointManager = checkPointManager;
        }
    }
}