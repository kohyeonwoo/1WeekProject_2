using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public List<GameObject> wallList = new List<GameObject>();

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public int killCount;

    public int rand;
    public int wallRandIndex;

    public List<GameObject> portals = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        rand = Random.Range(0, portals.Count);
        wallRandIndex = Random.Range(0, wallList.Count);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("InGameBGM");

        killCount = 0;

        wallList[wallRandIndex].SetActive(true);
    }

    private void Update()
    {
        if (killCount >= 8)
        {
            portals[rand].SetActive(true);
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

    public void ActiveGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("GameOverSoundEffect");
    }

    public void DeActiveGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("StartScene"); 
    }

    public void IntroLevel()
    {
        SceneManager.LoadScene("IntroScene");
    }

}
