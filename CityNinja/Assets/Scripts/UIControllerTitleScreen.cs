using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIControllerTitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        // Brings player to the controls screen before starting the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("ControlScreen");
    }

    public void controls()
    {
        // Brings player to the controls screen if they wish to read the controls first
        UnityEngine.SceneManagement.SceneManager.LoadScene("ControlScreen");
    }
}
