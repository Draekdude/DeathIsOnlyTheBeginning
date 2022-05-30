using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public string GetTime()
    {
        return timer.ToString("00.00");
    }

    public void StartOverGame()
    {
        SceneManager.LoadScene(0);
    }
}
