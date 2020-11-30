using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GamePoints : MonoBehaviour
{
    public TMP_Text points;
    private int pointCount = 0;

    public void AddPoints()
    {
        pointCount += 100;
        points.text = pointCount.ToString(CultureInfo.CurrentCulture);
    }
    private void OnEnable()
    {
        EnemyHealth.OnEnemyDie += AddPoints;
    }
    
    private void OnDisable()
    {
        EnemyHealth.OnEnemyDie -= AddPoints;
    }
}
