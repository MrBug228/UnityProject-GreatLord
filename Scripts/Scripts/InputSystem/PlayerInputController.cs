using UnityEngine;
using UnityEngine.InputSystem;
namespace MrBug228.GameRPG2D
{
    public class PlayerInputController : MonoBehaviour
    {

        private PlayerInput _playerInput;

        private InputAction _moveAction;
        private InputAction _jumpAction;



        public static Vector2 movement { get; set; }

        public static bool jumpStared { get; set; }

        public static bool jumpCanceled { get; set; }


        public bool playerControllerInputBlocked = false;   // Biến kiểm tra có khóa input không
        public bool externalInputBlocked = false;         // Biến kiểm tra có khóa input ngoại vi không

        private void Awake()
        {
            // Lấy PlayerInput từ GameObject
            _playerInput = GetComponent<PlayerInput>();

            // Lấy các hành động từ PlayerInput
            _moveAction = _playerInput.actions["Move"];
            _jumpAction = _playerInput.actions["Jump"];

        }

        private void OnEnable()
        {
            // Đăng ký các lệnh gọi lại cho từng hành động
            _moveAction.performed += OnMove;
            _moveAction.canceled += OnMove;

            _jumpAction.started += OnJump;
            _jumpAction.canceled += OnJump;

        }

        private void OnDisable()
        {
            // Hủy đăng ký lệnh gọi lại khi đối tượng bị vô hiệu hóa
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;



            _jumpAction.started -= OnJump;
            _jumpAction.canceled -= OnJump;
        }

        // Callback cho hành động di chuyển
        private void OnMove(InputAction.CallbackContext context)
        {
            if (playerControllerInputBlocked || externalInputBlocked) return;
            movement = context.ReadValue<Vector2>();
        }


        // Callback cho hành động nhảy
        private void OnJump(InputAction.CallbackContext context)
        {
            if (playerControllerInputBlocked || externalInputBlocked) return;
            jumpStared = context.started;
            jumpCanceled = context.canceled;
        }
    }

}