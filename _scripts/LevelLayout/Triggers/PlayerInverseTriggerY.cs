using UnityEngine;

namespace Pryanik.Layout
{
    public class PlayerInverseTriggerY : PlayerTrigger
    {

        [SerializeField] private bool _val;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _player.SetInverseY(_val);
        }
    }
}