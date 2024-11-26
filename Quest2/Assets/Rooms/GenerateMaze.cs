using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    [SerializeField]
    int length; //number of rows/columns
    [SerializeField]
    GameObject room;
    [SerializeField]
    GameObject door;
    [SerializeField]
    MeshRenderer roomRenderer;
    Vector3 size;
    void Start()
    {
        //place rooms
        GameObject[,] grid = new GameObject[length,length];
        size = roomRenderer.bounds.size;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                grid[i,j] = Instantiate(room, new Vector3(i*size.x,0,j*size.z), Quaternion.identity, this.transform);
                GameObject currentRoom = grid[i,j];
                for (int k = 0; k < 4; k++)
                {
                    Instantiate(door, currentRoom.transform.position, Quaternion.Euler(0, k*90, 0), currentRoom.transform).name = k.ToString();

                }
            }
        }


    }
}
