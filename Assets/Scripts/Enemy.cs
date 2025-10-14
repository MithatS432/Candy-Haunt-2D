using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D erb;
    public float damage;
    public float health;
    public float speed;
    private Transform player;

    public AudioSource deathSound;
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
            Destroy(gameObject);
            deathSound.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerControl>().GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
