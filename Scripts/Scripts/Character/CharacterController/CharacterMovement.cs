using UnityEngine;


namespace MrBug228.GameRPG2D
{
    public class CharacterMovement : MonoBehaviour
    {
        protected CharacterBody _characterBody;
        [SerializeField] protected Vector2 _moveInput;
        [Min(0)]
        [SerializeField] protected float _speed = 5;

        protected Vector3 _originalScale;
        protected float _movement;

        void Setup()
        {
            _characterBody = GetComponent<CharacterBody>();

            _originalScale = transform.localScale;

        }
        private void Awake()
        {
            Setup();
        }

        private void Update()
        {
            HandleInput();
            OnMove();
        }

        private void HandleInput()
        {
            _moveInput = PlayerInputController.movement;
        }
        private void OnMove()
        {
            LockingDirectionHandle();
            _movement = _moveInput.x * _speed;
            Move(_movement);
        }
        private void Move(float movement)
        {
            Vector2 moveVelocity = new Vector2(movement, _characterBody.bodyVelocity.y);
            _characterBody.UpdateBodyVelocity(moveVelocity);
        }
        private void LockingDirectionHandle()
        {
            if (_moveInput.x != 0)
            {
                var scale = _originalScale;
                scale.x = _moveInput.x > 0 ? _originalScale.x : -_originalScale.x;
                transform.localScale = scale;
            }
        }
    }
}
