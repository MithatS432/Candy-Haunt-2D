using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private float dashDistance = 5f;
    private float dashDuration = 0.15f;
    private float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private bool isRunning = false;

    [Header("Effects")]
    public GameObject dashEffectPrefab;
    [Header("Audio")]
    public AudioClip dashSound, attackSound, spearThrowSound;

    [Header("Attack Settings")]
    private int attackCombo = 0;
    private float comboResetTime = 2f;
    private float lastAttackTime;
    private bool isAttacking = false;
    private float attackCooldown = 0.4f;
    public GameObject spearPrefab;

    [Header("Character Settings")]
    private float damage = 20f;
    private int health = 100;

    [Header("UI Components")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Time.time - lastAttackTime > comboResetTime && attackCombo > 0)
        {
            attackCombo = 0;
            anim.SetInteger("AttackCombo", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDir = ((Vector2)(mousePos - transform.position)).normalized;
            StartCoroutine(Dash(dashDir));
        }

        if (Input.GetMouseButtonDown(0) && !isAttacking && Time.time - lastAttackTime > attackCooldown)
        {
            if (attackSound != null)
            {
                AudioSource.PlayClipAtPoint(attackSound, transform.position);
            }
            StartCoroutine(PerformAttack());
        }

        if (Input.GetMouseButtonDown(1) && spearPrefab != null)
        {
            if (spearThrowSound != null)
            {
                AudioSource.PlayClipAtPoint(spearThrowSound, transform.position);
            }
            anim.SetTrigger("SpearThrow");
            GameObject spear = Instantiate(spearPrefab, transform.position, Quaternion.identity);
            Destroy(spear, 2f);
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        attackCombo++;
        if (attackCombo > 2) attackCombo = 1;

        anim.SetInteger("AttackCombo", attackCombo);
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
    private IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;

        Vector2 startPos = rb.position;
        Vector2 targetPos = startPos + direction * dashDistance;

        float elapsed = 0f;
        float effectSpawnRate = 0.05f;
        float effectTimer = 0f;

        while (elapsed < dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, targetPos, elapsed / dashDuration));

            effectTimer += Time.deltaTime;
            if (effectTimer >= effectSpawnRate)
            {
                if (dashEffectPrefab)
                {
                    GameObject effect = Instantiate(dashEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(effect, 0.5f);
                    if (dashSound != null)
                    {
                        AudioSource.PlayClipAtPoint(dashSound, transform.position);
                    }
                }
                effectTimer = 0f;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.MovePosition(targetPos);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(x, y).normalized;

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
        anim.SetFloat("Speed", move.magnitude);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!isRunning)
            {
                anim.SetTrigger("Run");
                isRunning = true;
            }
            moveSpeed = 8f;
        }
        else
        {
            moveSpeed = 5f;
            isRunning = false;
        }

        if (x < 0)
            spr.flipX = true;
        else if (x > 0)
            spr.flipX = false;
    }
}