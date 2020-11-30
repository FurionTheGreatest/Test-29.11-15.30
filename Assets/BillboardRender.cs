using UnityEngine;

public class BillboardRender : MonoBehaviour
{
    public Transform sceneCamera;

    private void OnEnable()
    {
        if (sceneCamera == null)
            sceneCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(sceneCamera.transform);
    }
}
