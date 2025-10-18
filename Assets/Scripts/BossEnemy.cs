using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private GameObject player;

    public float speed;
    public float health;
    public float damage;

    public AudioClip deathSound, spawnSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform.gameObject;
    }

    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }
    public void GetDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            if (deathSound != null)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
    public void BossSpawn()
    {
        if (spawnSound != null)
        {
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
        }
    }
}
