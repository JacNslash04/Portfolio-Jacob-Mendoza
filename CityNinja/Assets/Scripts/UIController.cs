using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distanceText;
    GameObject GameOverScreen;
    TextMeshProUGUI finalDistanceText;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        finalDistanceText = GameObject.Find("FinalDistanceText").GetComponent<TextMeshProUGUI>();
        GameOverScreen = GameObject.Find("GameOverScreen");
        GameOverScreen.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m";

        if(player.isDead)
        {
            GameOverScreen.SetActive(true);
            finalDistanceText.text = distance + " m!";
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
