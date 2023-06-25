using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FallObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fallObjects = new List<GameObject>();
    [SerializeField] private float spawnInterval;
    private float time;
    private void Awake()
    {

    }


    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (!IsSpawnTiming()) return;
        Instantiate(fallObjects[Random.Range(0, fallObjects.Count)],new Vector3(Random.Range(-8.0f,8.0f),7.0f,0.0f), Quaternion.identity, transform.parent) ;
        time = 0.0f;
        

    }

    private bool IsSpawnTiming()
    {
        return time >= spawnInterval;
    }

}
