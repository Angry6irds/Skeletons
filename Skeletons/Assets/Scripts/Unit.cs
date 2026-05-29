using UnityEngine;
using System.Collections.Generic;
using Alejandro;

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
        [SerializeField] protected float detectionRange = 5f;

        protected float lastAttackTime;
        protected Entity currentTarget;
        
        protected Animator animator;
        protected int currentWaypointIndex;

        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();
            InitializeWaypoints();
        }

        private void InitializeWaypoints()
        {
            if (GameManager.Instance != null && GameManager.Instance.waypoints.Count > 0)
            {
                if (team == Team.Player)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    currentWaypointIndex = GameManager.Instance.waypoints.Count - 1;
                }
            }
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
            if (currentTarget != null && !currentTarget.IsDead)
            {
                float dist = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (dist <= detectionRange) return;
            }

            currentTarget = null;
            float closestDist = detectionRange;

            if (GameManager.Instance != null)
            {
                foreach (Unit unit in GameManager.Instance.allUnits)
                {
                    if (unit.IsDead || unit.team == team) continue;
                    
                    float dist = Vector3.Distance(transform.position, unit.transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        currentTarget = unit;
                    }
                }

                if (currentTarget == null)
                {
                    BaseTower enemyTower = team == Team.Player ? GameManager.Instance.enemyTower : GameManager.Instance.playerTower;
                    if (enemyTower != null && !enemyTower.IsDead)
                    {
                        float dist = Vector3.Distance(transform.position, enemyTower.transform.position);
                        if (dist < closestDist)
                        {
                            currentTarget = enemyTower;
                        }
                    }
                }
            }
        }

        protected virtual void MoveTowardsTarget()
        {
            if (currentTarget == null) return;
            
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            direction.y = 0; 
            
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
                transform.position += direction * moveSpeed * Time.deltaTime;
                if (animator != null) animator.SetBool("IsWalking", true);
            }
        }

        protected virtual void MoveForward()
        {
            if (GameManager.Instance == null || GameManager.Instance.waypoints.Count == 0) return;

            Vector3 targetWaypoint = GameManager.Instance.waypoints[currentWaypointIndex];
            Vector3 direction = (targetWaypoint - transform.position);
            direction.y = 0;

            if (direction.magnitude < 0.2f)
            {
                if (team == Team.Player && currentWaypointIndex < GameManager.Instance.waypoints.Count - 1)
                {
                    currentWaypointIndex++;
                }
                else if (team == Team.Enemy && currentWaypointIndex > 0)
                {
                    currentWaypointIndex--;
                }
                return;
            }

            direction.Normalize();
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
            
            if (animator != null) animator.SetBool("IsWalking", true);
        }

        protected virtual void Attack()
        {
            if (animator != null) animator.SetBool("IsWalking", false);
            
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                
                if (animator != null) animator.SetTrigger("Attack");
                
                currentTarget.TakeDamage(damage);
            }
        }

        protected override void Die()
        {
            base.Die();
            if (animator != null) animator.SetTrigger("Die");
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.UnregisterUnit(this);
                if (team == Team.Enemy)
                {
                    GameplayUI gameplayUI = UiManager.Instance.GetWindow(WindowsId.GameplayUI) as GameplayUI;
                    if (gameplayUI != null) gameplayUI.RegisterEnemyKilled();
                }
            }

            Destroy(gameObject, 2f); 
        }
    }
}
