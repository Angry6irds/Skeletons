using System;
using UnityEngine;

namespace Alejandro.Gameplay
{
    public abstract class Entity : MonoBehaviour
    {
        [Header("Entity Stats")]
        [SerializeField] protected float maxHealth = 100f;
        protected float currentHealth;

        public bool IsDead => currentHealth <= 0;
        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;
        
        public Action<float, float> OnHealthChanged;
        public Action<Entity> OnDeath;

        protected virtual void Start()
        {
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public virtual void TakeDamage(float damage)
        {
            if (IsDead) return;

            currentHealth -= damage;
            currentHealth = Mathf.Max(0, currentHealth);
            
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            OnDeath?.Invoke(this);
        }
    }
}
