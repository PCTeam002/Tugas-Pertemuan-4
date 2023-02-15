using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    private float startDelay = 2;
    private float repeatRate = 2;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(65, 75), 1, Random.Range(-4, 4));
        int index = Random.Range(0, obstaclePrefab.Length);
        Instantiate(obstaclePrefab[index], spawnPos, obstaclePrefab[index].transform.rotation);
    }
}
