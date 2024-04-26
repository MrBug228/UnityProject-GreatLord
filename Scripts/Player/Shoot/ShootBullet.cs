using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShootBullet : MonoBehaviour
{
   
    private ObjectPool objectPool;
    public static Transform spawnPointTransform { get; set; }

    [SerializeField]
    private float timeBetweenShoots = 0.2f;

    private float lastFireTime;
    private bool canFire;
  

    private void Awake()
    {
        this.LoadSpawnPoint();
        this.objectPool = transform.root.GetComponentInChildren<ObjectPool>();
    }
    void Update()
    {
        this.Shoot();
    }

    private void Shoot()
    {
        if (this.canFire )
        {
            float timeSinceLasetFire = Time.time - this.lastFireTime;
            if (timeSinceLasetFire >= timeBetweenShoots)
            {
                this.SpawnBullet();
                this.lastFireTime = Time.time;
               
            }
        }
    }

    private void LoadSpawnPoint()
    {
        spawnPointTransform = GameObject.Find("Fire_Bullet_Point").transform;
    }
    private void SpawnBullet()
    {
        GameObject bullet = this.objectPool.GetObjectFromPool();
        bullet.transform.position = spawnPointTransform.position;
        bullet.SetActive(true);
    }

    public void OnFirePerformed(InputAction.CallbackContext context)
    {
        this.canFire = context.performed;
       
        
    }
    public void OnFireCanceled(InputAction.CallbackContext context)
    {
        this.canFire = context.canceled;
        // Vô hiệu hóa bắn liên tục khi chuột trái được nhả ra
     
    }



}
