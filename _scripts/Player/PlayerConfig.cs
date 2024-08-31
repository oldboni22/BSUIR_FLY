using UnityEngine;


namespace Pryanik.Res
{

    [CreateAssetMenu(menuName = "Player/Config")]
    public class PlayerConfig : Storeable
    {
        [SerializeField] private Config _config;
        public Config Config => _config;
    }
}