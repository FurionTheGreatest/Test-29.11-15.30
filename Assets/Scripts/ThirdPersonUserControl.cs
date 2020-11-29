using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonUserControl : MonoBehaviour
{
    public Transform sceneCamera;

    public float movementSpeed = 6f;
    public float horizontalInput;
    public float verticalInput;
    
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;    

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
#endif
        Move();
    }

    private void Move()
    {
        var direction = new Vector3(horizontalInput,0f,verticalInput).normalized;

        if (!(direction.magnitude >= 0.1f)) return;
        //rotate
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + sceneCamera.eulerAngles.y;
        var smoothedAngle =
            Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f,smoothedAngle,0f);
        //move
        _characterController.Move(MoveWithCamera(smoothedAngle).normalized * (movementSpeed * Time.deltaTime));
    }

    private Vector3 MoveWithCamera(float angle)
    {
        return Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
    }
}
