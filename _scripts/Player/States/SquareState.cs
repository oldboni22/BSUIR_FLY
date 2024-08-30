using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pryanik.PlayerStateMachine
{


    public class SquareState : PlayerState
    {
        public SquareState(Player player) : base(player)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log("square enter");
        }

        public override void OnStateLeave()
        {
            Debug.Log("square left");
        }
        public override void OnClick()
        {
            Debug.Log(InverseMultiplyer);
            _player._rb.velocity = Vector2.up * _player._config._jump * InverseMultiplyer;
        }
    }
}