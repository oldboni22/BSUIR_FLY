using UnityEngine;


namespace Pryanik.Res
{


    [CreateAssetMenu(menuName = "Player/Skin")]
    public class PlayerSkin : Storeable
    {
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite { get => _sprite; }
    }
}