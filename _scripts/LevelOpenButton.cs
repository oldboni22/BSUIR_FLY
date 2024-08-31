using UnityEngine;
using Zenject;

namespace Pryanik
{
    public class LevelOpenButton : MonoBehaviour
    {
        [Inject] ISceneController _controller;
        [SerializeField] private string _levelId;

        public void OpenLevel()
        {
            PlayerPrefsManager.LevelId = _levelId;
            _controller.OpenGameplay();
        }
    }
}