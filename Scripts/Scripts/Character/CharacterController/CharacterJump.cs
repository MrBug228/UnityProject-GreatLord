using Unity.Collections;
using UnityEngine;

namespace MrBug228.GameRPG2D
{
    public class CharacterJump : MonoBehaviour
    {
        protected CharacterBody _characterBody;
        protected bool _jumpInputStarted;
        protected bool _jumpInputCanceled;

        [Min(0)]
        [SerializeField] protected float _jumpHeight = 5.5f;
        [SerializeField] protected bool _isJumping;
        [Min(1)]
        [SerializeField] protected float _stopJumpFactor = 1.5f;
        [Min(0)]
        [SerializeField] protected float _jumpActionTime = 0.1f;
        [Min(0)]
        [SerializeField] protected float _rememberGroundTime = 0.1f;
        protected float _jumpActionEndTime;
        protected float _lostGroundTime;
        protected float _jumpSpeed;

        void Setup()
        {
            _characterBody = GetComponent<CharacterBody>();

            _jumpSpeed = Mathf.Sqrt(2 * Physics2D.gravity.magnitude *
                _characterBody.gravityScale * _jumpHeight);
        }
        private void Awake()
        {
            Setup();
        }
        private void Update()
        {
            HandleInput();
            OnJump();
        }
        private void HandleInput()
        {
            _jumpInputStarted = PlayerInputController.jumpStared;
            _jumpInputCanceled = PlayerInputController.jumpCanceled;
        }

        private void OnGrounded()
        {
            _isJumping = false;
            if (_jumpActionEndTime > Time.unscaledTime)
            {
                _jumpActionEndTime = 0;
                Jump();
            }
        }

        private void OnJump()
        {
            if (_jumpInputStarted)
            {
                if (_characterBody.state == CharacterBody.BodyState.Grounded
                || !_isJumping && _lostGroundTime > Time.unscaledDeltaTime)
                {

                    Jump();
                }
                else
                {
                    _jumpActionEndTime = Time.unscaledTime + _jumpActionTime;
                }
            }
            else if (_jumpInputCanceled)
            {

                StopJumping();
            }
        }
        void Jump()
        {
            Vector2 jumpVelocity = new Vector2(_characterBody.bodyVelocity.x, _jumpSpeed);
            _characterBody.UpdateBodyVelocity(jumpVelocity);
            _isJumping = true;
        }
        void StopJumping()
        {
            _jumpActionEndTime = 0;
            var velocity = _characterBody.bodyVelocity;

            if (_isJumping && velocity.y > 0)
            {
                _characterBody.UpdateBodyVelocity(
                new Vector2(velocity.x, velocity.y / _stopJumpFactor)
                );

            }
        }

        private void OnEnable()
        {
            _characterBody.stateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _characterBody.stateChanged -= OnStateChanged;
        }

        private void OnStateChanged(CharacterBody.BodyState previousState, CharacterBody.BodyState state)
        {
            if (state == CharacterBody.BodyState.Grounded)
            {
                OnGrounded();
            }
            else if (previousState == CharacterBody.BodyState.Grounded)
            {
                _lostGroundTime = Time.unscaledTime + _rememberGroundTime;
            }
        }
    }
}
