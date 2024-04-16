using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float speed = 1;
    public float jumpForce = 3;
    public float damage = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private bool _isAttacking;
    private const string RunAnimation = "Run";
    private const string Attack1Animation = "Attack1";
    private const string Attack2Animation = "Attack2";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float movement = Input.GetAxis("Horizontal");

        SetRunAnimation(movement);
        SetSpriteFlip(movement);
        OnMove(movement);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.5f)
            OnJump();

        if (Input.GetMouseButtonDown(0) && _isAttacking == false)
            SetAttackAnimation();

    }

    private void SetAttackAnimation()
    {
        if (Random.value > 0.5f)
            animator.SetTrigger(Attack1Animation);
        else
            animator.SetTrigger(Attack2Animation);
    }

    private void OnJump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void OnMove(float movement)
    {
        //transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    private void SetRunAnimation(float movement)
    {
        if (movement != 0)
            animator.SetBool(RunAnimation, true);
        else
            animator.SetBool(RunAnimation, false);
    }

    private void SetSpriteFlip(float movement)
    {
        if (movement < 0)
            sr.flipX = true;
        else if (movement > 0)
            sr.flipX = false;
    }

    private void StartAttacking()
    {
        _isAttacking = true;
    }

    private void EndAttacking()
    {
        _isAttacking = false;
    }

    private void Attack()
    {
        Vector2 attackPosition = new Vector2(
            transform.position.x, transform.position.y + (transform.localScale.y / 2));

        Vector2 attackDirection = sr.flipX == false ? Vector2.right : Vector2.left;

        RaycastHit2D[] hits = Physics2D.RaycastAll(attackPosition, attackDirection);

        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Enemy" && hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamange(damage);
                print(hit.collider.name);
            }
        }

        
    }

}