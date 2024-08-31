using UnityEngine;
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
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        [SerializeField] private Transform _followTarget;

        private float _speedX;
        private float _speedY;

        private float _targetOffsetX;
        private float _offsetX;

        private float _curY;
        private float _targetY;

        [Inject]
        public void Inject(IGamePlaySceneController gamePlaySceneController)
        {
            gamePlaySceneController.OnStart += () =>
            {
                _targetOffsetX = 0f;
                _offsetX = 0f;

                _targetY = 0f;
                _curY = 0f;
            };
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
    }
}