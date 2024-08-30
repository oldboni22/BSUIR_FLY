using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pryanik.Layout
{
    internal struct TriggerParams
    {
        internal IPlayer player;
        internal IPlayerCamera camera;
    }
    public abstract class LevelTrigger : MonoBehaviour
    {
        [SerializeField] protected bool _destroyOnTouch;
        internal abstract void SetParameters(TriggerParams @params);
    }
}