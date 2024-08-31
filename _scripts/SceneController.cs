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
        public event Action OnMenuOpened, OnGameplayOpened;
        bool IsLoading { get; }
        void OpenMenu();
        void OpenGameplay();
    }
    public class SceneController : MonoBehaviour, ISceneController
    {
        [Header("SceneNames")]
        [SerializeField] private string _menuName;
        [SerializeField] private string _gameplayName;

        
        public event Action OnMenuOpened, OnGameplayOpened;


        [Header("Transition")]
        [SerializeField] private Image _transitionImage;
        [SerializeField] private float _transitionDuration;
        private bool _canUnhide;

        [SerializeField] private bool _isLoading = false;
        public bool IsLoading => _isLoading;

        public void OpenGameplay()
        {
            OnGameplayOpened?.Invoke();
            StartCoroutine(OpenScene(_gameplayName));
        }

        public void OpenMenu()
        {
            OnMenuOpened?.Invoke();
            StartCoroutine(OpenScene(_menuName));
        }
        IEnumerator OpenScene(string name)
        {
            Time.timeScale = 1;
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
            _isLoading = false;

        }
        
        void HideScreen()
        {
            _canUnhide = false;
            Debug.Log("");
            var col = _transitionImage.color;
            col.a = 1;

            _transitionImage.gameObject.SetActive(true);
            _transitionImage.DOColor(col, _transitionDuration).OnComplete(() => _canUnhide = true);
        }
        void UnHideScreen()
        {
            var col = _transitionImage.color;
            col.a = 0;

            _transitionImage.DOColor(col, _transitionDuration).OnComplete(() => _transitionImage.gameObject.SetActive(false));
        }
    }
}