using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General References")]
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    [Header("Player Movement")]
    // SerializeField: Appears in the Unity Inspector even though the field is private.
    [SerializeField] float playerSpeed;
    Vector2 movementInput; // Stores the W, A, S, and D keys.
    Vector2 movementDirection; // Direction of the player's movement.

    [Header("Movement Limits")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;


    // Tracks the time between the player's attacks.
    [Header("Attack Control")]
    [SerializeField] private float maxAttackInterval; // Time the player must wait between attacks. Configured in the Unity Inspector.
    private float currentAttackInterval; // Time elapsed since the player's last attack.
    private bool canAttack;
    private int combo;
    [SerializeField] private float maxComboTime; // Time available for the player to continue the combo, configured in the Unity Inspector.
    private float currentComboTime;
    private int totalCombo; // Stores the player's longest combo, displayed at the end of the game.
    [SerializeField] private Combo[] comboSet;
    private int attackIndex;// Index of the current attack in the combo set,whether is punch (J) or kick (K).

    /*public int TotalCombo
    {
        get => totalCombo;
        set => totalCombo = value;
    }*/

    // Punch sequence when the player keeps pressing the punch button.
    [SerializeField] private float maxSequenceTime;
    private float currentSequenceTime;
    private bool sequenceStarted;
    private int sequence;
    [SerializeField] private string[] animationName;

    [Header("Damage Control")]
    [SerializeField] private float maxDamageTime; // Time the player takes to recover from damage, configured in the Unity Inspector.
    private float currentDamageTime; // Time elapsed since the player last took damage.
    private bool tookDamage; // Prevents the player from taking damage continuously.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Current attack interval: " + currentAttackInterval);
        combo = 0; // Initializes the player's combo.
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentDamageTime = maxDamageTime; // Initializes the damage timer so the player can take damage when the game starts.

        currentComboTime = maxComboTime; // Initializes the combo timer so the player can attack when the game starts.

        currentSequenceTime = maxSequenceTime;

        sequence = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerHealth>().IsPlayerAlive())
        {
            RunAttackTimer();

            // Prevents movement while the player is taking damage.
            if (!tookDamage)
            {
                ReadInput();
                RunAnimations();

                RunAttackAnimations();

                FlipPlayer();
                MovePlayer();

                RunComboTimer(); 
            }
            else
            {
                RunDamageTimer();
            }
        }
        else
        {
            PlayDefeatAnimation();
        }
        
    }

    // Controls the time between attacks so the player cannot attack indefinitely.
    private void RunAttackTimer()
    {
        currentAttackInterval -= Time.deltaTime; // Counts down the attack interval every second.
        if(currentAttackInterval <= 0)
        {
            canAttack = true;
            currentAttackInterval = maxAttackInterval; // Resets the timer.
        }
    }

    private void RunDamageTimer()
    {
        currentDamageTime -= Time.deltaTime; // Counts down the damage timer every second.
        if(currentDamageTime <= 0)
        {
            tookDamage = false;
            currentDamageTime = maxDamageTime; // Resets the timer.
        }
    }

    // Combo timer and management.
    private void RunComboTimer()
    {
        if(combo > 0)
        {
            UIManager.instance.EnableCombo();

            currentComboTime -= Time.deltaTime;
            //Debug.Log("Combo atual: " + combo);

            if (currentComboTime <= 0)
            {
                if (combo > totalCombo)
                {
                    totalCombo = combo; // Stores the player's longest combo.
                    UIManager.instance.MaxCombo(totalCombo);//Display longest combo in score screen
                }
                
                combo = 0; // Resets the player's combo.
                //currentComboTime = maxComboTime; // Resets the combo timer.

                sequence = 0;//Resets this counter to start over punch sequence over

                UIManager.instance.DisableCombo();

                //Debug.Log("Combo reset.");
                //Debug.Log("Current total combo: " + totalCombo);
            }
        }

    }

    public void CountCombo()
    {
        combo++;
        currentComboTime = maxComboTime;

        UIManager.instance.UpdateComboCounter(combo); // Updates the combo counter.
        //Debug.Log("Current combo: " + combo);
    }

    //////////////////////////////////////////////////////////////////////////////////

    // Stores the player's movement direction according to the pressed keys.
    private void ReadInput()
    {
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Damage test.
        if(Input.GetKeyDown(KeyCode.L))
        {
            //GameObject.FindObjectOfType<PlayerHealth>().TakeDamage(2);
            GetComponent<PlayerHealth>().TakeDamage(2);
            PlayDamageAnimation();
        }
    }

    private void RunAnimations()
    {
        
        animator.SetBool("parado", movementInput.magnitude == 0);
        animator.SetBool("andando", movementInput.magnitude > 0);

    }

    private void RunAttackAnimations()
    {
        if (Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            attackIndex = 0; // Punch attack index.
            Attack(attackIndex);
        }
        else if (Input.GetKeyDown(KeyCode.K) && canAttack)
            {
                attackIndex = 1; // Kick attack index.
                Attack(attackIndex);
            }
        else
            return; // No attack input detected.
        /*{
            //Debug.Log("Pressed: " + combo[sequence].hits[sequence].button);
            //animator.SetTrigger(comboSet[0].hits[sequence].triggerAnim);
            canAttack = false; // Prevents another attack until the attack timer resets.
            SoundManager.instance.punchImpact.Play();

            if (combo <= 0)
            {
                sequence = 0;
            }

            //animator.SetTrigger(animationName[sequence]);

            if (combo > 0)
            {
                sequence++;
                //animator.SetTrigger(animationName[sequence]);

                if (sequence >= animationName.Length)
                {
                    sequence = 0;
                    //animator.SetTrigger(animationName[sequence]);
                }
            }
            animator.SetTrigger(comboSet[attackIndex].hits[sequence].triggerAnim);
            Debug.Log("Sequence atual: " + sequence);
        }*/
        
    }

    private void Attack(int _attackIndex)
    {
        canAttack = false; // Prevents another attack until the attack timer resets.
        SoundManager.instance.punchImpact.Play();

        if (combo <= 0)
        {
            sequence = 0;
        }

        //animator.SetTrigger(animationName[sequence]);

        if (combo > 0)
        {
            sequence++;
            //animator.SetTrigger(animationName[sequence]);

            if (sequence >= animationName.Length)
            {
                sequence = 0;
                //animator.SetTrigger(animationName[sequence]);
            }
        }
        animator.SetTrigger(comboSet[_attackIndex].hits[sequence].triggerAnim);
        Debug.Log("Sequence atual: " + sequence);
    }

    // Turns the player toward the movement direction by flipping the sprite.
    private void FlipPlayer()
    {
     
        if(movementInput.x == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 

        else if(movementInput.x == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    private void MovePlayer()
    {
        // Prevents the player from attacking and moving at the same time.
        if(!canAttack)
        {
            rigidbody2D.linearVelocity = Vector2.zero; // Stops the player while attacking.
        }

        else
        {
            movementDirection = movementInput.normalized;
            rigidbody2D.linearVelocity = movementDirection * playerSpeed;

            // Limits horizontal player movement.
            rigidbody2D.position = new Vector2(Mathf.Clamp(rigidbody2D.position.x, minX, maxX), rigidbody2D.position.y);
            
            // Limits vertical player movement.
            rigidbody2D.position = new Vector2(rigidbody2D.position.x, Mathf.Clamp(rigidbody2D.position.y, minY, maxY));
        }
        // Moves the player based on the input direction and player speed.
        //movementDirection = movementInput.normalized; // Normalizes the vector so speed is the same in every direction.
        //rigidbody2D.linearVelocity = movementDirection * playerSpeed; // Velocity is a Vector2, so it can be multiplied directly.
    }

    // Called by the player's health component to play the player's damage animation.
    public void PlayDamageAnimation()
    {
        animator.SetTrigger("levando-dano");// The "levando-dano" trigger is defined in the Animator.
        tookDamage = true; // Prevents the player from taking damage continuously.

        rigidbody2D.linearVelocity = Vector2.zero; // Stops the player while taking damage.
    }

    public void PlayDefeatAnimation()
    {
        animator.Play("jogador-derrotado");// The "jogador-derrotado" animation is defined in the Animator.
    }
}
