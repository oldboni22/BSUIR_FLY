using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pryanik.Res
{

    [CreateAssetMenu(menuName = "Player/Config")]
    public class PlayerConfig : Storeable
    {
        [SerializeField] private Player.Config _config;
        public Player.Config Config => _config;
    }
}