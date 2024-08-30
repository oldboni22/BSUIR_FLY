using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pryanik.Res
{


    public abstract class Storeable : ScriptableObject
    {
        [SerializeField] private string _id;
        public string Id => _id;
    }
}