/*
* Skill Base Class
*/

using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum ProjectileType
    {
        Fire,
        Earth,
        Water,
        Wind
    }

    [SerializeField] public ProjectileType projectile;
    [SerializeField] public string skillName;
    [SerializeField] public int skillDamage;
    [SerializeField] public int skillCost;
    [SerializeField] public float critChance;
    [SerializeField] public float critDamage;
    
    private void Awake()
    {

/*        Debug.Log("AWAKE");
        InitializeSkill();*/
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


    /*    private void InitializeSkill()
        {
            switch (projectile)
            {
                case ProjectileType.Earth:
                    skillDamage = 50;
                    skillCost = 30;
                    critChance = 0.6f;
                    critDamage = 3.0f;
                    skillName = "Rock Throw";
                    break;
                case ProjectileType.Fire:
                    skillDamage = 20;
                    skillCost = 20;
                    critChance = 0.5f;
                    critDamage = 2.0f;
                    skillName = "Fireball";
                    break;
                case ProjectileType.Water:
                    skillDamage = 15;
                    skillCost = 10;
                    critChance = 0.5f;
                    critDamage = 2.0f;
                    skillName = "Water Bullet";
                    break;
                case ProjectileType.Wind:
                    skillDamage = 10;
                    skillCost = 5;
                    critChance = 0.5f;
                    critDamage = 2.0f;
                    skillName = "Wind Burst";
                    break;
                default:
                    Debug.LogWarning("Unknown skill type!");
                    break;
            }
        }*/
}
