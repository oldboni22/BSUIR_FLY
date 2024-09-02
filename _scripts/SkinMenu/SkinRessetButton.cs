using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pryanik.Skinmenu
{

    public class SkinRessetButton : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                PlayerPrefsManager.SkinId = "";
            });
        }
    }
}