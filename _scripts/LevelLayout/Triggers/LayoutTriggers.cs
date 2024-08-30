using Pryanik;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pryanik.Layout
{

    public class LayoutTriggers : MonoBehaviour
    {
        internal void SetParams(TriggerParams @params)
        {
            foreach(var t in GetComponentsInChildren<LevelTrigger>())
                t.SetParameters(@params);
        }

    }
}