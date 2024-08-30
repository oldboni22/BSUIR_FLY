using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pryanik
{
    public interface IPlayerGravityUi
    {
        void SetGravity(float val);
        void Hide();
        void UnHide();
    }
    public class PlayerGravityUi : MonoBehaviour, IPlayerGravityUi
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _rotatTime;
        [SerializeField] private float _hideTime;

        public void SetGravity(float val)
        {
            Vector3 newRotat = new Vector3(0,0,90 * val);
            _image.transform.DORotate(newRotat, _rotatTime);
        }

        public void Hide()
        {
            var newCol = _image.color;
            newCol.a = 0;
            _image.DOColor(newCol,_hideTime);
        }

        public void UnHide()
        {
            var newCol = _image.color;
            newCol.a = 1;
            _image.DOColor(newCol, _hideTime);
        }
    }
}