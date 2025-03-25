using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public GameObject pausePanel;

    public int killCount;

    public int rand;

    public List<GameObject> portals = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        killCount = 0;

        int rand = Random.Range(0, portals.Count);
    }

    private void Update()
    {
        if(killCount >= 8)
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
}
