using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Pryanik.Checkpoint
{
    public struct CameraCheckointData
    {
        public float speedX;
        public float speedY;

        public float targetOffsetX;

        public float targetY;
    }
    public interface IToggleSubscriber
    {
        void OnChechpointToggle(bool val);
    }
    public interface ICheckPointManager
    {
        void SetPlayerCheckpoint(PlayerCheckointData data);
        void SetCameraCheckpoint(CameraCheckointData data);
        void CheckpointToggleSuscribe(IToggleSubscriber subscriber);
        void CheckpointToggleUnSuscribe(IToggleSubscriber subscriber);
        void ToggleCheckpointMode(bool val);
        bool TryGetLastPlayerCheckpoint(out PlayerCheckointData data);
        bool TryGetLastCameraCheckpoint(out CameraCheckointData data);
    }
    public class CheckPointManager : MonoBehaviour, ICheckPointManager
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private GameObject _mark;
        private event Action<bool> _onToggled;
        private bool _isOn = false;

        private readonly Stack<PlayerCheckointData> _playerCheckpoints = new Stack<PlayerCheckointData>();
        private readonly Stack<CameraCheckointData> _cameraCheckpoints = new Stack<CameraCheckointData>();
        private readonly Stack<GameObject> _marks = new Stack<GameObject>();
        private LocalGameobjectPool _marksPool;

        [Inject]
        public void Inject(ISettingsController settings)
        {
            settings.SubscribeCheckPoint(this);
            _marksPool = new LocalGameobjectPool(_mark);
        }
        public void RemoveCheckpoint(InputAction.CallbackContext ctx)
        {
            if (_isOn is false)
                return;

            if (ctx.performed && _playerCheckpoints.Count > 0)
            {
                _marks.Pop().SetActive(false);
                _playerCheckpoints.Pop();
                _cameraCheckpoints.Pop();
            }
        }

        public void SetPlayerCheckpoint(PlayerCheckointData data)
        {
            _marks.Push(_marksPool.Spawn(data.pos));
            _playerCheckpoints.Push(data);
        }

        public void ToggleCheckpointMode(bool val)
        {
            _isOn = val;
            if (val is false)
            {
                RemoveAllCheckPoints();
            }

            _onToggled?.Invoke(val);
            _ui?.SetActive(val);
        }

        void RemoveAllCheckPoints()
        {
            _marksPool.DispawnAll();
            _marks.Clear();
            _playerCheckpoints.Clear();
            _cameraCheckpoints.Clear();
        }


        public void CheckpointToggleSuscribe(IToggleSubscriber subscriber)
        {
            _onToggled += subscriber.OnChechpointToggle;
        }

        public bool TryGetLastPlayerCheckpoint(out PlayerCheckointData data)
        {
            data = new PlayerCheckointData();
            if (_playerCheckpoints.Count != 0)
            {
                data = _playerCheckpoints.Peek();
                return true;
            }

            return false;
        }

        public void SetCameraCheckpoint(CameraCheckointData data)
        {
            _cameraCheckpoints.Push(data);
        }

        public bool TryGetLastCameraCheckpoint(out CameraCheckointData data)
        {
            data = new CameraCheckointData();
            if (_playerCheckpoints.Count != 0)
            {
                data = _cameraCheckpoints.Peek();
                return true;
            }

            return false;
        }

        private void OnDisable()
        {
            
        }
        public void CheckpointToggleUnSuscribe(IToggleSubscriber subscriber)
        {
            _onToggled -= subscriber.OnChechpointToggle;
        }
    }
}
