using UnityEngine;



namespace Pryanik.Res
{


    [CreateAssetMenu(menuName = "Audio")]
    public class Audio : Storeable
    {
        [SerializeField] private AudioClip _clip;

        public AudioClip Clip { get => _clip; }
    }
}