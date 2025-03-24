using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
    public void GoInGameScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
