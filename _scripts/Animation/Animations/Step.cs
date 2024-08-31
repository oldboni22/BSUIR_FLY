using UnityEngine;

namespace Pryanik.Animations.UI
{

    internal enum AnimationType
    {
        Move,Scale
    }


    [System.Serializable]
    internal struct AnimationStep
    {
        [Header("Parameters")]
        [SerializeField] internal float _duration;
        [SerializeField] internal Vector3 _target;
        [SerializeField] internal AnimationType _type;

    }
}
