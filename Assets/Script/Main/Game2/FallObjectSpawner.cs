using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FallObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fallObjects = new List<GameObject>();
    [SerializeField] private float spawnInterval;
    private const float MAX_SPAWN_INTERVAL = 2.0f;
    [SerializeField] private float range;
    private float time;
    private void Awake()
    {

    }


    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (!IsSpawnTiming()) return;
        spawnInterval = Random.Range(0.5f, MAX_SPAWN_INTERVAL);
        GameObject obj =Instantiate(fallObjects[Random.Range(0, fallObjects.Count)],new Vector3(Random.Range(-(range * 0.5f), range * 0.5f),7.0f,0.0f), Quaternion.identity, transform.parent) ;
        if (obj.CompareTag("Arrow"))
            obj.GetComponent<Arrow>().Init();
        else if (obj.CompareTag("Candy"))
            obj.GetComponent<Candy>().Init();
        time = 0.0f;
        

    }

    private bool IsSpawnTiming()
    {
        return time >= spawnInterval;
    }

}
