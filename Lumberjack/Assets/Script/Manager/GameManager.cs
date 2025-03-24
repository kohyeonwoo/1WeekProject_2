using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public GameObject pausePanel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void ActivePause()
    {
        pausePanel.SetActive(true);
    }

    public void DeActivePause()
    {
        pausePanel.SetActive(false);
    }
}
