using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _jumpForce = 3;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private Joystick _joystick;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private bool _isAttacking;
    private const string RunAnimation = "Run";
    private const string Attack1Animation = "Attack1";
    private const string Attack2Animation = "Attack2";

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float movement = _joystick.Horizontal;

#if UNITY_EDITOR
        //movement = Input.GetAxisRaw("Horizontal");
#endif


        SetRunAnimation(movement);
        SetSpriteFlip(movement);
        OnMove(movement);

        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_rb.velocity.y) < 0.1f)
            OnJump();

#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //    SetAttackAnimation();
#endif

    }


    public void SetAttackAnimation()
    {
        if (_isAttacking == true)
            return;

        if (Random.value > 0.5f)
            _animator.SetTrigger(Attack1Animation);
        else
            _animator.SetTrigger(Attack2Animation);
    }

    public void JumpBtn()
    {
        OnJump();
    }

    private void OnJump()
    {
        if(Mathf.Abs(_rb.velocity.y) < 0.1f)
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnMove(float movement)
    {
        _rb.velocity = new Vector2(movement * _speed, _rb.velocity.y);
    }

    private void SetRunAnimation(float movement)
    {
        if (movement != 0)
            _animator.SetBool(RunAnimation, true);
        else
            _animator.SetBool(RunAnimation, false);
    }

    private void SetSpriteFlip(float movement)
    {
        if (movement < 0)
            _sr.flipX = true;
        else if (movement > 0)
            _sr.flipX = false;
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

        Vector2 attackDirection = _sr.flipX == false ? Vector2.right : Vector2.left;

        RaycastHit2D[] hits = Physics2D.RaycastAll(attackPosition, attackDirection, _attackDistance);

        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Enemy" && hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);
            }
        }

        
    }

}