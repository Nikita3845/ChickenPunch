using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Chill, Angry, Patrol
    }

    public float argRange;
    public float patrolDistance;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private State state;

    private Vector2 startPositionPatroling;
    private Vector2 endPositionPatroling;
    private Vector2 currentPositionPatroling;

    private Vector2 target;


    public float speed;

    public int positionOfPatrol;
    public Transform point;



    bool moveRight;

    Transform player;
    public float stop;


    bool chill = false;
    bool angry = false;
    bool back = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPositionPatroling = transform.position;
        endPositionPatroling = transform.position + Vector3.right * patrolDistance;
        currentPositionPatroling = endPositionPatroling;
        StartCoroutine(Patroling());
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = GetDistanceToPlayer();
        


        if (distanceToPlayer > argRange)
            state = State.Patrol;
        else
            state = State.Angry;

        //if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false) 
        //{
        //    chill = true;
        //}
        //if(Vector2.Distance(transform.position, player.position) < stop) 
        //{
        //    angry = true;
        //    chill = false;
        //    back = false;
        //}
        //if (Vector2.Distance(transform.position, player.position) > stop)
        //{
        //    back = true;
        //    angry = false;
        //}


        //if(chill == true)
        //{
        //    Chill();
        //}
        //else if(angry == true)
        //{
        //    Angry();
        //}
        //else if (back == true)
        //{
        //    Back();
        //}
        //SetSpriteFlip(MathF.Sign(rb.velocity.x));


    }
    private void ChangeTarget()
    {
        currentPositionPatroling = currentPositionPatroling == startPositionPatroling ? startPositionPatroling : endPositionPatroling;
    }


    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(transform.position, player.position);
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol) 
        {
            moveRight = true;
        }
        else if(transform.position.x < point.position.x - positionOfPatrol)
        {
            moveRight = false;
        }

        if (moveRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void Back()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }

    private IEnumerator Patroling()
    {
        while (true)
        {
            float direction = Mathf.Sign(endPositionPatroling.x - transform.position.x);
            SetSpriteFlip(direction);
            while (Vector2.Distance(transform.position, endPositionPatroling) > 0.1f)
            {
                MoveTo(endPositionPatroling);
                yield return null;
            }

            yield return new WaitForSeconds(1);

            direction = Mathf.Sign(startPositionPatroling.x - transform.position.x);
            SetSpriteFlip(MathF.Sign(direction));
            while (Vector2.Distance(transform.position, startPositionPatroling) > 0.1f)
            {
                MoveTo(startPositionPatroling);
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }
    }

    public float DirectionX;
    private void MoveTo(float direction)
    {
        DirectionX = direction;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void MoveTo(Vector2 target)
    {
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void StopMove()
    {
        rb.velocity = Vector2.zero;
    }

    private void SetSpriteFlip(float direction)
    {
        if (direction < 0)
            sr.flipX = false;
        else if (direction > 0)
            sr.flipX = true;
    }

    private void OnDrawGizmos()
    {
        float sphereRadius = 0.2f;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(startPositionPatroling, sphereRadius);
        Gizmos.DrawSphere(endPositionPatroling, sphereRadius);
        Gizmos.DrawLine(startPositionPatroling, endPositionPatroling);
    }
}
