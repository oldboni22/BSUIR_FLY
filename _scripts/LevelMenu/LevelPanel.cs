using Pryanik.Progress;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pryanik.LevelMenu
{
    public class LevelPanel : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _normalTries, _practiceTries, _levelName;
        
        private ISceneController _sceneController;
        internal string _lvlId;

        public void SetSceneController(ISceneController sceneController) => _sceneController = sceneController;
        public void OpenLevel()
        {
            PlayerPrefsManager.LevelId = _lvlId;
            _sceneController.OpenGameplay();
        }


        internal void SetParams(LevelProgress progress)
        {
            _slider.value = progress.progress;
            _normalTries.text = $"Normal tries: \n {progress.normalTriesCount}";
            _practiceTries.text = $"Practice tries: \n {progress.practiceTriesCount}";
        }
    }
}