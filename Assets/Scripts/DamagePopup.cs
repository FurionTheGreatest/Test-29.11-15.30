using System.Globalization;
using Cinemachine;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMesh;
    private Color _textColor;
    private Transform _cameraTransform;
    
    private float _disappearTime = .5f;
    private float _fadeOutSpeed = 4f;
    private float _moveYSpeed = 3f;
    
    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void SetupPopupText(float amount)
    {
        _cameraTransform = FindObjectOfType<CinemachineFreeLook>().transform;

        _textColor = _textMesh.color;
        _textMesh.text = amount.ToString(CultureInfo.CurrentCulture);
    }

    private void LateUpdate()
    {
        transform.LookAt(2 * transform.position - _cameraTransform.position);
        transform.position += new Vector3(0f, _moveYSpeed * Time.deltaTime,0f);

        _disappearTime -= Time.deltaTime;
        if (_disappearTime < 0)
        {
            _textColor.a -= _fadeOutSpeed * Time.deltaTime;
            _textMesh.color = _textColor;
            if (_textColor.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
