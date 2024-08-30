using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Pryanik.Res;
using Pryanik.PlayerSprite;
using Zenject;
using System;

public enum PlayerStateEnum
{
    square, arrow, circle
}
public interface IPlayer
{
    public void SetPlayerConfig(string id);
    public void SetInverse(bool val);
}
namespace Pryanik
{
    public class Player : MonoBehaviour, IPlayer
    {
        [System.Serializable]
        public struct Config
        {
            [SerializeField] internal float _speed;
            [SerializeField] internal float _jump;
            [SerializeField] internal PlayerStateEnum _state;
        }

        private bool _isPaused;
        private PlayerStateMachine.PlayerStateMachine _stateMachine;

        #region Inject
        [Inject] private SkinStorage _skinStorage;
        [Inject] private PlayerConfigStorage _playerConfigStorage;
        [Inject] internal IPlayerGravityUi _gravityUi;

        [Inject]
        public void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.OnStart += OnStart;
            gamePlaySceneController.OnFail += Fail;
        }
        #endregion

        #region Components
        [SerializeField] private PlayerSpriteController _spriteController;
        [SerializeField] internal Rigidbody2D _rb;
        #endregion

        internal bool _inversed = false;
        internal Config _config;

        


        private void Start()
        {
            _spriteController.SetSprite(_skinStorage.GetRandom().Sprite);
        }
        void OnStart()
        {
            _gravityUi.UnHide();
            _isPaused = false;

            transform.position = Vector2.zero;
            _rb.velocity = Vector2.zero;

            _stateMachine ??= new PlayerStateMachine.PlayerStateMachine(this, _spriteController);
            _spriteController.gameObject.SetActive(true);

            SetPlayerConfig("square");
            SetInverse(false);
            
        }
        private void Update()
        {
            if (_isPaused) return;
            _stateMachine.OnUpdate();
        }
        private void FixedUpdate()
        {
            if (_isPaused) return;

            _rb.position += Vector2.right * _config._speed * Time.fixedDeltaTime;
            _stateMachine.OnFixedUpdate();
        }
        public void OnClick(InputAction.CallbackContext ctx)
        {
            if (_isPaused) return;

            if (ctx.performed)
                Click();
            if (ctx.canceled)
                Release();
        }
        public void SetInverse(bool val)
        {
            _inversed = val;
            float valMult = val ? -1 : 1;
            float newGravityScale = Mathf.Abs(_rb.gravityScale) * valMult;

            _gravityUi.SetGravity(valMult);
            _rb.gravityScale = newGravityScale;
        }
        void Click()
        {
            _stateMachine.OnClick();
            Debug.Log("Click");
        }
        void Release()
        {
            _stateMachine.OnRelease();
            Debug.Log("Release");
        }


        #region Set
        public void SetPlayerConfig(string id)
        {
            _config = _playerConfigStorage.GetById(id).Config;
            _stateMachine.EnterState(_config._state);
        }
        #endregion
        void Fail()
        {
            _isPaused = true;
            _spriteController.gameObject.SetActive(false);
        }
        


    }
}