using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    Player player;

    private void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        // Controls how health gets added after picking up a heart power up 
        // and its attached sprite's appearance
        for (int i = 0; i < hearts.Length; i++){
            if (health > numOfHearts){
                health = numOfHearts;
            }
            if (i < health){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts){
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
        health = player.playerHealth;
    }
}
