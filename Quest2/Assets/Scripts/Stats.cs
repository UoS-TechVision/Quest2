/**
* Player / Mob Combat Stats Helper
*/
using UnityEngine;

[System.Serializable] // Allows the stats to be visible in Unity's inspector
public class Stats
{
    // Strength is how much damage a standard attack does
    public int Strength { get; private set; } = 25;
    
    // Skills expend mana
    public int Mana { get; private set; } = 100;
    
    // Skill Points are used to allocate skills to the player,
    // start with 2 that can be allocated to any player stat
    public int SkillPoints { get; private set; } = 2;
    
    // Starting health
    public int Health { get; set; } = 100;

    // Constructor to initialize stats (useful for different player types or saves)
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

    public string GetStats()
    {
        return "Strength: " + Strength + " // " +
               "Mana: " + SkillPoints + " // " +
               "Health: " + Health + " // " +
               "Skill Points: " + SkillPoints;
    }
}