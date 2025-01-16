using UnityEngine;
using System;
namespace MrBug228.GameRPG2D
{
    public class CharacterBody : MonoBehaviour
    {
        public enum BodyState
        {
            Grounded,
            Airborne,
            Climbing
        }
        [SerializeField] protected Rigidbody2D.SlideMovement _slideMovement;


        protected Rigidbody2D _rigidbody;

        [Min(0)]
        [field: SerializeField] public float gravityScale { get; set; } = 1f;

        [SerializeField] protected LayerMask _solidLayers;

        [Min(0)]
        [SerializeField] protected float _maxSpeed = 30f;

        [Min(0)]
        [SerializeField] protected float _surfaceAnchor = 0.01f;

        //Góc nghiêng tối đa mà một nhân vật có thể đứng vững trong khi vẫn duy trì trạng thái tiếp đất
        [Range(0, 90)]
        [SerializeField] protected float _maxStandableAngle = 50f;


        [Range(0, 90)]
        [SerializeField] protected float _slopeLimit = 45f; // goc toi da co the leo len 

        [SerializeField] protected Vector2 _velocity;

        [Range(0, 1)]
        [SerializeField] float _ariSpeedScale = 0.7f;

        protected float _sprMaxSpeed;

        // Setter and getter Movement Speed Limitation
        public Vector2 bodyVelocity
        {
            get { return _velocity; }
            private set
            {
                _velocity =
                    value.sqrMagnitude > _sprMaxSpeed
                    ? value.normalized * _maxSpeed
                    : value;
            }
        }
        [field: SerializeField] protected BodyState _state;
        public BodyState state
        {
            get => _state;

            private set
            {
                if (_state != value)
                {
                    var previousState = _state;
                    _state = value;
                    stateChanged?.Invoke(previousState, value);
                }
            }
        }

        public event Action<BodyState, BodyState> stateChanged;

        protected float _minGroundVertical;


        private static Vector2 ClipVector(Vector2 vector, Vector2 hitNormal)
        {
            return vector - Vector2.Dot(vector, hitNormal) * hitNormal;
        }

        private void SetUp()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _minGroundVertical = Mathf.Cos(_maxStandableAngle * Mathf.PI / 180f);
            _sprMaxSpeed = _maxSpeed * _maxSpeed;
            _slideMovement = CreateSlideMovement();

        }
        private void Awake()
        {
            SetUp();

        }
        private void FixedUpdate()
        {
            AddGravity();
            Move();

        }
        private void AddGravity()
        {
            if (!(state == BodyState.Airborne)) return;
            bodyVelocity += gravityScale * Time.fixedDeltaTime * Physics2D.gravity;
        }
        public void UpdateBodyVelocity(Vector2 velocity)
        {
            if (state == BodyState.Airborne)
            {
                velocity.x = velocity.x * _ariSpeedScale;
            }
            bodyVelocity = velocity;
        }

        private void Move()
        {
            var slideResults = _rigidbody.Slide(
                _velocity,
                 Time.fixedDeltaTime,
                  _slideMovement);

            var slideHit = slideResults.slideHit;
            var surfaceHit = slideResults.surfaceHit;

            if (slideHit)
            {
                bodyVelocity = ClipVector(_velocity, slideHit.normal);
            }


            if (surfaceHit)
            {

                bodyVelocity = ClipVector(_velocity, surfaceHit.normal);

                if (surfaceHit.normal.y >= _minGroundVertical)
                {
                    state = BodyState.Grounded;
                    return;
                }
                else
                {
                    state = BodyState.Climbing;
                    float angle = Vector2.Angle(surfaceHit.normal, Vector2.up);
                    float reductionFactor = Mathf.Clamp01(1 - angle / _slopeLimit);
                    bodyVelocity = new Vector2(_velocity.x, _velocity.y * reductionFactor);
                    return;
                }
            }

            state = BodyState.Airborne;


        }


        private Rigidbody2D.SlideMovement CreateSlideMovement()
        {
            return new Rigidbody2D.SlideMovement
            {
                maxIterations = 3,
                surfaceSlideAngle = _slopeLimit,
                gravitySlipAngle = 90,
                surfaceUp = Vector2.up,
                surfaceAnchor = Vector2.down * _surfaceAnchor,
                gravity = gravityScale * Physics2D.gravity,
                useSimulationMove = true,
                layerMask = _solidLayers,
                useLayerMask = true,

                //     maxIterations = 3; // Số lần tối đa để thực hiện một hành động trượt
                //     surfaceSlideAngle = 90f; // Góc tối đa cho phép trượt trên bề mặt
                //     gravitySlipAngle = 90f; // Góc tối đa cho phép trượt do trọng lực
                //     surfaceUp = Vector2.up; // Hướng lên của bề mặt, thường là hướng dương của trục Y
                //     surfaceAnchor = Vector2.down; // Điểm neo của bề mặt, thường là hướng âm của trục Y
                //     gravity = new Vector2(0f, -9.81f); // Lực trọng lực, với giá trị âm cho thấy hướng xuống
                //     startPosition = Vector2.zero; // Vị trí bắt đầu, được khởi tạo tại gốc tọa độ (0, 0)
                //     selectedCollider = null; // Collider được chọn, khởi tạo là null (không có collider nào được chọn)
                //     useStartPosition = false; // Biến xác định có sử dụng vị trí bắt đầu hay không
                //     useNoMove = false; // Biến xác định có cho phép không di chuyển hay không
                //     useSimulationMove = false; // Biến xác định có sử dụng di chuyển mô phỏng hay không
                //     useAttachedTriggers = false; // Biến xác định có sử dụng các trigger gắn liền hay không
                //     useLayerMask = false; // Biến xác định có sử dụng mặt nạ lớp hay không
                //     layerMask = -1; // Mặt nạ lớp, khởi tạo là -1 (có thể có nghĩa là không có lớp nào được áp dụng)

            };
        }
    }
}
