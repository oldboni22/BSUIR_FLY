using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pryanik.Res
{


    public class Storage<T> : ScriptableObject where T : Storeable
    {
        [SerializeField] private T[] _members;
        public T GetById(string id) => _members.Single(m => m.Id == id);
        public T GetRandom() => _members.OrderBy(r => Random.Range(0, 100)).First();
        public IEnumerable<T> GetAll() => _members;
        public int Count => _members.Length;
    }
}