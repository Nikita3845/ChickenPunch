using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Chill, Angry, Patrol
    }

    [SerializeField] private float _speed;
    [SerializeField] private float _argRange;
    [SerializeField] private float _patrolDistance;
    [SerializeField] private State _state;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;

    private Vector2 _startPositionPatroling;
    private Vector2 _endPositionPatroling;

    private Transform _player;
    private Coroutine _patrolCoroutine;

    private const string RunAnimation = "Run";


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _startPositionPatroling = transform.position;
        _endPositionPatroling = transform.position + Vector3.right * _patrolDistance;
    }

    void Update()
    {
        SwitchState();
        StateHandler();
        SetRunAnimation();
        SetSpriteFlip(_rb.velocity.x);
    }

    private void SwitchState()
    {
        float distanceToPlayer = GetDistanceToPlayer();

        if (distanceToPlayer > _argRange)
            _state = State.Patrol;
        else
            _state = State.Angry;
    }

    private void StateHandler()
    {
        switch (_state)
        {
            case State.Patrol:
                StartPatroling();
                break;
            case State.Angry:
                StopPatroling();
                MoveTo(_player.position);
                break;
        }
    }

    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, _player.position);
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            if (_state != State.Patrol)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(2);
            while (Mathf.Abs(transform.position.x - _endPositionPatroling.x) >= 0.1)
            {
                MoveTo(_endPositionPatroling);
                yield return null;
            }
            StopMove();

            yield return new WaitForSeconds(2);
            while (Mathf.Abs(transform.position.x - _startPositionPatroling.x) >= 0.1)
            {
                MoveTo(_startPositionPatroling);
                yield return null;
            }
            StopMove();
        }
    }

    private void StartPatroling()
    {
        if (_patrolCoroutine == null)
        {
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _patrolCoroutine = StartCoroutine(Patroling());
        }
    }

    private void StopPatroling()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
            _rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void SetRunAnimation()
    {
        if (Mathf.Abs(Mathf.Ceil(_rb.velocity.x)) != 0)
            _animator.SetBool(RunAnimation, true);
        else
            _animator.SetBool(RunAnimation, false);
    }

    private void MoveTo(Vector2 target)
    {
        float direction = Math.Sign(target.x - transform.position.x);

        Vector2 moveVelocity = new Vector2(direction * _speed, _rb.velocity.y);
        _rb.velocity = moveVelocity;
    }

    private void StopMove()
    {
        _rb.velocity = Vector2.zero;
    }

    private void SetSpriteFlip(float direction)
    {
        if (direction < 0)
            _sr.flipX = false;
        else if (direction > 0)
            _sr.flipX = true;
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.2f;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_startPositionPatroling, sphereRadius);
        Gizmos.DrawSphere(_endPositionPatroling, sphereRadius);
        Gizmos.DrawLine(_startPositionPatroling, _endPositionPatroling);
    }


}
