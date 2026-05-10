using UnityEngine;

namespace Alejandro.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Towers")]
        public BaseTower playerTower;
        public BaseTower enemyTower;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void StartGame()
        {
            
        }

        public void OnTowerDestroyed(BaseTower destroyedTower)
        {
            if (destroyedTower.towerType == BaseTower.TowerType.Player)
            {
                GameOver(false);
            }
            else
            {
                GameOver(true);
            }
        }

        private void GameOver(bool playerWon)
        {
            if (playerWon)
            {
                
            }
            else
            {
                
            }
        }
    }
}
