using System.Globalization;
using Entity.Behaviour;
using StatsSystem;
using TMPro;
using UnityEngine;

namespace UI.Stats
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Slider healthPoolSlider;
        [SerializeField] private UnityEngine.UI.Slider manaPoolSlider;
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
        private void HandleGoldAmountChanged(int goldValue) => UpdateText(goldValue.ToString(CultureInfo.CurrentCulture));
        private void HandleManaPointsChanged(float mana)
        {
            float normalizedMana = CalculateNormalizedValue(mana, 
                PlayerManager.Instance.PlayerObject.GetComponent<PlayerEntityBehaviour>().Stats.maxManaPool);
            manaPoolSlider.value = normalizedMana;
        }
        private void HandleHealthPointsChanged(float health)
        {
            float normalizedHealth = CalculateNormalizedValue(health,
                PlayerManager.Instance.PlayerObject.GetComponent<PlayerEntityBehaviour>().Stats.MaxHitPoints);
            healthPoolSlider.value = normalizedHealth;
        }
        private void UpdateText(string value) => textMeshProUGUI.text = value;
        private float CalculateNormalizedValue(float value, float maxValue) => value / maxValue;

    }
}
