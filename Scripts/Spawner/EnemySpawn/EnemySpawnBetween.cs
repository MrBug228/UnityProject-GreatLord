using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBetween : MonoBehaviour
{
    
    private ObjectPool objectPool;

    [SerializeField]
    private float timeUntilSpawn = 2f;

    private float timer  = 2f;

    private GameObject enemy;

    private HealthController healthController;


    private void Awake()
    {
        this.objectPool = transform.root.GetComponentInChildren<ObjectPool>();

    }

    private void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >=  this.timeUntilSpawn)
        {
            this.enemy = objectPool.GetObjectFromPool();
            this.healthController = this.enemy.GetComponentInChildren<HealthController>();
            this.healthController.ReBorn();
            this.enemy.transform.position = this.transform.position;
            this.enemy.SetActive(true);
           

            this.timer -= this.timeUntilSpawn;

        }
    }
}
