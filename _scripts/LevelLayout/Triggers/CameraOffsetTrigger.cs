using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pryanik.Layout
{
    public class CameraOffsetTrigger :CameraTrigger
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _speed;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _camera.SetOffsetTarget(_offset);
            if(_speed > 0)
                _camera.SetSpeedX(_speed);
            
        }
    }
}