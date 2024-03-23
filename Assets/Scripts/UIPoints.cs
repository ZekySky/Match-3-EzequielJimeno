using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPoints : MonoBehaviour
{
    int displayedPoints;
    public TextMeshProUGUI pointsLabel;
    public TextMeshProUGUI pointsGameOver;
    void Start()
    {
        GameManager.Instance.OnPointsUpdate.AddListener(UpdatedPoints);
    }
    
    private void UpdatedPoints()
    {
        StartCoroutine(UpdatedPointsCoroutine());
    }
    IEnumerator UpdatedPointsCoroutine()
    {
        while (displayedPoints < GameManager.Instance.Points)
        {
            displayedPoints++;
            pointsLabel.text = displayedPoints.ToString();
            pointsGameOver.text = displayedPoints.ToString() +" Points";
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
