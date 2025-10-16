using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    private float damage = 20f;
    private float attackRange = 1f;
    [SerializeField] private LayerMask enemyLayer;
    private Animator anim;
    private SpriteRenderer spr;

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        if (enemyLayer.value == 0)
            enemyLayer = LayerMask.GetMask("Enemy");
    }

    public void Attack()
    {
        Debug.Log("Attack method called!");

        Vector2 attackPos = (Vector2)transform.position + (GetAttackDirection() * attackRange * 0.5f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos, attackRange, enemyLayer);

        Debug.Log("Hit " + hitEnemies.Length + " enemies");

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit enemy: " + enemy.name);
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemyComponent.GetDamage(damage);
            }
        }
    }

    private Vector2 GetAttackDirection()
    {
        if (spr == null)
        {
            spr = GetComponent<SpriteRenderer>();
            if (spr == null)
                return Vector2.right;
        }
        return spr.flipX ? Vector2.left : Vector2.right;
    }


    void OnDrawGizmosSelected()
    {
        if (spr == null)
            spr = GetComponent<SpriteRenderer>();

        if (spr == null)
            return;

        Gizmos.color = Color.red;
        Vector2 attackPos = (Vector2)transform.position + (GetAttackDirection() * attackRange * 0.5f);
        Gizmos.DrawWireSphere(attackPos, attackRange);
    }

}