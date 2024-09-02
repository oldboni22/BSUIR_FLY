using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Pryanik
{
    public interface ISceneController
    {
        public event Action OnMenuOpened, OnGameplayOpened, OnSceneChanged;
        bool IsLoading { get; }
        void OpenMenu();
        void OpenGameplay();
        void CloseGame();
    }
    public class SceneController : MonoBehaviour, ISceneController
    {
        [Header("SceneNames")]
        [SerializeField] private string _menuName;
        [SerializeField] private string _gameplayName;

        public event Action OnMenuOpened, OnGameplayOpened;
        public event Action OnSceneChanged;

        [Header("Transition")]
        [SerializeField] private Image _transitionImage;
        [SerializeField] private float _transitionDuration;
        private bool _canUnhide;

        [SerializeField] private bool _isLoading = false;
        public bool IsLoading => _isLoading;

        public void OpenGameplay()
        {
            Cursor.visible = false;
            StartCoroutine(OpenScene(_gameplayName,OnGameplayOpened));
        }

        public void OpenMenu()
        {
            Cursor.visible = true;
            StartCoroutine(OpenScene(_menuName,OnMenuOpened));
        }
        IEnumerator OpenScene(string name, Action openEvent)
        {
            
            Time.timeScale = 1;
            OnSceneChanged();
            _isLoading = true;
            var handle = SceneManager.LoadSceneAsync(name);
            HideScreen();
            handle.allowSceneActivation = false;

            while (_canUnhide is false)
                yield return null;

            
            handle.allowSceneActivation = true;
            

            while (handle.progress <= .99f)
                yield return null;

            UnHideScreen();
            openEvent();
        }
        
        void HideScreen()
        {
            _canUnhide = false;
            var col = _transitionImage.color;
            col.a = 1;

            _transitionImage.gameObject.SetActive(true);
            _transitionImage.DOColor(col, _transitionDuration).OnComplete(() => _canUnhide = true);
        }
        void UnHideScreen()
        {
            var col = _transitionImage.color;
            col.a = 0;

            _transitionImage.DOColor(col, _transitionDuration).OnComplete(() =>
            {
                _transitionImage.gameObject.SetActive(false);
                _isLoading = false;

            });
        }

        public void CloseGame()
        {
            StartCoroutine(Close());
        }

        private IEnumerator Close()
        {
            
            _isLoading = true;
            Time.timeScale = 1;
            HideScreen();
            while (_canUnhide is false)
                yield return null;
            Application.Quit();
        }
    }
}