using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    UIController _uiController;
    // Start is called before the first frame update
    void Start()
    {
        _uiController = FindObjectOfType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) 
        {
            _uiController.PlayerWon();
        }
    }
}
