using System;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour
{
    //���� - ����������
    //int, float, bool, string, char
    // ����������� ������� | ��� ������ | �������� | ? �������� ;
    // private, public
    [SerializeField] private int _hp;
    [SerializeField] private Healths _health;

    public string PlayerName;


    //������ - �������
    private void Start()
    {
        SayHello("����");

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


    // ����������� ������� | ��� ������������ ������ | �������� | (? ���������)

    private void Move()
    {

    }

    private void SayHello(string name)
    {
        print("������ " + name);
    }
}
