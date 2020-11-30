using UnityEngine;

public class PopupSpawner : MonoBehaviour
{
    public static PopupSpawner instance;
    
    public GameObject damagePopupPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void DisplayDamagePopup(float amount, Transform parent)
    {
        var popupInstance = Instantiate(damagePopupPrefab, parent.position, Quaternion.identity,parent.transform);
        popupInstance.GetComponent<DamagePopup>().SetupPopupText(amount);
    }
}
