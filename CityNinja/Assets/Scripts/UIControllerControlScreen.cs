using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class UIControllerControlScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        // Brings player to the game screen when ready 
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void backToMenu()
    {
        // Returns players back to the menu screen
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

}
