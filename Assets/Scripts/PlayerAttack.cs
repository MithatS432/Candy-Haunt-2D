using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 20f;
    [SerializeField] private Collider2D attackCollider;
    public float attackDuration = 0.3f;
    public float attackCooldown = 0.2f;

    private bool canAttack = true;
    private bool isAttacking = false;

    private void Awake()
    {
        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    public void Attack()
    {
        if (canAttack && !isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        canAttack = false;
        isAttacking = true;

        // Collider'ı aç
        attackCollider.enabled = true;

        // Collider açık kalma süresi
        yield return new WaitForSeconds(0.5f);

        // Collider'ı kapat
        attackCollider.enabled = false;
        isAttacking = false;

        // Saldırı bekleme süresi
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isAttacking && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(damage);
            }
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
