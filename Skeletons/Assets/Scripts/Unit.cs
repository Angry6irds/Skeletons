using UnityEngine;

namespace Alejandro.Gameplay
{
    public class Unit : Entity
    {
        [Header("Unit Settings")]
        [SerializeField] protected Team team;
        [SerializeField] protected float moveSpeed = 2f;
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float attackRange = 1.5f;
        [SerializeField] protected float attackCooldown = 1f;

        protected float lastAttackTime;
        protected Entity currentTarget;
        
        protected Animator animator;

        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();
        }

        protected virtual void Update()
        {
            if (IsDead) return;

            FindTarget();

            if (currentTarget != null)
            {
                float distance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (distance <= attackRange)
                {
                    Attack();
                }
                else
                {
                    MoveTowardsTarget();
                }
            }
            else
            {
                MoveForward(); 
            }
        }

        protected virtual void FindTarget()
        {
            
        }

        protected virtual void MoveTowardsTarget()
        {
            
        }

        protected virtual void MoveForward()
        {
            
        }

        protected virtual void Attack()
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                
                if(animator != null) animator.SetTrigger("Attack");
                
                currentTarget.TakeDamage(damage);
            }
        }

        protected override void Die()
        {
            base.Die();
            if(animator != null) animator.SetTrigger("Die");
            
            Destroy(gameObject, 2f); 
        }
    }
}
