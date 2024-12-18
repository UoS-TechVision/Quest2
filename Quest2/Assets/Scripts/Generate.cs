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
    MeshRenderer StartRenderer;
    Vector3 size;
    [SerializeField]
    float cohesion;
    void Start()
    {
        size = StartRenderer.bounds.size;
        Debug.Log(size.x);
        Instantiate(StartRoom, new Vector3(0, 0, 0), new Quaternion(0,180,0,1), this.transform);

        GameObject previousRoom = Rooms[UnityEngine.Random.Range(0, Rooms.Length)];
        for (int i = 1; i <= nRooms; i++)
            {
                if (UnityEngine.Random.Range(0f, 1f) < cohesion)
                {
                    Instantiate(previousRoom, new Vector3(i * 12.8f, 0, 0), Quaternion.identity, this.transform);
                }
                else
                {
                    previousRoom = Instantiate(Rooms[UnityEngine.Random.Range(0, Rooms.Length)], new Vector3(i * 12.8f, 0, 0), Quaternion.identity, this.transform);
                }
            }
            
        Instantiate(StartRoom, new Vector3((nRooms + 1) * 12.8f, 0, 0), Quaternion.identity, this.transform);
    }
}
