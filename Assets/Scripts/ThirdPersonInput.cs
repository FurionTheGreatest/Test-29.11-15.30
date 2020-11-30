using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonInput : MonoBehaviour
{
    public FixedJoystick movementInput;
    private ThirdPersonUserControl _userControl;

    private void OnEnable()
    {
        _userControl = GetComponent<ThirdPersonUserControl>();
    }

    private void Update()
    {
        _userControl.horizontalInput = movementInput.Horizontal;
        _userControl.verticalInput = movementInput.Vertical;
    }
}
