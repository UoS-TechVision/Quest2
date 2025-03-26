using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GC_BattleHUD : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    public TMP_Text healthText;
    public TMP_Text manaText;

    public TMP_Text nameText;
    public TMP_Text levelText;


    public void SetHUD(Unit unit)
    {
        healthSlider.maxValue = unit.MaxHealth;
        healthSlider.value = unit.Health;
        manaSlider.maxValue= unit.MaxMana;
        manaSlider.value = unit.Mana;

        nameText.text = unit.UnitName;
        levelText.text = "Lvl. " + unit.UnitLevel;
    }
}
