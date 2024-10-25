using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 5f;
    public LayerMask enemyLayers;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets up attack function/animations once the player
        // presses the Attack key [F]
        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            animator.SetBool("isAttacking", false);
        }

    }

    private void Attack()
    {
        // Sets up collision check between the player attack and enemies 
        animator.SetBool("isAttacking", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            // Console log to check if enemies are being hit correctly
            Debug.Log("Enemy hit");
            // Destroys enemy object on valid hit 
            Destroy(enemy.gameObject);
        }

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}