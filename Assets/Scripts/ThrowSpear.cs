using UnityEngine;

public class ThrowSpear : MonoBehaviour
{
    private float speed = 10f;
    private float damage = 15f;
    private Vector2 direction;
    void Start()
    {
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyHealth = other.GetComponent<Enemy>();
            if (enemyHealth != null)
            {
                enemyHealth.GetDamage(damage);
            }
            Destroy(gameObject);
            BossEnemySummon summonEnemyHealth = other.GetComponent<BossEnemySummon>();
            if (summonEnemyHealth != null)
            {
                summonEnemyHealth.GetDamage(damage);
            }
            BossEnemy bossEnemy = other.GetComponent<BossEnemy>();
            if (bossEnemy != null)
            {
                bossEnemy.GetDamage(damage);
            }
        }
    }
}
