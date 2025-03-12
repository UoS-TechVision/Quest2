// using UnityEngine;

// public class BossSpawner : MonoBehaviour
// {
//     public int gridWidth = 8;
//     public int gridHeight = 5;
//     public float cellSize = 1.5f; // Adjust based on room size

//     public List<GameObject> bossPrefabs;  
//     public string roomTag = "Room";

//     public List<string> roomTypes;

//     void Start(){
//         //Why you reading this its obvious what it does
//         BossSpawner();
//     }


//     void BossSpawner()
//     {
//         // Find all objects in the scene tagged as "Room"
//         GameObject[] rooms = GameObject.FindGameObjectsWithTag(roomTag);

//         for (int i = 0; i < rooms.Length; i++)
//         {
//             // Check if this is the 5th room
//             if ((i + 1) % 2 == 0)
//             {                
//                 GameObject room = rooms[i];
//                 Room roomScript = room.GetComponent<Room>();

//                 // Skip if room missing
//                 if (roomScript == null)
//                 {
//                     Debug.LogWarning("Room script missing on room: " + room.name);
//                     continue;
//                 }

//                 // Get the room type
//                 string type = roomScript.roomType;

//                 // Find the index of this type in the roomTypes list
//                 int index = roomTypes.IndexOf(type);

//                 // If a matching room type was found and a boss prefab exists at that index
//                 if (index != -1 && index < bossPrefabs.Count)
//                 {
//                     switch (roomType[i])
//                     {
//                         case "hellroom":
//                             GameObject bossPrefab = "Basilisk";
//                             break;
//                         case "cultroom":
//                             GameObject bossPrefab = "Leonardo";
//                             break;
//                         // case "Hell":
//                         //     GameObject bossPrefab = "Basilisk";
//                         //     break;
//                         // case "Hell":
//                         //     GameObject bossPrefab = "Basilisk";
//                         //     break;  
//                     }
//                     //Spawn boss
//                     Transform spawnPoint = room.transform.Find("BossSpawnPoint");

//                     if (spawnPoint != null)
//                     {
//                         Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
//                         //Debug.Log($"Spawned {bossPrefab.name} in room {i + 1} (type: {type})");
//                     }
//                     else
//                     {
//                         Debug.LogWarning("Spawn point not found in room: " + room.name);
//                     }
//                 }
//                 else
//                 {
//                     Debug.LogWarning($"No boss prefab for room type: {type}");
//                 }
//             }
//         }

//     }
// }
