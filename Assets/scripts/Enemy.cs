using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _health;

    void Start()
    {
        
    }

    public void TakeDamange(float value)
    {
        _health -= value;

        if (_health <= 0)
            Destroy(gameObject);
    }
}
