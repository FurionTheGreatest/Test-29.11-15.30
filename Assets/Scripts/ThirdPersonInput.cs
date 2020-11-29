using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonInput : MonoBehaviour
{
    public FixedJoystick movementInput;
    //public Button fireButton;
    private ThirdPersonUserControl _userControl;

    private void Start()
    {
        _userControl = GetComponent<ThirdPersonUserControl>();
    }

    private void Update()
    {
        _userControl.horizontalInput = movementInput.Horizontal;
        _userControl.verticalInput = movementInput.Vertical;
    }
}
