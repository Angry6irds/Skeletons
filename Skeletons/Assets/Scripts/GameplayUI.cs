using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Alejandro.Gameplay;
using ScriptableObjet;

namespace Alejandro
{
    public class GameplayUI : UIWindow
    {
        [Header("Gameplay References")]
        [SerializeField] private Slider playerTowerHealthBar;
        [SerializeField] private Slider enemyTowerHealthBar;
        [SerializeField] private Button pauseButton;

        [Header("Inventory / Spawn References")]
        [SerializeField] private Transform inventoryContentContainer;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private TextMeshProUGUI selectedNameText;
        [SerializeField] private TextMeshProUGUI selectedPowerText;
        [SerializeField] private Image selectedIcon;
        [SerializeField] private Button spawnButton;
        
        [Header("Clicker Settings")]
        [SerializeField] private float baseSpawnCooldown = 3f;
        private float currentCooldownTimer;
        private int enemiesKilled;
        private ItemData currentSelectedItem;

        public override void Initialize()
        {
            base.Initialize();
            if (pauseButton != null) pauseButton.onClick.AddListener(OnPauseClicked);
            if (spawnButton != null) spawnButton.onClick.AddListener(OnSpawnClicked);
        }

        public override void Show()
        {
            base.Show();
            
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.playerTower != null)
                {
                    GameManager.Instance.playerTower.OnHealthChanged += UpdatePlayerTowerHealth;
                    UpdatePlayerTowerHealth(GameManager.Instance.playerTower.CurrentHealth, GameManager.Instance.playerTower.MaxHealth);
                }
                
                if (GameManager.Instance.enemyTower != null)
                {
                    GameManager.Instance.enemyTower.OnHealthChanged += UpdateEnemyTowerHealth;
                    UpdateEnemyTowerHealth(GameManager.Instance.enemyTower.CurrentHealth, GameManager.Instance.enemyTower.MaxHealth);
                }
            }
            
            RefreshInventory();
            currentCooldownTimer = 0f;
            enemiesKilled = 0;
        }

        public override void Hide()
        {
            base.Hide();
            
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.playerTower != null)
                {
                    GameManager.Instance.playerTower.OnHealthChanged -= UpdatePlayerTowerHealth;
                }
                
                if (GameManager.Instance.enemyTower != null)
                {
                    GameManager.Instance.enemyTower.OnHealthChanged -= UpdateEnemyTowerHealth;
                }
            }
        }

        private void Update()
        {
            if (currentCooldownTimer > 0)
            {
                currentCooldownTimer -= Time.deltaTime;
                if (spawnButton != null) spawnButton.interactable = false;
            }
            else
            {
                if (spawnButton != null) spawnButton.interactable = currentSelectedItem != null;
            }
        }

        private void RefreshInventory()
        {
            if (inventoryContentContainer == null || itemPrefab == null) return;

            foreach (Transform child in inventoryContentContainer)
            {
                Destroy(child.gameObject);
            }

            if (InventoryManager.Instance == null) return;

            foreach (var runtimeItem in InventoryManager.Instance.items)
            {
                ItemData data = InventoryManager.Instance.GetItemData(runtimeItem.itemType);
                if (data != null)
                {
                    GameObject newIcon = Instantiate(itemPrefab, inventoryContentContainer);
                    ItemUI itemUI = newIcon.GetComponent<ItemUI>();
                    if (itemUI != null)
                    {
                        itemUI.SetData(data);
                        itemUI.OnItemClicked += HandleItemSelected;
                    }
                }
            }
        }

        private void HandleItemSelected(ItemData selectedData)
        {
            currentSelectedItem = selectedData;
            
            if (selectedNameText != null) selectedNameText.text = selectedData.ItemName;
            if (selectedPowerText != null) selectedPowerText.text = selectedData.StatsDescription;
            if (selectedIcon != null) selectedIcon.sprite = selectedData.Icon;
        }

        private void OnSpawnClicked()
        {
            if (currentCooldownTimer > 0 || currentSelectedItem == null) return;

            float speedReduction = enemiesKilled * 0.1f;
            float actualCooldown = Mathf.Max(0.5f, baseSpawnCooldown - speedReduction);
            
            currentCooldownTimer = actualCooldown;
        }

        public void RegisterEnemyKilled()
        {
            enemiesKilled++;
        }

        private void UpdatePlayerTowerHealth(float currentHealth, float maxHealth)
        {
            if (playerTowerHealthBar != null)
            {
                playerTowerHealthBar.maxValue = maxHealth;
                playerTowerHealthBar.value = currentHealth;
            }
        }

        private void UpdateEnemyTowerHealth(float currentHealth, float maxHealth)
        {
            if (enemyTowerHealthBar != null)
            {
                enemyTowerHealthBar.maxValue = maxHealth;
                enemyTowerHealthBar.value = currentHealth;
            }
        }

        private void OnPauseClicked()
        {
            UiManager.Instance.ShowWindow(WindowsId.PauseUI);
        }

        private void OnDestroy()
        {
            if (pauseButton != null) pauseButton.onClick.RemoveListener(OnPauseClicked);
            if (spawnButton != null) spawnButton.onClick.RemoveListener(OnSpawnClicked);
        }
    }
}