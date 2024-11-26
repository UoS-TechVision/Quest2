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
    void Start()
    {
        size = StartRenderer.bounds.size;
        Instantiate(StartRoom, new Vector3(0, 0, 0), new Quaternion(0,180,0,1), this.transform);
        for (int i = 1; i <= nRooms; i++)
            {
                Instantiate(Rooms[UnityEngine.Random.Range(0,4)], new Vector3(i * size.x, 0, 0), Quaternion.identity, this.transform);
            }
        Instantiate(StartRoom, new Vector3((nRooms + 1) * size.x, 0, 0), Quaternion.identity, this.transform);
    }
}
