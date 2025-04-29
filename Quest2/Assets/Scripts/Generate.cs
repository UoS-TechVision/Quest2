using System;
using UnityEngine;

public class Generate : MonoBehaviour
{
    [SerializeField]
    int nRooms;
    [SerializeField]
    GameObject StartRoom;
    [SerializeField]
    GameObject[] Rooms;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Vector3 size;
    [SerializeField]
    float cohesion;
    public float cellSize = 1.6f;
    public int gridWidth = 8;
    public int gridHeight = 5;
    void Start()
    {
        Instantiate(StartRoom, new Vector3(0, 0, 0), new Quaternion(0,180,0,1), this.transform);

        for (int i = 1; i <= nRooms; i++)
            {   
                // spawns player
                if (i == 1) {
                    Vector3 roomPosition = transform.position; // Room center
                    float totalWidth = (gridWidth - 1) * cellSize; // Total grid width
                    float totalHeight = (gridHeight - 1) * cellSize; // Total grid height

                    Vector3 bottomLeft = roomPosition - new Vector3(totalWidth / 2, 0, totalHeight / 2 - 3);
                    Instantiate(Player, bottomLeft, Quaternion.identity);
                }

                GameObject currentRoom = Instantiate(Rooms[UnityEngine.Random.Range(0, Rooms.Length)], new Vector3(i * 12.8f, 0, 0), Quaternion.identity, this.transform);
                BossSpawner bossSpawner = currentRoom.GetComponent<BossSpawner>();
                
                if (i % 5 != 0) {
                    // Deactivate BossSpawner script on non-5th rooms
                    bossSpawner.enabled = false;
                } else {
                    // Enable BossSpawner script on 5th rooms
                    bossSpawner.enabled = true;
                }
            }            
    }
}
