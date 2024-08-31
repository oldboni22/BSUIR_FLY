using UnityEngine;


namespace Pryanik.Layout
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class PlayerTrigger : LevelTrigger
    {
        [SerializeField] protected IPlayer _player;
        internal override void SetParameters(TriggerParams @params)
        {
            _player = @params.player;   
        }
    }
}