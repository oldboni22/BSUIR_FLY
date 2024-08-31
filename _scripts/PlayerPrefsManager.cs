using UnityEngine;

namespace Pryanik
{
    public static class PlayerPrefsManager
    {

        static readonly string LevelKey = "Level";
        static readonly string SkinKey = "Skin";

        static readonly string VolumeKey = "Volume";
        public static string LevelId
        {
            get => PlayerPrefs.GetString(LevelKey);
            set
            {
                PlayerPrefs.SetString(LevelKey, value);
                PlayerPrefs.Save();
            }
        }
        public static string SkinId
        {
            get => PlayerPrefs.GetString(SkinKey);
            set
            {
                PlayerPrefs.SetString(SkinKey, value);
                PlayerPrefs.Save();
            }
        }
        public static float Volume
        {
            get => PlayerPrefs.GetFloat(VolumeKey);
            set
            {
                PlayerPrefs.SetFloat(VolumeKey, value);
                PlayerPrefs.Save();
            }
        }
    }
}