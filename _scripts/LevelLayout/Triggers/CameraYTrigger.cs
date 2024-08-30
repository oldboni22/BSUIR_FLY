using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pryanik.Layout
{

    public class CameraYTrigger : CameraTrigger
    {
        [SerializeField] private float _y;
        [SerializeField] private float _speed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _camera.SetPosY(_y);
            if (_speed > 0)
                _camera.SetSpeedY(_speed);

        }
    }
}