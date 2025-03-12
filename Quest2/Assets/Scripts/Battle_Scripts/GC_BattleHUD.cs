using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GC_BattleHUD : MonoBehaviour
{
    public Slider healthSlider;
    public Slider manaSlider;
    public TextMeshPro healthText;
    public TextMeshPro manaText;

    public TextMeshPro nameText;
    public TextMeshPro levelText;


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
