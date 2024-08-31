using Pryanik.Audio;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Pryanik
{

    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private AudioPool _pool;

        public void SetPool(AudioPool pool) => _pool = pool;
        private async void Start()
        {

            while (_pool == null)
                await Task.Delay(10);

            _slider.value = PlayerPrefsManager.Volume;
        }
        public void OnSliderChanged(Single value)
        {
            PlayerPrefsManager.Volume = value;
            _pool.ChangeVolume(value);
        }
    }
}