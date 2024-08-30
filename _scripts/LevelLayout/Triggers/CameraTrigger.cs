using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pryanik.Layout
{
    public class CameraTrigger : LevelTrigger
    {
        protected IPlayerCamera _camera;
        internal override void SetParameters(TriggerParams @params)
        {
            _camera = @params.camera;
        }
    }
}