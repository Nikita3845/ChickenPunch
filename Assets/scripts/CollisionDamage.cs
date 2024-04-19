using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int _collisionDamage = 10;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Если тег объекта коллайдер которого столкнулся с коллайдером нашего объекта - Player
        if (coll.gameObject.tag == "Player" && coll.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_collisionDamage);
        }
    }
}
