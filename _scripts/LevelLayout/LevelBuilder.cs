using Pryanik.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Pryanik.Layout
{

    public class LevelBuilder : MonoBehaviour
    {
        [Inject] private IPlayer _player;
        [Inject] private IPlayerCamera _cam;
        [Inject] private LayoutStorage _storage;

        private void Awake()
        {
            var @params = _storage.GetById(PlayerPrefsManager.LevelId);
            ReadLayout(@params);
            
        }
        void ReadLayout(LevelLayout layout)
        {

            var tParams = new TriggerParams
            {
                player = _player,
                camera = _cam,
            };

            
            GameObject.Instantiate(layout._obstacles);
            GameObject.Instantiate(layout._trigger).SetParams( tParams );
        }
    }
}