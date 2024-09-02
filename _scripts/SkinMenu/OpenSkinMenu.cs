using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pryanik.Skinmenu
{

    public class OpenSkinMenu : MonoBehaviour
    {
        [Inject] private ISkinMenuController _controller;
        public void OpenMenu() => _controller.DisplayMenu();
    }
}