using UnityEngine;

public class GC_LoadEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterPrefabs; // Array to hold monster prefabs

    private GameObject specifiedMonster;

    void Start()
    {
        // Find the persisted monster
        specifiedMonster = FindMonsterByLayer("Enemy");

        if (specifiedMonster != null) {
            Debug.Log("Persisted Monster found: " + specifiedMonster.name);
            LoadCorrectMonster();
        }
        else {
            Debug.LogWarning("No persisted monster found in the scene.");
        }
    }

    private GameObject FindMonsterByLayer(string layerName)
    {
        // Get the layer index by layer name
        int layerIndex = LayerMask.NameToLayer(layerName);

        // Find all colliders on the specified layer
        Collider[] colliders = Physics.OverlapSphere(Vector3.zero, Mathf.Infinity, 1 << layerIndex);

        // Loop through the colliders and return the first one
        if (colliders.Length > 0)
        {
            return colliders[0].gameObject; // Return the first GameObject found
        }

        return null; // Return null if no GameObject found
    }

    private void LoadCorrectMonster()
    {
        // You can use the name, tag, or any other identifier to match the monster
        string monsterName = specifiedMonster.name + "Enemy";

        // Loop through the prefabs to find the matching monster
        foreach (var prefab in monsterPrefabs) {
            if (prefab.name == monsterName) {
                // Instantiate the matching prefab at the position of this GameObject (the empty GameObject)
                Instantiate(prefab, transform.position, Quaternion.identity);
                Debug.Log($"Loaded correct monster prefab: {prefab.name}");
                break;
            }
        }
    }
}
