using Pryanik.Checkpoint;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Pryanik
{
    public interface IPlayerCamera
    {
        public void SetPosY(float val);
        public void SetSpeedY(float val);
        public void SetOffsetTarget(float val);
        public void SetSpeedX(float val);
    }
    [RequireComponent(typeof(Camera))]
    public class PlayerCamera : MonoBehaviour, IPlayerCamera, IStartable
    {
        [SerializeField] private Transform _followTarget;

        private bool _setCheckpoints;

        private float _speedX;
        private float _speedY;

        private float _targetOffsetX;
        private float _offsetX;

        private float _curY;
        private float _targetY;

        [Inject] private ICheckPointManager _checkPointManager;

        [Inject]
        public void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.StartSubscribe(this);
        }

        private void Update()
        {
            var newPos = new Vector3(_followTarget.position.x + _offsetX, _curY, -10);
            transform.position = newPos;
        }

        private void FixedUpdate()
        {
            if (_offsetX != _targetOffsetX)
            {
                _offsetX = Mathf.Lerp(_offsetX, _targetOffsetX, _speedX * Time.fixedDeltaTime);
            }
            if (_curY != _targetY)
            {
                _curY = Mathf.Lerp(_curY, _targetY, _speedY * Time.fixedDeltaTime);
            }
        }
        public void SetOffsetTarget(float val)
        {
            _targetOffsetX = val;
        }

        public void SetSpeedX(float val)
        {
            _speedX = val;
        }

        public void SetPosY(float val)
        {
            _targetY = val;
        }

        public void SetSpeedY(float val)
        {
            _speedY = val;
        }

        public void OnStart(bool practice)
        {
            _setCheckpoints = practice;
            CameraCheckointData data;
            if (practice && _checkPointManager.TryGetLastCameraCheckpoint(out data))
            {
                _speedX = data.speedX;
                _speedY = data.speedY;

                _targetOffsetX = data.targetOffsetX;
                _offsetX = data.targetOffsetX;

                _targetY = data.targetY;
                _curY = data.targetY;
            }
            else SetDefault();
        }

        void SetDefault()
        {
            _speedX = 0;
            _speedY = 0;

            _targetOffsetX = 0f;
            _offsetX = 0f;

            _targetY = 0f;
            _curY = 0f;
        }

        public void OnCheckpointSet(InputAction.CallbackContext ctx)
        {
  
            if (_setCheckpoints && ctx.performed)
            {
                _checkPointManager.SetCameraCheckpoint(new CameraCheckointData
                {
                    speedX = _speedX,
                    speedY = _speedY,
                    targetOffsetX = _targetOffsetX,
                    targetY = _targetY
                });
            }
        }

        public void OnFail(bool practice)
        {
            _setCheckpoints = false;
        }
    }
}