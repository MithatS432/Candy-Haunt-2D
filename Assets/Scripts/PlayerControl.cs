using UnityEngine;
using System.Collections;

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
    public AudioClip dashSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dashDir = ((Vector2)(mousePos - transform.position)).normalized;
            StartCoroutine(Dash(dashDir));
        }
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
