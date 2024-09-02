using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pryanik.Layout
{

    public class DisableOnTouchTrigget : LevelTrigger, IStartable
    {
        public void OnFail(bool practice)
        {
            
        }

        public void OnStart(bool practice)
        {
            gameObject.SetActive(true);
        }

        internal override void SetParameters(TriggerParams @params)
        {
            @params.gamePlaySceneController.StartSubscribe(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}