using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D erb;
    public float damage;
    public float health;
    public float speed;
    private Transform player;

    public AudioClip deathSound;
    public GameObject deathEffect;
    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        erb.linearVelocity = direction * speed;
    }
    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().GetDamage(damage);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(gameObject);
        }
    }
}
