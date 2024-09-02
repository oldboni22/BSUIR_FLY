using Pryanik.Res;
using UnityEngine;

namespace Pryanik.Layout
{
    public struct LevelBuilderParams
    {
        public LayoutTriggers triggers;
        public GameObject obstacles;
    }

    [CreateAssetMenu(menuName ="Layout")]
    public class LevelLayout : Storeable
    {
        [SerializeField] internal LayoutTriggers _trigger;
        [SerializeField] internal GameObject _obstacles;
        [SerializeField] private float _endX;
        public float EndX => _endX;
    }
}