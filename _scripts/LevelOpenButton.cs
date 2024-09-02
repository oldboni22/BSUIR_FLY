using UnityEngine;
using Zenject;

namespace Pryanik.LevelMenu
{
    public class LevelOpenButton : MonoBehaviour
    {
        [Inject] ILevelMenuManager _controller;

        public void OpenMenu()
        {
            _controller.Open();
        }
    }
}