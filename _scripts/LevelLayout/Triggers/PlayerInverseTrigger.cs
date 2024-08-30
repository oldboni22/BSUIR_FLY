using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pryanik.Layout
{
    public class PlayerInverseTrigger : PlayerTrigger
    {

        [SerializeField] private bool _val;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _player.SetInverse(_val);
        }
    }
}