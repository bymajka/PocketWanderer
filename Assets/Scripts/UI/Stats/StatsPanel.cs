using System.Globalization;
using Entity.Behaviour;
using StatsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Stats
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] private Slider healthPoolSlider;
        [SerializeField] private Slider manaPoolSlider;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        
        private void Start()
        {
            PlayerStats.OnHealthPointsChanged += HandleHealthPointsChanged;
            PlayerStats.OnManaPointsChanged += HandleManaPointsChanged;
            PlayerStats.OnGoldAmountChanged += HandleGoldAmountChanged;
        }
        
        private void OnDestroy()
        {
            PlayerStats.OnHealthPointsChanged -= HandleHealthPointsChanged;
            PlayerStats.OnManaPointsChanged -= HandleManaPointsChanged;
            PlayerStats.OnGoldAmountChanged -= HandleGoldAmountChanged;
        }
        
        private void HandleGoldAmountChanged(int goldValue)
        {
            UpdateText(goldValue.ToString(CultureInfo.InvariantCulture));
        }

        private void HandleManaPointsChanged(float mana)
        {
            var maxMana = PlayerManager
                .Instance
                .PlayerObject
                .GetComponent<PlayerEntityBehaviour>()
                .Stats
                .MaxManaPool;
            
            var normalizedMana = CalculateNormalizedValue(mana, maxMana);
            manaPoolSlider.value = normalizedMana;
        }
        
        private void HandleHealthPointsChanged(float health)
        {
            var maxHitPoints = PlayerManager
                .Instance
                .PlayerObject
                .GetComponent<PlayerEntityBehaviour>()
                .Stats
                .MaxHitPoints;
            
            var normalizedHealth = CalculateNormalizedValue(health, maxHitPoints);
            healthPoolSlider.value = normalizedHealth;
        }
        
        private void UpdateText(string value)
        {
            textMeshProUGUI.text = value;
        }

        private static float CalculateNormalizedValue(float value, float maxValue)
        {
            return value / maxValue;
        }
    }
}
