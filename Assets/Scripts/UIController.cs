using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deadScreen;
    bool _isPaused = false;
    GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerDied()
    {
        deadScreen.SetActive(true);
    }

    public void StartOver()
    {
        _gameController.StartOverGame();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            Pause(_isPaused);
            _isPaused = !_isPaused;
        }

    }

    public void Pause(bool isPaused)
    {
        if (isPaused) {
            Time.timeScale = 0;
            pauseScreen.SetActive(isPaused);
        } else {
            Time.timeScale = 1;
            pauseScreen.SetActive(isPaused);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
