using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour{
    Player player;
    private void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector2 pos = transform.position;
        pos.x -=  player.velocity.x * Time.fixedDeltaTime;
        if (pos.x < -100)
        {
            Destroy(gameObject);
        }


        transform.position = pos;
    }
}
