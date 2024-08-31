using UnityEngine;
namespace Pryanik.PlayerStateMachine
{


    public class CircleState : PlayerState
    {
        private float _curMultiplyer;
        public CircleState(Player player) : base(player)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log("circle enter");
            _player._gravityUi.Hide();
            _player._rb.velocity = Vector2.zero;
            _curMultiplyer = InverseMultiplyer;
        }

        public override void OnStateLeave()
        {
            Debug.Log("circle left");
            _player._gravityUi.UnHide();
            _player._rb.gravityScale = InverseMultiplyer;
        }
        public override void OnClick()
        {
            _player._rb.velocity -= _player._rb.velocity * .5f;
            _curMultiplyer *= -1;
            _player._rb.gravityScale = _curMultiplyer * _player._config._jump;
        }
    }
}