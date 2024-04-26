
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private Rigidbody2D rb;

    private Vector2 movementInput;

  

     void Awake()
    {
        this.rb = this.transform.root.GetComponent<Rigidbody2D>();
    }
   
    void FixedUpdate()
    {
        this.Move();
    }

   
   void Move()
    {
       
        this.rb.AddForce(10f * this.moveSpeed * this.movementInput);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        this.movementInput = context.ReadValue<Vector2>();
    }
    
}
