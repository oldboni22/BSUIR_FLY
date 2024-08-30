using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pryanik.PlayerSprite
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerSprite : MonoBehaviour
    {
        [Inject] private IGamePlaySceneController _gamePlaySceneController;

        [SerializeField] internal PlayerStateEnum _state;
        [SerializeField] private float _size;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _col;

        internal void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.size = Vector2.one * _size;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _gamePlaySceneController.Fail();
        }
    }
}