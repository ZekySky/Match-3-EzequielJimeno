using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;
    public GameObject TextoMenu;
    public GameObject MenuOver;
    public GameObject Board;
    public GameObject End;
    void Start()
    {
        End.SetActive(false);
        MenuOver.SetActive(false);
        TextoMenu.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 0);
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int Points = 0;
    public UnityEvent OnPointsUpdate;
    public void AddPoints(int newPoints)
    {
        Points += newPoints;
        OnPointsUpdate?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        if (Points >= 80)
        {
            End.SetActive(true);
            Board.SetActive(false);
            MenuOver.SetActive(true);
            TextoMenu.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
        }
    }
}
