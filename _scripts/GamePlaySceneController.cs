using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Pryanik
{
    public interface IGamePlaySceneController
    {
        event Action OnStart;
        event Action OnFail;

        void Fail();
    }

    public class GamePlaySceneController : MonoBehaviour, IGamePlaySceneController
    {

        [SerializeField] private float _timeToRestart;

        public event Action OnStart;
        public event Action OnFail;

        public void Fail()
        {
            StartCoroutine(Restart());
        }
        IEnumerator Restart()
        {
            OnFail();
            yield return new WaitForSeconds(_timeToRestart);
            OnStart();
        }


        void Start()
        {
            OnStart();
        }
        
    }
}