using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Pryanik.PlayerSprite
{
    public class PlayerSpriteController : MonoBehaviour
    {
        [SerializeField] private PlayerSprite[] _sprites;
        private PlayerSprite _curSprite;
        public void SetSprite(Sprite sprite)
        {
            foreach(var s in _sprites)
                s.SetSprite(sprite);
        }

        public void SetSprite(PlayerStateEnum state)
        {
            if(_curSprite != null)
                _curSprite.gameObject.SetActive(false);
            _curSprite = _sprites.Single(s => s._state == state);
            _curSprite.gameObject.SetActive(true);
        }
    }
}