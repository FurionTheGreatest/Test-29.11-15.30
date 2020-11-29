using Cinemachine;
using UnityEngine;

public class CinemachineCoreGetInputTouchAxis : MonoBehaviour {
 
    public float touchSensitivityX = 2f;
    public float touchSensitivityY = 2f;

    public FixedJoystick rotationJoystick;

    private void Start () 
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }
     
    private float HandleAxisInputDelegate(string axisName)
    {
        switch(axisName)
        {
            case "Mouse X":
                if (Input.touchCount>0)
                {
                    return rotationJoystick.Horizontal * touchSensitivityX;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }
 
            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    return rotationJoystick.Vertical * touchSensitivityY;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }
 
            default:
                Debug.LogError("Input <"+axisName+"> not recognized.",this);
                break;
        }
 
        return 0f;
    }
}