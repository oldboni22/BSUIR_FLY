using UnityEngine;

namespace Pryanik.Layout
{
    internal struct TriggerParams
    {
        internal IPlayer player;
        internal IPlayerCamera camera;
        internal IGamePlaySceneController gamePlaySceneController;
    }
    public abstract class LevelTrigger : MonoBehaviour
    {
        internal abstract void SetParameters(TriggerParams @params);
    }
}