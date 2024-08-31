using Pryanik.Audio;
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
    }
    public class SettingsController : MonoBehaviour, ISettingsController
    {
        [SerializeField] private GameObject _menu;
        [SerializeField] private GameObject _gameplayOnly;


        [SerializeField] private Button _menuButton;
        [SerializeField] private VolumeSlider _volumeSlider;

        private ISceneController _sceneController;

        [Inject] public void Inject(ISceneController sceneController, AudioPool audio)
        {
            _volumeSlider.SetPool(audio);
            _sceneController = sceneController;

            _menuButton.onClick.AddListener(() => 
            { 
                _sceneController.OpenMenu();
                _menu.SetActive(false);
                _isOpened = false;
            });
            _sceneController.OnMenuOpened += () =>
            {
                _gameplayOnly.SetActive(false);
            };
            _sceneController.OnGameplayOpened += () =>
            {
                _gameplayOnly.SetActive(true);
            };
        }

        private bool _isOpened = false;

        public event Action OnOpened;
        public event Action OnClosed;

        public void OnEsc(InputAction.CallbackContext ctx)
        {
            Debug.Log(_sceneController.IsLoading);
            if (_sceneController.IsLoading)
                return;

            if (ctx.performed)
            {
                SwitchState();
            }
        }

            
        
        void SwitchState()
        {
            _isOpened = !_isOpened;
            _menu.SetActive(_isOpened);
            Time.timeScale = _isOpened ? 0 : 1;

            if (_isOpened) OnOpened?.Invoke();
            else OnClosed?.Invoke();
        }
    }
}