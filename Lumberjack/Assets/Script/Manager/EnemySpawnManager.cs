using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    //생성 목록
    public List<GameObject> enemySpawnList = new List<GameObject>();

    //오브젝트 풀 부분
    private List<GameObject> enemyPoolObject = new List<GameObject>();

    //오브젝트 생성 위치 모음
    public List<Transform> spawnLocationList = new List<Transform>();

  
    //웨이브 수
    public int waveCount = 1;

    //오브젝트 풀에서 생성될 제한 수 변수
    public int amountToPool = 23;


    private void Start()
    {
        CreateOriginPool();

        //for(int i =0; i < amountToPool; i++)
        //{
        //    SpawnOriginMode();
        //}
 
    }


    IEnumerator SpawnOrigin()
    {

        for (int i = 0; i < waveCount; i++)
        {
            SpawnOriginMode();

            yield return new WaitForSeconds(0.5f);
        }

    }

    public void CreateOriginPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(enemySpawnList[1]);
            obj.SetActive(false);
            enemyPoolObject.Add(obj);
        }
    }

    public GameObject GetOriginPoolObject()
    {

        for (int i = 0; i < enemyPoolObject.Count; i++)
        {
            if (!enemyPoolObject[i].activeInHierarchy)
            {
                return enemyPoolObject[i];
            }
        }

        return null;

    }


    private void SpawnOriginMode()
    {

        GameObject objects = GetOriginPoolObject();

        int i = Random.Range(0, spawnLocationList.Count);

        if (objects != null)
        {
            objects.transform.position = spawnLocationList[i].position;
            objects.SetActive(true);
        }

    }

}
