using UnityEngine;
using System.Collections;
using Microsoft.Win32.SafeHandles;
using System.Security.Cryptography.X509Certificates;

public class PlayerControl : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    [Header("Movement")]
    private float speed = 5f;
    private float dashSpeed = 20f;
    private float dashTime = 0.5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash());
        }
        if (spr.flipX)
        {
            spr.flipX = true;
        }
        else
        {
            spr.flipX = false;
        }

    }
    IEnumerator Dash()
    {
        rb.AddForce(transform.right * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        rb.linearVelocity = Vector2.zero;
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, y, 0) * speed * Time.fixedDeltaTime;
    }
}
