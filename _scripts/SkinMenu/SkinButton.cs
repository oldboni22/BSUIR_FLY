using Pryanik.Res;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pryanik.Skinmenu
{
    public class SkinButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private string _skinId;

        internal void SetSkin(PlayerSkin skin)
        {
            _skinId = skin.Id;
            _button.image.sprite = skin.Sprite;
        }

        public void OnClick()
        {
            PlayerPrefsManager.SkinId = _skinId;
        }

    }
}