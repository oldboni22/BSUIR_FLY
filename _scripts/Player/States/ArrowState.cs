using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pryanik.PlayerStateMachine
{


    public class ArrowState : PlayerState
    {
        private float _heldMultiplyer;
        public ArrowState(Player player) : base(player)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log("arrow enter");
            _player._rb.velocity = Vector2.zero;
            _player._rb.gravityScale = 0;
            _heldMultiplyer = -1;
        }

        public override void OnStateLeave()
        {
            Debug.Log("arrow left");
            _player._rb.gravityScale = InverseMultiplyer;
        }

        public override void OnClick()
        {
            _heldMultiplyer = 1;
        }
        public override void OnRelease()
        {
            _heldMultiplyer = -1;
        }
        public override void OnFixedUpdate()
        {
            float posMultiplyer = _heldMultiplyer * _player._config._jump * Time.fixedDeltaTime;
            _player._rb.position -= Vector2.up * posMultiplyer * -InverseMultiplyer;
            
        }
    }
}