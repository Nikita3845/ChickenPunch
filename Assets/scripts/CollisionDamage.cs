using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public int collisionDamage = 10;
    public string collisionTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Если тег объекта коллайдер которого столкнулся с коллайдером нашего объекта - Player
        if (coll.gameObject.tag == collisionTag)
        {
            //Берём у этого объекта компонент Health (Скрипт который на нём висит)
            Healths heal = coll.gameObject.GetComponent<Healths>();
            //И вызываем функцию получения урона, в агрументе переменная урона
            heal.TakeHit(collisionDamage);
        }
    }
}
