using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public GameObject MenuStart;
    public GameObject Board;
    public AudioSource StartGame;
    // Start is called before the first frame update
    private void Start()
    {
        Board.SetActive(false);
        MenuStart.SetActive(true);
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("Match-3Ezequiel");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Empezar()
    {
        StartGame.Play();
        Board.SetActive(true);
        MenuStart.SetActive(false);
    }
}
