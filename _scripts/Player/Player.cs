using UnityEngine;
using UnityEngine.InputSystem;
using Pryanik.Res;
using Pryanik.PlayerSprite;
using Zenject;


namespace Pryanik
{
    [System.Serializable]
    public struct Config
    {
        [SerializeField] internal float _speed;
        [SerializeField] internal float _jump;
        [SerializeField] internal PlayerStateEnum _state;
    }
    public enum PlayerStateEnum
    {
        square, arrow, circle
    }
    public interface IPlayer
    {
        public void SetPlayerConfig(string id);
        public void SetInverse(bool val);
    }

    public class Player : MonoBehaviour, IPlayer, IPauseable
    {
        private bool _isPaused = false;
        private bool _isDead = false;
        private PlayerStateMachine.PlayerStateMachine _stateMachine;

        internal bool _inversed = false;
        internal Config _config;

        #region Inject
        [Inject] private SkinStorage _skinStorage;
        [Inject] private PlayerConfigStorage _playerConfigStorage;
        [Inject] internal IPlayerGravityUi _gravityUi;

        [Inject]
        public void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.OnStart += OnStart;
            gamePlaySceneController.OnFail += Fail;
            gamePlaySceneController.PauseSubscribe(this);
        }
        #endregion

        #region Components
        [SerializeField] private PlayerSpriteController _spriteController;
        [SerializeField] internal Rigidbody2D _rb;
        #endregion

        private void Start()
        {
            SetSkin();
        }

        private void Update()
        {
            if (_isPaused || _isDead) return;
            _stateMachine.OnUpdate();
        }
        private void FixedUpdate()
        {
            if (_isPaused || _isDead) return;

            _rb.position += Vector2.right * _config._speed * Time.fixedDeltaTime;
            _stateMachine.OnFixedUpdate();
        }

        #region Set
        public void SetInverse(bool val)
        {
            _inversed = val;
            float valMult = val ? -1 : 1;
            float newGravityScale = Mathf.Abs(_rb.gravityScale) * valMult;

            _gravityUi.SetGravity(valMult);
            _rb.gravityScale = newGravityScale;
        }
        void SetSkin()
        {
            Sprite sprite;
            var id = PlayerPrefsManager.SkinId;

            if (id == "")
                sprite = _skinStorage.GetRandom().Sprite;
            else
                sprite = _skinStorage.GetById(id).Sprite;

            _spriteController.SetSprite(sprite);
        }

        public void SetPlayerConfig(string id)
        {
            _config = _playerConfigStorage.GetById(id).Config;
            _stateMachine.EnterState(_config._state);
        }
        #endregion

        #region Events
        public void OnClick(InputAction.CallbackContext ctx)
        {
            if (_isPaused || _isDead) return;

            if (ctx.performed)
                Click();
            if (ctx.canceled)
                Release();
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

        void Fail()
        {
            _isDead = true;
            _spriteController.gameObject.SetActive(false);
        }
        void OnStart()
        {
            _gravityUi.UnHide();
            _isDead = false;

            transform.position = Vector2.zero;
            _rb.velocity = Vector2.zero;

            _stateMachine ??= new PlayerStateMachine.PlayerStateMachine(this, _spriteController);
            _spriteController.gameObject.SetActive(true);

            SetPlayerConfig("square");
            SetInverse(false);

        }
        public void Pause()
        {
            _isPaused = true;
        }

        public void UnPause()
        {
            _isPaused = false;
        }
        #endregion
    }
}