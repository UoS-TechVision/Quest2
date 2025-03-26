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

        nameText.text = unit.name;
        levelText.text = "Lvl. " + unit.unitLevel;

        healthText.text = unit.Health.ToString() + "/" + unit.MaxHealth;


    }

    public void SetHP(Unit unit)
    {
        healthSlider.value = unit.Health;
        healthText.text = unit.Health.ToString() + "/" + unit.MaxHealth;
    }

    public void SetMana(Unit unit)
    {
        manaSlider.value = unit.Mana;

        manaText.text = unit.Mana.ToString() + "/" + unit.MaxMana;
    }
}
