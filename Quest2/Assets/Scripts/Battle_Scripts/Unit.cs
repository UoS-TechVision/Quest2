using UnityEngine;

public class Unit : MonoBehaviour
{
    // Default values
    [SerializeField] public string unitName = "default_name";
    [SerializeField] public int unitLevel = 1;
    [SerializeField] private int strength = 25;
    [SerializeField] private int mana = 100;
    [SerializeField] private int skillPoints = 1;
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 300;
    [SerializeField] private int maxMana = 300;
    [SerializeField] private int maxStrength = 100;
    // [SerializeField] private int skillDamage = 50;
    // [SerializeField] private int skillCost = 75;
    // [SerializeField] private string skillName = "";
    [SerializeField] private Skill skill;
    
    // Public properties with getters and setters
    // TODO: validation for set methods below

    public int Strength
    {
        get => strength;
        set
        {
            if (value > MaxStrength) strength = MaxStrength;
            else if (value < 0) strength = 0;
            else strength = value;
        }
    }

    public int Mana
    {
        get => mana;
        set
        {
            if (value > maxMana) mana = MaxMana;
            else if (value < 0) mana = 0;
            else mana = value;
        }
    }

    public int SkillPoints { get => skillPoints; set => skillPoints = value < 0 ? 0 : value; }
    public int Health 
    { 
        get => health;
        set
        {
            if (value > maxHealth) health = MaxHealth;
            else if (value < 0) health = 0;
            else health = value;
        } 
    }
    public int MaxHealth { get => maxHealth; }
    public int MaxMana { get => maxMana; }
    public int MaxStrength { get => maxStrength; }

    public int SkillCost { get => skill.skillCost; }

    public int SkillDamage { get=> skill.skillDamage; }

    public string SkillName { get => skill.projectile.ToString(); }
    
    // Add points to a stat
    public bool AllocateStat(string statName)
    {
        // not enough skill points
        if (SkillPoints <= 0) return false;

        if (statName == "strength") 
        {
            Strength += 25;
        }
        else if (statName == "mana")
        {
            Mana += 50;
        }
        else if (statName == "health")
        {
            Health += 50;
        }
        else
        {
            return false;
        }

        SkillPoints--;
        return true;
    }
    
    // Call this function after defeating enemy/boss
    public void AddSkillPoints(int points)
    {
        SkillPoints += points;
    }

    public override string ToString()
    {
        return "Strength: " + Strength + " // " +
               "Mana: " + SkillPoints + " // " +
               "Health: " + Health + " // " +
               "Skill Points: " + SkillPoints;
    }


    public bool TakeDamage(int damage)
    {
        health -= damage;

        return health <= 0;
    }

    public void DeductMana()
    {
        mana -= skill.skillCost;
    }

    public void IncrementMana()
    {
        mana += 10;
    }
    
}


  
