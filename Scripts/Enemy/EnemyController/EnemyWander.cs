using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWander : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed; // Tốc độ di chuyển của kẻ thù

    [SerializeField]
    private float rotationSpeed; // Tốc độ quay của kẻ thù

    private Rigidbody2D rb; // Rigidbody để điều khiển vận tốc của kẻ thù

    private Vector2 targetDirection; // Hướng mục tiêu của kẻ thù

    private float changeDirectionCooldown; // Thời gian cooldown trước khi thay đổi hướng (đổi góc quay)

    private PlayerDetection playerDetection;

   

    // Awake được gọi khi đối tượng được khởi tạo
    private void Awake()
    {
        this.rb = this.transform.parent.parent.GetComponent<Rigidbody2D>(); // Lấy tham chiếu đến Rigidbody2D của đối tượng
        this.targetDirection = transform.up; // Khởi tạo hướng mục tiêu ban đầu là hướng lên
        this.playerDetection = this.transform.parent.GetComponentInChildren<PlayerDetection>();
    }

    // FixedUpdate được gọi với tần suất cố định, thường được sử dụng cho các thao tác vật lý
    private void FixedUpdate()
    {
        UpdateTargetDirection(); // Cập nhật hướng mục tiêu
        Rotate(); // Xoay kẻ thù về hướng mục tiêu
        Move(); // Thiết lập vận tốc dic chuyen
       
    }

    // Cập nhật hướng mục tiêu của kẻ thù
    private void UpdateTargetDirection()
    {


        if (this.playerDetection.playerDitected)
        {
            this.PlayerTargerting();
            return;

        } else

        {
            this.RandomDirectionChange(); // Xử lý thay đổi hướng ngẫu nhiên
        }
    }

    // Xử lý thay đổi hướng ngẫu nhiên
    private void RandomDirectionChange()
    {
        changeDirectionCooldown -= Time.fixedDeltaTime;
        if (changeDirectionCooldown <= 0)
        {
            DirectionChange();
            changeDirectionCooldown = Random.Range(3f, 9f); // Cập nhật lại thời gian cooldown trước khi thay đổi hướng
        }
    }

    // Xử lý thay đổi hướng
    private void DirectionChange()
    {
        float angleChange = Random.Range(-90f, 90f); // Tạo góc quay ngẫu nhiên
        Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward); // Lưu trữ góc đã tạo
        this.targetDirection = rotation * this.targetDirection; // Cập nhật hướng quay
    }

    // Xử lý nhắm mục tiêu người chơi
    private void PlayerTargerting()
    {
      
            this.targetDirection = this.playerDetection.directionToPlayer;
        
    }

    // Xoay kẻ thù về hướng mục tiêu
    private void Rotate()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, this.targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        this.rb.MoveRotation(rotation);
    }

    // Thiết lập vận tốc của kẻ thù
    private void Move()
    {
        this.rb.AddForce(this.targetDirection * moveSpeed);
    }

}
