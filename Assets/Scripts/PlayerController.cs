using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LevelManager levelManager;
    private PlayerManager playerManager;
    public CombatHandler combatHandler;

    private float horizontalInput;
    private bool isFacingRight = true;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector3 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerManager = FindObjectOfType<PlayerManager>();
        levelManager = FindObjectOfType<LevelManager>();
        combatHandler = FindObjectOfType<CombatHandler>();

        InvokeRepeating("SaveLastGroundedPosition", 1f, 2f);
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.7f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void SaveLastGroundedPosition()
    {
        if (isGrounded())
        {
            lastPosition = transform.position;
        }
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void ReloadPosition()
    {
        transform.position = lastPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OverworldEnemy enemy = collision.gameObject.GetComponent<OverworldEnemy>();
            if (enemy != null)
            {
                combatHandler.ReadEnemyData(enemy.enemyName, enemy.maxHP, enemy.currentHP, enemy.damage, enemy.coinDrop, enemy.GetComponentInChildren<SpriteRenderer>());
                enemy.Despawn();
                combatHandler.TriggerCombat();
            }
        }
        else if (isGrounded())
        {
            SaveLastGroundedPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            levelManager.EnterNextLevel();
        }
        else if (collision.gameObject.CompareTag("Abyss"))
        {
            playerManager.TakeDamage(Mathf.RoundToInt(playerManager.maxHP * 0.1f));
            ReloadPosition();
            FindObjectOfType<UI_OverworldHUD>().UpdateHPSlider(playerManager.currentHP, playerManager.maxHP);
        }
    }

}
