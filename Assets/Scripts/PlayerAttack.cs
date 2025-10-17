using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage = 20f;

    private Animator anim;
    [SerializeField] private Collider2D attackCollider;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void EnableAttackCollider()
    {
        if (attackCollider != null)
            attackCollider.enabled = true;
    }

    public void DisableAttackCollider()
    {
        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.GetDamage(damage);
            }
        }
    }
}
