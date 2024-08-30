

using Pryanik.PlayerSprite;
using System.Collections.Generic;

namespace Pryanik.PlayerStateMachine
{
    public abstract class PlayerState
    {
        protected float InverseMultiplyer => _player._inversed ? -1 : 1;
        protected readonly Player _player;
        protected PlayerState(Player player) => _player = player;

        public virtual void OnUpdate()
        {

        }
        public virtual void OnFixedUpdate()
        {

        }
        public virtual void OnClick()
        {

        }
        public virtual void OnRelease()
        {

        }

        public abstract void OnStateLeave();
        public abstract void OnStateEnter();
    }
    public class PlayerStateMachine
    {

        private readonly Dictionary<PlayerStateEnum, PlayerState> _states;
        private PlayerState _curState;
        private readonly PlayerSpriteController _spriteController;


        public PlayerStateMachine(Player player, PlayerSpriteController spriteController)
        {
            _spriteController = spriteController;
            _states = new Dictionary<PlayerStateEnum, PlayerState>
            {
                { PlayerStateEnum.square, new SquareState(player) },
                { PlayerStateEnum.arrow, new ArrowState(player) },
                { PlayerStateEnum.circle, new CircleState(player) },
            };
        }
        public void EnterState(PlayerStateEnum state)
        {
            _spriteController.SetSprite(state);
            _curState?.OnStateLeave();
            _curState = _states[state];
            _curState.OnStateEnter();
        }
        public void OnUpdate()
        {
            _curState.OnUpdate();
        }
        public void OnFixedUpdate()
        {
            _curState.OnFixedUpdate();
        }
        public void OnClick()
        {
            _curState.OnClick();
        }
        public void OnRelease()
        {
            _curState.OnRelease();
        }
    }
}