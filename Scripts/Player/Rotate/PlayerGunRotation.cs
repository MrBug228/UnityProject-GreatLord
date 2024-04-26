
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerGunRotation : MonoBehaviour
{


    [SerializeField]
    private float turnSpeed = 1000f;

    private GameObject gun;

    // Start is called before the first frame update
    void Awake()
    {        
        this.gun = GameObject.Find("Gun");
    }
    private void Update()
    {
        this.Rotate();
    }
    // Update is called once per frame
    void Rotate()
    {
        // lay vi tri cua chuot
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // lay huong
        Vector3 direction = (mousePosition - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(this.gun.transform.forward, direction);
        Quaternion rotation = Quaternion.RotateTowards(this.gun.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        this.gun.transform.rotation = rotation;

    }
}
