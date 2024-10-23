using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMovement3D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _deceleration = 5f;
    [SerializeField] private float _turnSpeed = 10f;
    private float _currentSpeed = 0f;
    private float _targetSpeed = 0f;
    private bool _hasMoveInput;
    private Vector3 _moveInput;

    [Header("Jumping")]
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _jumpHeight = 2.5f;
    private Vector3 _velocity;

    private bool _isRunning = false;
    private bool _isStop = false;
    private bool _isCrouch = false;
    private bool _isGrounded;
    public bool isTalk = false;

    private CharacterController _characterController;
    private Animator _anim;

    private PlayerControls _controls;

    private void Awake()
    {
        if (!isTalk)
        {
            _controls = new PlayerControls();

            // 綁定輸入動作到方法
            _controls.Player.Move.performed += ctx => SetMoveInput(ctx.ReadValue<Vector2>());
            _controls.Player.Move.canceled += ctx => SetMoveInput(Vector2.zero);
            _controls.Player.Run.performed += ctx => HandleRun(ctx);
            _controls.Player.Crouch.performed += ctx => HandleCrouch(ctx);
        }
    }


    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _isGrounded = _characterController.isGrounded; // 檢查是否在地面上

        if (!isTalk)
        {
            // 跑步邏輯
            if (_hasMoveInput)
            {
                if (_isRunning)
                {
                    _anim.SetBool("running", true);
                }
                else
                {
                    _anim.SetBool("running", false);
                }
            }
            else if (_isRunning)
            {
                _isRunning = false;
                _anim.SetBool("running", false);
                _isStop = true; // 停止移動邏輯
                StartCoroutine(DelayCheckSpeed()); // 延遲1秒後重啟移動邏輯
            }

            // 蹲下邏輯
            if (_isGrounded)
            {
                if (_isCrouch)
                {
                    _anim.SetBool("crouch", true);
                    _currentSpeed = 0f; // 速度歸零
                }
                else
                {
                    _anim.SetBool("crouch", false);
                }
            }
        }
    }

    private void HandleRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
    }

    private void HandleCrouch(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            _isCrouch = !_isCrouch;
        }
    }

    private IEnumerator DelayCheckSpeed()
    {
        yield return new WaitForSeconds(1f);
        if (_isStop)
        {
            _isStop = false;
            _currentSpeed = 0f;
        }
    }

    public void SetMoveInput(Vector2 input)
    {
        _hasMoveInput = input.magnitude > 0.1f;
        _moveInput = _hasMoveInput ? new Vector3(input.x, 0, input.y) : Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (!isTalk)
        {
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            else
            {
                _velocity.y += _gravity * Time.fixedDeltaTime;
            }

            _characterController.Move(_velocity * Time.fixedDeltaTime);
            HandleMovement();
        }
    }

    private void HandleMovement()
    {
            if (_isCrouch || _isStop)
            {
                if (_isStop)
                {
                    _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, _deceleration * Time.fixedDeltaTime);
                    if (Mathf.Approximately(_currentSpeed, 0f) || _currentSpeed < 0.01f) // 檢查非常小的速度
                    {
                        _isStop = false;
                        _currentSpeed = 0f;
                    }
                }

                return;
            }

            if (_moveInput.magnitude < 0.1f)
            {
                _targetSpeed = 0f;
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, _deceleration * Time.fixedDeltaTime);

                // 如果當前速度接近零，將其設置為零
                if (Mathf.Approximately(_currentSpeed, 0f) || _currentSpeed < 0.01f) // 檢查非常小的速度
                {
                    _currentSpeed = 0f;
                }
            }
            else
            {
                _targetSpeed = _isRunning ? 20f : _moveSpeed;

                // 根據角色的當前朝向調整移動方向
                Vector3 moveDirection = (transform.forward * _moveInput.z) + (transform.right * _moveInput.x);

                // 調整當前速度以匹配目標速度
                if (_currentSpeed < _targetSpeed)
                {
                    _currentSpeed += _acceleration * Time.fixedDeltaTime;
                    if (_currentSpeed > _targetSpeed) _currentSpeed = _targetSpeed;
                }
                else if (_currentSpeed > _targetSpeed)
                {
                    _currentSpeed -= _deceleration * Time.fixedDeltaTime;
                    if (_currentSpeed < _targetSpeed) _currentSpeed = _targetSpeed;
                }

                // 確保 _currentSpeed 不會低於零
                _currentSpeed = Mathf.Max(0, _currentSpeed);

                _characterController.Move(moveDirection.normalized * _currentSpeed * Time.fixedDeltaTime);
        }

        // 更新動畫參數
        _anim.SetFloat("Speed", _currentSpeed);
    }

    public void SetSpeedZreo()
    {
            _currentSpeed = 0f;
            isTalk = true;   
            _anim.SetFloat("Speed", _currentSpeed);
    }

    public void SetMs()
    {
            isTalk = false;
    }
}