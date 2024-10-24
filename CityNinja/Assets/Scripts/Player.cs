using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    // Start is called before the first frame update
    public float gravity;
    public Vector2 velocity;
    public float acceleration = 10;
    public float distance = 0;
    public float maxAcceleration = 10;
    public float jumpVelocity = 20; 
    public float maxXVelocity = 100;
    public float groundHeight = 10; 
    public bool isGrounded = false;
    public bool isHoldingJump = false;
    public float maxHoldJumpTime = 0.0f;
    public float capHoldJumpTime = 0.4f;
    public float holdJumpTimer = 0.0f;
    public float jumpGroundThreshold = 1;
    public bool isDead = false;
    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;
    public LayerMask powerUpsLayerMask;
    public int playerHealth = 5;
    public Animator animator;
    public bool isInvincible = false;
    public float invincibleTimer = 0.0f;
    public float maxInvincibleTime = 5.0f;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        // Ends game after player hits death state
        Vector2 pos = transform.position;
        if (isDead){
            return;
        }
        // Causes player to hit death state if player falls off a building
        if (pos.y < -20){
            playerHealth = 0;
            velocity.x = 0;
            isDead = true;
        }
        // Provides player with a brief period of invincibility after getting hit 
        if (isInvincible){
            invincibleTimer += Time.fixedDeltaTime;
            if (invincibleTimer >= maxInvincibleTime){
                isInvincible = false;
                invincibleTimer = 0.0f;
            }
        }
        // Controls the player's jump
        // Allows player to jump so long as player is touching the ground 
        // Double jumping to be added later on 
        float groundDistance = Mathf.Abs(pos.y - groundHeight);
        if (isGrounded || groundDistance <= jumpGroundThreshold){
            if (Input.GetKeyDown(KeyCode.Space)){
                isGrounded = false;
                velocity.y = jumpVelocity;
                isHoldingJump = true;
                holdJumpTimer = 0;
                animator.SetBool("isJumping", true);
            }
        }
        // Resets jumping state after player lets go of jump key (Spacebar)
        if (Input.GetKeyUp(KeyCode.Space)){
            isHoldingJump = false;
            animator.SetBool("isJumping", false);
        }
    
    //Everything below this line is supposed to technically be in a FixedUpdate function rather than an Update function.
    //As of writing these comments I believe moving the code segment will probably be better for movement 
    //However moving the code segment over causes problems and breaks the game so I am still looking for a fix
        
        // Controls physics of player jump
        if (!isGrounded){
            pos.y += velocity.y * Time.fixedDeltaTime;
            holdJumpTimer += Time.fixedDeltaTime;
                // Sets the maximum amount of time a player can hold the jump key for before character starts to fall
               if (holdJumpTimer >= maxHoldJumpTime){
                 isHoldingJump = false;
               }
            
            // Controls physics of player falling back to the ground
            if (!isHoldingJump){
                velocity.y += gravity * Time.fixedDeltaTime;
            }
            // Controls the collision of the player with the ground
            Vector2 rayOrigin = new Vector2(pos.x + 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if (hit2D.collider != null){
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null){
                    if (pos.y >= ground.groundHeight){
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;
                        velocity.y = 0;
                        isGrounded = true;
                    }
                }
            }
            // Debug ray written to track collision during initial coding 
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        // Controls incrementing speed as the player progresses through the game
            Vector2 wallOrigin = new Vector2(pos.x, pos.y);
            RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, groundLayerMask);
            if (wallHit.collider != null){
                Ground ground = wallHit.collider.GetComponent<Ground>();
                if (ground != null){
                    if (pos.y < ground.groundHeight){
                        velocity.x = 0;
                    }
                }
            }
        }
        distance += velocity.x * Time.fixedDeltaTime;
        
        if (isGrounded){
            float velocityRatio = velocity.x / maxXVelocity;
            acceleration = maxAcceleration * (1 - velocityRatio);
            maxHoldJumpTime = capHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;
            if (velocity.x >= maxXVelocity){
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.7f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null){
               isGrounded = false;
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
            animator.SetBool("isJumping", false);
        }

        // Sets up horizontal collision system for enemies
        // Causes player to lose health after colliding with enemies horizontally
        Vector2 enemyOriginX = new Vector2(pos.x, pos.y);
        RaycastHit2D enemyHitX = Physics2D.Raycast(enemyOriginX, Vector2.right, velocity.x * Time.fixedDeltaTime, obstacleLayerMask);
        if(enemyHitX.collider != null){  
            GroundEnemy groundEnemy = enemyHitX.collider.GetComponent<GroundEnemy>();
            if (groundEnemy != null){
                if (!isInvincible){
                    hitGroundEnemy(groundEnemy);
                }
            } 
            FlyingEnemy flyingEnemy = enemyHitX.collider.GetComponent<FlyingEnemy>();
            if (flyingEnemy != null){
                if (!isInvincible){       
                    hitFlyingEnemy(flyingEnemy);
                }
            }
        }

        // Sets up vertical collision system for enemies
        // Causes player to lose health after colliding with enemies vertically
        Vector2 enemyOriginY = new Vector2(pos.x, pos.y);
        RaycastHit2D enemyHitY = Physics2D.Raycast(enemyOriginY, Vector2.up, velocity.y * Time.fixedDeltaTime, obstacleLayerMask);
        if(enemyHitY.collider != null){
            GroundEnemy groundEnemy = enemyHitY.collider.GetComponent<GroundEnemy>();
            if (groundEnemy != null){  
                if(!isInvincible){
                    hitGroundEnemy(groundEnemy);
                }
            }

            FlyingEnemy flyingEnemy = enemyHitY.collider.GetComponent<FlyingEnemy>();
            if (flyingEnemy != null){
                if(!isInvincible){
                    hitFlyingEnemy(flyingEnemy);
                }
            }     
        }

        // Sets up horizontal collision for heart pick up
        // Adds one heart to player health
        Vector2 heartOriginX = new Vector2(pos.x, pos.y);
        RaycastHit2D heartHitX = Physics2D.Raycast(heartOriginX, Vector2.right, velocity.x * Time.fixedDeltaTime, powerUpsLayerMask);
        if (heartHitX.collider != null){
            Heart heart = heartHitX.collider.GetComponent<Heart>();
            if (heart != null){
                hitHeart(heart);
            }
        }

        // Sets up vertical collision for heart pick up
        // Adds one heart to player health
        Vector2 heartOriginY = new Vector2(pos.x, pos.y);
        RaycastHit2D heartHitY = Physics2D.Raycast(heartOriginY, Vector2.up, velocity.y * Time.fixedDeltaTime, powerUpsLayerMask);
        if (heartHitY.collider != null){
            Heart heart = heartHitY.collider.GetComponent<Heart>();
            if (heart != null){
                hitHeart(heart);
            }
        }
        transform.position = pos;
    }

    // Method that gets called when player collides with a ground enemy
    void hitGroundEnemy(GroundEnemy groundEnemy){
        // Removes enemy from Scene
        Destroy(groundEnemy.gameObject);
        // Begins player's invincibility phase
        isInvincible = true;
        // Takes on heart away from player
        playerHealth = playerHealth - 1;
        // End game if player loses last heart
        if (playerHealth <= 0){
            isDead = true;
        }
    }

    // Method that gets called whenever player collides with a flying enemy
    void hitFlyingEnemy(FlyingEnemy flyingEnemy){
        // Removes enemy from Scene
        Destroy(flyingEnemy.gameObject);
        // Begin's player's invincibility phase
        isInvincible = true;
        // Takes one heart away from player
        playerHealth = playerHealth - 1;
        // End game if player loses last heart 
        if (playerHealth <= 0){
            isDead = true;
        }
    }

    // Method that gets called whenever player collides with heart pick up
    void hitHeart(Heart heart){
        // Removes heart from Scene
        Destroy(heart.gameObject);
        // Adds one heart to player health
        playerHealth = playerHealth + 1;
        // If player health after picking up heart is over the max health, the heart does nothing
        if (playerHealth > 5){
            playerHealth = 5;
        }
    }
}



