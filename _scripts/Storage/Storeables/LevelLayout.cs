using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pryanik.Res;

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
    }
}