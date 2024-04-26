using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 10f;

    private Rigidbody2D rb;

    private Transform spawnPoint;
    private Transform bulletObjectTransform;
    private void Awake()
    {
        this.bulletObjectTransform = this.transform.parent.parent;
        this.spawnPoint = ShootBullet.spawnPointTransform;
        rb = this.bulletObjectTransform.GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        this.BulletSetup();
    }

    void OnEnable()
    {
        this.BulletSetup();
    }

    private void BulletSetup()
    {
        this.rb.velocity = this.spawnPoint.up * this.bulletSpeed;
        // Xác định hướng di chuyển của viên đạn
        Vector2 moveDirection = this.rb.velocity.normalized;

        // Tính toán góc quay dựa trên hướng di chuyển
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        // Quay viên đạn để nó hướng theo hướng di chuyển
        this.bulletObjectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


}
