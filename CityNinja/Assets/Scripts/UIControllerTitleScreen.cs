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
        UnityEngine.SceneManagement.SceneManager.LoadScene("ControlScreen");
    }

    public void controls()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ControlScreen");
    }
}
