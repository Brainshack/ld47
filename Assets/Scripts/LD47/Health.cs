using System;
using UnityEngine;
using UnityEngine.Events;

namespace LD47
{
    public class Health : MonoBehaviour
    {
        public int maxHealth = 100;

        public int CurrentHealth { get => _currentHealth; }

        public UnityEvent OnDeath;
        public UnityEvent<int, Vector3> OnDamageTaken;
        
        private int _currentHealth;
        
        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, Transform damager)
        {
            OnDamageTaken.Invoke(damage, damager.position);
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                OnDeath.Invoke();
            }
        }
    }
}