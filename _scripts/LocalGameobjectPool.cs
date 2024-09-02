using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pryanik
{

    public class LocalGameobjectPool
    {
        private readonly GameObject _spawnObj;
        private readonly List<GameObject> _list = new List<GameObject>();

        public LocalGameobjectPool(GameObject spawnObj)
        {
            _spawnObj = spawnObj;
        }

        public GameObject Spawn(Vector2 pos)
        {
            if (_list.Count == 0 || _list.All(o => o.activeInHierarchy))
            {
                var newObj = GameObject.Instantiate(_spawnObj, pos, new Quaternion());
                _list.Add(newObj);
                return newObj;
            }
            else
            {
                GameObject o = _list.First(o => o.activeInHierarchy is false);
                o.SetActive(true);
                o.transform.position = pos;
                return o;
            }
        }
        public void DispawnAll()
        {
            foreach (var o in _list)
                o.SetActive(false);
        }
    

    }
}