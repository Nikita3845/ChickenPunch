using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healths : MonoBehaviour
{
    //Значение здоровья
    public int health;
    //Максимальное значение здоровья
    public int maxHealth;

    public GameObject PanelDeath;


    //Функция получения урона
    public void TakeHit(int damage)
    {
        health -= damage;

        //Если здоровье меньше 0 - уничтожить объект на котором весит этот скрипт
        if (health < 0)
        {
            Destroy(gameObject);
            PanelDeath.SetActive(true);
        }

    }

    //Функция прибавления здоровья
    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;

        //Если здоровье, после пополнения, больше максимального значения - здоровье будет равно максимальному значению.
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }


    public int health_player;
    public int numberOfLives;

    public Image[] lives;

    public Sprite fullLive;
    public Sprite emptyLive;


    private void Update()
    {
        if (health_player > numberOfLives)
        {
            health_player = numberOfLives;
        }


        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }

            if (i < numberOfLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }
}
