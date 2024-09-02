using UnityEngine;


namespace Pryanik.Layout
{
    public class PlayerInverseXTrigger : PlayerTrigger
    {
        [SerializeField] private bool _inverseX;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _player.SetInverseX(_inverseX);
        }
    }
}