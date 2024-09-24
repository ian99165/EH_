using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private ControllerMovement3D _controllerMovement;
    private CameraController _cameraController;
    private PlayerControls _controls;

    private void Awake()
    {
        _controls = new PlayerControls();

        _controllerMovement = GetComponent<ControllerMovement3D>();
        if (_controllerMovement == null)
        {
            Debug.LogWarning("ControllerMovement3D is not assigned.");
        }

        _cameraController = FindObjectOfType<CameraController>();
        if (_cameraController == null)
        {
            Debug.LogWarning("CameraController is not assigned.");
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
        _controls.Player.Move.performed += HandleMove;
        _controls.Player.Move.canceled += HandleMove;

        _controls.Player.Look.performed += HandleLook;
        _controls.Player.Look.canceled += HandleLook;
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        if (_controllerMovement != null)
        {
            _controllerMovement.SetMoveInput(context.ReadValue<Vector2>());
        }
    }

    private void HandleLook(InputAction.CallbackContext context)
    {
        if (_cameraController != null)
        {
            _cameraController.HandleLook(context);
        }
        else
        {
            Debug.LogWarning("CameraController is not assigned.");
        }
    }
}
