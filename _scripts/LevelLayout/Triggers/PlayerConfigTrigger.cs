using UnityEngine;


namespace Pryanik.Layout
{
    public class PlayerConfigTrigger : PlayerTrigger
    {
        [SerializeField] private string _enterConfigId;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _player.SetPlayerConfig(_enterConfigId);
        }
    }
}