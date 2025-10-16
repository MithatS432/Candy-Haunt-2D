using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float damage = 20f;
    public float attackDuration = 0.2f;

    [Header("References")]
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Transform attackPoint;
    private Animator anim;
    private SpriteRenderer spr;

    private List<Collider2D> hitEnemies = new List<Collider2D>(); 

    void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        if (attackCollider != null)
            attackCollider.enabled = false;
    }

    void Update()
    {
        if (attackCollider != null && attackPoint != null)
        {
            attackCollider.transform.position = attackPoint.position;

            Vector3 scale = attackCollider.transform.localScale;
            scale.x = spr.flipX ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            attackCollider.transform.localScale = scale;
        }
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        hitEnemies.Clear(); 
        StartCoroutine(EnableColliderMomentarily());
    }

    private IEnumerator EnableColliderMomentarily()
    {
        attackCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !hitEnemies.Contains(other))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                // Düşmana hasar ver
                enemy.GetDamage(damage);
                
                hitEnemies.Add(other); 
            }
        }
    }
}