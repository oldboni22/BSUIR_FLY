using UnityEngine;
using UnityEngine.InputSystem;
using Pryanik.Res;
using Pryanik.PlayerSprite;
using Zenject;
using Pryanik.Checkpoint;

namespace Pryanik
{

    public struct PlayerCheckointData
    {
        public Config config;
        public bool inverseY;
        public Vector2 pos;
    }

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
        public void SetInverseY(bool val);
        public void SetInverseX(bool val);
    }

    public class Player : MonoBehaviour, IPlayer, IPauseable, IStartable
    {

        private bool _setCheckPoints;
        private bool _isPaused = false;
        private bool _isDead = true;
        private PlayerStateMachine.PlayerStateMachine _stateMachine;

        internal bool _inversed = false;
        internal bool _inversedX = false;
        internal Config _config;

        #region Inject
        private ICheckPointManager _checkPointManager;
        [Inject] private SkinStorage _skinStorage;
        [Inject] private PlayerConfigStorage _playerConfigStorage;
        [Inject] internal IPlayerGravityUi _gravityUi;

        [Inject]
        public void Inject(IGamePlaySceneController gamePlaySceneController, ICheckPointManager checkPointManager)
        {
            gamePlaySceneController.StartSubscribe(this);
            gamePlaySceneController.PauseSubscribe(this);

            _checkPointManager = checkPointManager;
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
            _stateMachine?.OnUpdate();
        }
        private void FixedUpdate()
        {
            if (_isPaused || _isDead) return;

            if(_inversedX is false)
                _rb.position += Vector2.right * _config._speed * Time.fixedDeltaTime;
            else
                _rb.position -= Vector2.right * _config._speed * Time.fixedDeltaTime;

            _stateMachine?.OnFixedUpdate();
        }

        #region Set
        public void SetInverseY(bool val)
        {
            _inversed = val;
            float valMult = val ? -1 : 1;
            float newGravityScale = Mathf.Abs(_rb.gravityScale) * valMult;

            _gravityUi.SetGravity(valMult);
            _rb.gravityScale = newGravityScale;
        }
        public void SetInverseX(bool val)
        {
            _inversedX = val;
            if (_inversedX)
                transform.localScale = new Vector2(-1, 1);
            else
                transform.localScale = new Vector2(1, 1);
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
        public void SetPlayerConfig(Config config)
        {
            _config = config;
            _stateMachine.EnterState(_config._state);
        }

        void SetDefault()
        {
            _gravityUi.UnHide();
            transform.position = Vector2.zero;
            SetPlayerConfig("square");
            SetInverseY(false);
            SetInverseX(false);
        }

        void SetCheckPoint()
        {
            PlayerCheckointData data;
            if (_checkPointManager.TryGetLastPlayerCheckpoint(out data))
            {
                transform.position = data.pos;
                SetPlayerConfig(data.config);
                SetInverseX(false);
                SetInverseY(data.inverseY);
            }
            else SetDefault();

        }
        #endregion


        public void OnCheckpointSet(InputAction.CallbackContext ctx)
        {
            if (_isPaused || _isDead) return;

            if (_setCheckPoints && ctx.performed)
            {
                _checkPointManager.SetPlayerCheckpoint(new PlayerCheckointData
                {
                    config = _config,
                    inverseY = _inversed,
                    pos = transform.position,

                });
            }
        }

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
        public void Pause()
        {
            _isPaused = true;
        }

        public void UnPause()
        {
            _isPaused = false;
        }

        public void OnStart(bool practice)
        {
            _setCheckPoints = practice;
            _stateMachine ??= new PlayerStateMachine.PlayerStateMachine(this, _spriteController);


            if (practice)
                SetCheckPoint();
            else
                SetDefault();

            _isDead = false;
            _rb.velocity = Vector2.zero;
            
            _spriteController.gameObject.SetActive(true);
        }

        

        public void OnFail(bool practice)
        {
            _isDead = true;
            _spriteController.gameObject.SetActive(false);
        }

 

        #endregion
    }
}