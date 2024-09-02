using Pryanik.Layout;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pryanik.Progress
{

    public class LevelProgressManager : MonoBehaviour, IStartable, IWriter
    {
        [SerializeField] private float _speed;
        [SerializeField] private Slider _slider;
        [SerializeField] private Transform _target;
        private IProgressSaver _progressSaver;

        private string _curLvlId;

        private int _normalTries;
        private int _practiceTries;
        private float _maxProgress;

        private float _curProgress;

        private float _endX;
        private bool _practice;

        [Inject]
        public void Inject(IProgressSaver progressSaver,LayoutStorage storage, IGamePlaySceneController gamePlaySceneController)
        {
            _curLvlId = PlayerPrefsManager.LevelId;
            _endX = storage.GetById(_curLvlId).EndX;

            _progressSaver = progressSaver;
            gamePlaySceneController.StartSubscribe(this);
        }


        private void Update()
        {
            _curProgress = (float)Math.Round(_target.position.x / _endX,2);
            _slider.value = Mathf.Lerp(_slider.value,_curProgress,Time.deltaTime * _speed);
        }

        private void Start()
        {
            _progressSaver.WriterSubscribe(this);
            var progress = _progressSaver.GetProgress(_curLvlId);

            _normalTries = progress.normalTriesCount;
            _practiceTries = progress.practiceTriesCount;
            _maxProgress = progress.progress;
        }

        private void OnDisable()
        {
            _progressSaver.WriterUnSubscribe(this);
            Write();
        }

        public void OnFail(bool practice)
        {
            if (practice)
                _practiceTries++;
            else
            {
                _normalTries++;
                if (_curProgress > _maxProgress)
                    _maxProgress = _curProgress;
            }
        }

        public void OnStart(bool practice)
        {
            _practice = practice;
        }

        public void Write()
        {
            if ( _practice is false && _curProgress > _maxProgress )
                _maxProgress = _curProgress;
            _progressSaver.SetProgress(_curLvlId, new LevelProgress
            {
                normalTriesCount = _normalTries,
                practiceTriesCount = _practiceTries,
                progress = _maxProgress,

            });
        }
    }
}