using UnityEngine;
using System.Collections.Generic;
using ScriptableObjet;

namespace Alejandro.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Towers")]
        public BaseTower playerTower;
        public BaseTower enemyTower;

        [Header("Spawning Settings")]
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float enemySpawnInterval = 5f;
        
        [HideInInspector] public List<Vector3> waypoints = new List<Vector3>();
        public List<Unit> allUnits = new List<Unit>();

        private float enemySpawnTimer;
        private bool isGameActive;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            if (DungeonGenerator.Instance != null)
            {
                DungeonGenerator.Instance.OnDungeonComplete += OnDungeonGenerated;
            }
        }

        public void StartGame()
        {
            if (DungeonGenerator.Instance != null)
            {
                DungeonGenerator.Instance.DungeonStartTrigger();
            }
        }

        private void OnDungeonGenerated()
        {
            var fragments = DungeonGenerator.Instance.dungeonFragments;
            int count = fragments.Count;
            if (count == 0) return;

            waypoints.Clear();

            float f0Width = fragments[0].chunkWidth * fragments[0].chunkSize;
            float f0Height = fragments[0].chunkHeight * fragments[0].chunkSize;
            Vector3 startPos = fragments[0].position + new Vector3(f0Width / 2f, 0f, f0Height / 2f);
            
            float fnWidth = fragments[count - 1].chunkWidth * fragments[count - 1].chunkSize;
            float fnHeight = fragments[count - 1].chunkHeight * fragments[count - 1].chunkSize;
            Vector3 endPos = fragments[count - 1].position + new Vector3(fnWidth / 2f, 0f, fnHeight / 2f);

            if (playerTower != null)
            {
                playerTower.transform.position = startPos;
                playerTower.ResetHealth();
            }
            if (enemyTower != null)
            {
                enemyTower.transform.position = endPos;
                enemyTower.ResetHealth();
            }

            waypoints.Add(startPos);

            if (fragments[0].acInfo.hasExit)
            {
                Vector3 exitLocal = new Vector3(fragments[0].acInfo.exitStart.x, 0f, fragments[0].acInfo.exitStart.y);
                waypoints.Add(fragments[0].position + exitLocal);
            }

            for (int i = 1; i < count - 1; i++)
            {
                var frag = fragments[i];
                if (frag.acInfo.hasEntrance)
                {
                    Vector3 entranceLocal = new Vector3(frag.acInfo.entranceEnd.x, 0f, frag.acInfo.entranceEnd.y);
                    waypoints.Add(frag.position + entranceLocal);
                }
                
                float fWidth = frag.chunkWidth * frag.chunkSize;
                float fHeight = frag.chunkHeight * frag.chunkSize;
                waypoints.Add(frag.position + new Vector3(fWidth / 2f, 0f, fHeight / 2f));

                if (frag.acInfo.hasExit)
                {
                    Vector3 exitLocal = new Vector3(frag.acInfo.exitStart.x, 0f, frag.acInfo.exitStart.y);
                    waypoints.Add(frag.position + exitLocal);
                }
            }

            if (fragments[count - 1].acInfo.hasEntrance)
            {
                Vector3 entranceLocal = new Vector3(fragments[count - 1].acInfo.entranceEnd.x, 0f, fragments[count - 1].acInfo.entranceEnd.y);
                waypoints.Add(fragments[count - 1].position + entranceLocal);
            }

            waypoints.Add(endPos);

            isGameActive = true;
            enemySpawnTimer = enemySpawnInterval;
        }

        private void Update()
        {
            if (!isGameActive) return;

            enemySpawnTimer -= Time.deltaTime;
            if (enemySpawnTimer <= 0f)
            {
                enemySpawnTimer = enemySpawnInterval;
                SpawnEnemy();
            }
        }

        public void SpawnAlly(ItemData unitData)
        {
            if (!isGameActive || waypoints.Count == 0 || unitData == null || unitData.UnitPrefab == null) return;

            Vector3 spawnPos = waypoints[0];
            GameObject newAlly = Instantiate(unitData.UnitPrefab, spawnPos, Quaternion.identity);
            Unit unit = newAlly.GetComponent<Unit>();
            if (unit != null)
            {
                RegisterUnit(unit);
            }
        }

        private void SpawnEnemy()
        {
            if (!isGameActive || waypoints.Count == 0 || enemyPrefab == null) return;
            
            Vector3 spawnPos = waypoints[waypoints.Count - 1];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Unit unit = newEnemy.GetComponent<Unit>();
            if (unit != null)
            {
                RegisterUnit(unit);
            }
        }

        public void RegisterUnit(Unit unit)
        {
            if (!allUnits.Contains(unit)) allUnits.Add(unit);
        }

        public void UnregisterUnit(Unit unit)
        {
            if (allUnits.Contains(unit)) allUnits.Remove(unit);
        }

        public void OnTowerDestroyed(BaseTower destroyedTower)
        {
            isGameActive = false;
            
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

        private void OnDestroy()
        {
            if (DungeonGenerator.Instance != null)
            {
                DungeonGenerator.Instance.OnDungeonComplete -= OnDungeonGenerated;
            }
        }
    }
}
