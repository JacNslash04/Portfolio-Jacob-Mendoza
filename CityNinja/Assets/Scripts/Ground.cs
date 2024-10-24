using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour{
    Player player;
    public float groundHeight;
    public float groundRight;
    public float screenRight;
    BoxCollider2D collider;

    bool groundGenerated = false;
    public GroundEnemy EnemyTemplateG;
    public FlyingEnemy EnemyTemplateF;
    public Heart SupportTemplateH;

    private void Awake(){
        player = GameObject.Find("Player").GetComponent<Player>();

        collider = GetComponent<BoxCollider2D>();
        groundHeight = transform.position.y + (collider.size.y / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Vector2 pos = transform.position;
        pos.x -= player.velocity.x * Time.fixedDeltaTime;

        groundRight = transform.position.x + (collider.size.x / 2);

        if (groundRight < 0){
            Destroy(gameObject);
            return;
        }
        if (!groundGenerated){
            if (groundRight < screenRight){
                groundGenerated = true;
                generateGround();
            }   
        }

        transform.position = pos;
    }
    void generateGround(){
        GameObject go = Instantiate(gameObject);
        BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        Vector2 pos; 

        float height1 = player.jumpVelocity * player.maxHoldJumpTime;
        float time = player.jumpVelocity / -player.gravity;
        float height2 = player.jumpVelocity * time + (0.5f * (player.gravity * (time * time)));
        float maxJumpHeight = height1 + height2;
        float maxY = maxJumpHeight * 0.7f;
        maxY += groundHeight;
        float minY = 1;
        float realY = Random.Range(minY, maxY);


        pos.x = screenRight + 30;
        pos.y = realY - goCollider.size.y / 2;
        if (pos.y > 2.7f){
            pos.y = 2.7f;
        }
        float time1 = time + player.maxHoldJumpTime;
        float time2 = Mathf.Sqrt((2.0f * (maxY - realY)) / -player.gravity);
        float totalTime = time1 + time2;
        float maxX = totalTime * player.velocity.x;
        maxX *= 0.7f;
        maxX += groundRight;
        float minX = screenRight + 5;
        float realX = Random.Range(minX, maxX);
       
        pos.x = realX + goCollider.size.x / 2;
        go.transform.position = pos;
        Ground goGround = go.GetComponent<Ground>();
        goGround.groundHeight = go.transform.position.y + (goCollider.size.y / 2);


        int groundEnemyNum = Random.Range(0, 2);
        for (int i = 0; i < groundEnemyNum; i++){
            GameObject groundEnemy = Instantiate(EnemyTemplateG.gameObject);
            float y = goGround.groundHeight;
            float halfWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            Vector2 enemyPosG = new Vector2(x, y);
            groundEnemy.transform.position = enemyPosG;
        }

        int flyingEnemyNum = Random.Range(0, 2);
        for (int i = 0; i < flyingEnemyNum; i++){
            GameObject flyingEnemy = Instantiate(EnemyTemplateF.gameObject);
            float y = Random.Range(20, 40);
            float halfWidth = goCollider.size.x / 2 - 1;
            float left = go.transform.position.x - halfWidth;
            float right = go.transform.position.x + halfWidth;
            float x = Random.Range(left, right);
            Vector2 enemyPosF = new Vector2(x, y);
            flyingEnemy.transform.position = enemyPosF;
        }

        int heartNum = Random.Range(0, 2);
        if (player.playerHealth < 3){
            for (int i = 0; i < heartNum; i++){
                GameObject heart = Instantiate(SupportTemplateH.gameObject);
                float y = goGround.groundHeight;
                float halfWidth = goCollider.size.x / 2 - 1;
                float left = go.transform.position.x - halfWidth;
                float right = go.transform.position.x + halfWidth;
                float x = Random.Range(left, right);
                Vector2 heartPosH = new Vector2(x, y);
                heart.transform.position = heartPosH;
            }
        }
    }
}
