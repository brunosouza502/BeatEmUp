//using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General References")]
    private Rigidbody2D rigidbody2D;
    private GameObject player;

    private Animator animator;

    [Header("Enemy Movement")]
    // SerializeField: Appears in the Unity Inspector even though the field is private.
    [SerializeField] float enemySpeed;
    private Vector2 movementInput;
    private Vector2 movementDirection; // Direction of the enemy's movement.

    [Header("Enemy Attack")]
    [SerializeField] private float attackDistance; // Distance from the player at which the enemy attacks.
    private bool canAttack;
    [SerializeField] private float maxAttackInterval; // Time the enemy must wait between attacks.
    private float currentAttackInterval; // Time elapsed since the enemy's last attack.

    [SerializeField] private int enemyAttackCount; // Includes the strong and weak punches.
    [SerializeField] private int currentEnemyAttack;

    [Header("Movement Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindObjectOfType<PlayerController>().gameObject; // Gets the player object, which has the PlayerController script.
        canAttack = false;
        //Debug.Log("Enemy initialized: " + gameObject.name + " | Player: " + player.name + " " + player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Prevents active movement animations from running after defeat.
        if(GetComponent<EnemyHealth>().IsEnemyAlive())
        {
            //Debug.Log(GetComponent<EnemyHealth>().IsEnemyAlive().ToString());
            RunAttackTimer();
            FlipEnemy();
            FollowPlayer();
        }
        // The else is required to play the defeat animation.
        else
        {
            PlayDefeatAnimation();
        }

    }

    private void RunAttackTimer()
    {
        // Limits consecutive attacks so the enemy cannot attack the player continuously.
        currentAttackInterval -= Time.deltaTime;
        if(currentAttackInterval <= 0)
        {
            canAttack = true;
            currentAttackInterval = maxAttackInterval; // Resets the timer.
        }
    }

    // Makes the enemy face the player by flipping its sprite.
    private void FlipEnemy()
    {
        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } 

        else if(player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
    }

    private void FollowPlayer()
    {
        // Moves toward the player when the attack distance has not been reached.
        if(Vector2.Distance(transform.position, player.transform.position) > attackDistance)
        {
            // Calculates the direction from the enemy to the player.
            movementDirection = (player.transform.position - transform.position).normalized;
        
            rigidbody2D.linearVelocity = movementDirection * enemySpeed;
            animator.SetTrigger("andando");// The "andando" trigger is defined in the Animator.
            //canAttack = false;


            // Limits horizontal player movement.
            rigidbody2D.position = new Vector2(Mathf.Clamp(rigidbody2D.position.x, minX, maxX), rigidbody2D.position.y);

            // Limits vertical player movement.
            rigidbody2D.position = new Vector2(rigidbody2D.position.x, Mathf.Clamp(rigidbody2D.position.y, minY, maxY));
        }
        else
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            animator.SetTrigger("parado");// The "parado" trigger is defined in the Animator.
            //canAttack = true;

            SelectEnemyAttack();
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // First version of the enemy attack.
    private void AttackPlayer()
    {
        // Implement the enemy attack logic here, such as damaging the player or playing an attack animation.
        animator.SetTrigger("socando"); // Plays the enemy attack animation.
        //Debug.Log("Inimigo atacando o jogador!");
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void RunAnimations()
    {
        if(movementDirection.magnitude == 0)
        {
            animator.SetTrigger("parado");// The "parado" trigger is defined in the Animator.
        }

        else if(movementDirection.magnitude != 0)
        {
            animator.SetTrigger("andando");// The "andando" trigger is defined in the Animator.
        }

        if(canAttack)
        {
            AttackPlayer();
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Randomly selects which attack the enemy will use.
    private void SelectEnemyAttack()
    {
        currentEnemyAttack = Random.Range(0, enemyAttackCount + 1); // Selects an attack from the configured range.
        if(canAttack)
        {
            StartAttack();
        }

        //Debug.Log("Current enemy attack: " + currentEnemyAttack);
    }

    private void StartAttack()
    {
        // Changes the attack to the selected one.
        if (currentEnemyAttack == 0)
        {
            animator.SetTrigger("socando-fraco"); // Plays the weak punch animation.
            //Debug.Log("Inimigo atacando o jogador!");
        }
        else if (currentEnemyAttack == 1)
        {
            animator.SetTrigger("socando-forte"); // Plays the strong punch animation.
            //Debug.Log("Inimigo atacando o jogador com soco forte!");
        }
        
        canAttack = false; // Prevents another attack until the attack timer resets.
        
    }

    public void PlayDamageAnimation()
    {
        animator.SetTrigger("levando-dano"); // Plays the enemy damage animation.
    }

    public void PlayDefeatAnimation()
    {
        animator.Play("inimigo-derrotado"); // Plays the defeat animation without using a trigger.
        rigidbody2D.linearVelocity = Vector2.zero; // Stops the enemy after defeat.
    }
}
