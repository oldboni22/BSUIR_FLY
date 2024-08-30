using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelOpenButton : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private string _levelId;

    public void OpenLevel()
    {
        PlayerPrefsManager.LevelId = _levelId;
        SceneManager.LoadScene(_sceneName,LoadSceneMode.Single);
    }
}
