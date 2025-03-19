/**
* Player / Mob Combat Stats Helper
*/
using UnityEngine;

[System.Serializable]
public class Stats : MonoBehaviour
{
    [SerializeField] private int strength = 25;
    [SerializeField] private int mana = 100;
    [SerializeField] private int skillPoints = 2;
    [SerializeField] private int health = 100;
    
    // Public properties with getters and setters
    public int Strength { get => strength; set => strength = value; }
    public int Mana { get => mana; set => mana = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int Health { get => health; set => health = value; }
    
    public Stats()
    {
    }

    public Stats(int strength, int mana, int skillPoints, int health)
    {
        Strength = strength;
        Mana = mana;
        SkillPoints = skillPoints;
        Health = health;
    }

    // Add points to a stat
    public bool AllocateStat(string statName)
    {
        // not enough skill points
        if (SkillPoints <= 0) return false;

        if (statName == "strength" && this.Strength < 100) 
        {
            Strength += 25;
        }
        else if (statName == "mana" && this.Mana < 300)
        {
            Mana += 50;
        }
        else if (statName == "health" && this.Health < 300)
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

    public string toString()
    {
        return "Strength: " + Strength + " // " +
               "Mana: " + SkillPoints + " // " +
               "Health: " + Health + " // " +
               "Skill Points: " + SkillPoints;
    }
}