using System;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    //Поля - переменный
    //int, float, bool, string, char
    // Модификатор доступа | тип данных | название | ? значение ;
    // private, public
    [SerializeField] private int _hp;
    [SerializeField] private Healths _health;

    public string PlayerName;


    //Методы - функции
    private void Start()
    {
        SayHello("Вася");

        //GameObject.FindWithTag();
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            //enemies[i]
        }

        foreach (Enemy enemy in enemies)
        {
            //enemy
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _health.TakeDamage(1);
        }
    }


    // Модификатор доступа | тип возвращаемых данных | название | (? аргументы)

    private void Move()
    {

    }

    private void SayHello(string name)
    {
        print("Привет " + name);
    }
}
