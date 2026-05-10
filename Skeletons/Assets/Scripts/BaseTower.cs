using UnityEngine;

namespace Alejandro.Gameplay
{
    public class BaseTower : Entity
    {
        public enum TowerType { Player, Enemy }
        
        [Header("Tower Settings")]
        public TowerType towerType;

        protected override void Die()
        {
            base.Die();
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnTowerDestroyed(this);
            }
        }
    }
}
