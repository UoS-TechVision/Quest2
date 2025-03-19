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
        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.currentHP;
        manaSlider.maxValue= unit.maxMana;
        manaSlider.value = unit.currentMana;

        nameText.text = unit.name;
        levelText.text = "Lvl. " + unit.unitLevel;

        
    }
}
