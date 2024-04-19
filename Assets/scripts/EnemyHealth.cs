using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _deathEffect;

    public void TakeDamage(int value)
    {
        _health -= value;

        if (_health <= 0)
        {
            Instantiate(_deathEffect.gameObject, transform.position + Vector3.up * (transform.localScale.y / 2), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
