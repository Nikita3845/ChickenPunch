using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false) 
        {
            chill = true;
        }
        if(Vector2.Distance(transform.position, player.position) < stop) 
        {
            angry = true;
            chill = false;
            back = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stop)
        {
            back = true;
            angry = false;
        }


        if(chill == true)
        {
            Chill();
        }
        else if(angry == true)
        {
            Angry();
        }
        else if (back == true)
        {
            Back();
        }
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
}
