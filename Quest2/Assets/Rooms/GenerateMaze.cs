using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class GenerateMaze : MonoBehaviour
{
    [SerializeField]
    int length; //number of rows/columns
    [SerializeField]
    GameObject room; //room template (all doors open)
    [SerializeField]
    GameObject door; //door to be placed on rooms
    [SerializeField]
    MeshRenderer roomRenderer;
    Vector3 size;
    bool[,] visited;
    [SerializeField]
    Material[] mats;
    GameObject[,] grid;
    void Start()
    {
        //place rooms/all doors
        grid = new GameObject[length,length];
        size = roomRenderer.bounds.size;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                grid[i,j] = Instantiate(room, new Vector3(i*size.x,0,j*size.z), Quaternion.identity, this.transform); //add room to grid
                GameObject currentRoom = grid[i,j];
                Material mat = mats[UnityEngine.Random.Range(0,mats.Length)];
                currentRoom.GetComponent<MeshRenderer>().material = mat;
                for (int k = 0; k < 4; k++)
                {
                    Instantiate(door, currentRoom.transform.position, Quaternion.Euler(0, k*90, 0), currentRoom.transform).name = k.ToString(); //place doors in room
                    currentRoom.transform.Find(k.ToString()).gameObject.GetComponent<MeshRenderer>().material = mat;
                }
            }
        }

        //generate maze by removing doors
        visited = new bool[length,length];
        Generate(UnityEngine.Random.Range(0, length), UnityEngine.Random.Range(0, length));

    }

    void Generate(int i, int j)
    {
        visited[i,j] = true;
        bool hasNeighbours = true;
        while (hasNeighbours)
        {
            //get neighbour list
            List<Tuple<int,int>> neighbours = GetNeighbours(i,j);
            if (neighbours.Count > 0)
            {
                Tuple<int,int> neighbour = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
                RemoveDoor(i, j, neighbour.Item1, neighbour.Item2);
                Generate(neighbour.Item1, neighbour.Item2);
                //pick random unvisited rooms
                //remove door
                //generate(room)
            }
            else
            {
                hasNeighbours = false;
            }
        }
    }

    List<Tuple<int,int>> GetNeighbours(int i, int j)
    {
        List<Tuple<int,int>> neighbours = new();
        
        if (0 <= i-1)
        {
            if (!visited[i-1,j])
            {
                neighbours.Add(new Tuple<int,int>(i-1,j));
            }
        }
        if (i+1 < length)
        {
            if (!visited[i+1,j])
            {
                neighbours.Add(new Tuple<int,int>(i+1,j));
            }
        }
        if (0 <= j-1)
        {
            if (!visited[i,j-1])
            {
                neighbours.Add(new Tuple<int,int>(i,j-1));
            }
        }
        if (j+1 < length)
        {
            if (!visited[i,j+1])
            {
                neighbours.Add(new Tuple<int,int>(i,j+1));
            }
        }

        return neighbours;
    }

    void RemoveDoor(int i, int j, int a, int b)
    {
        if (i>a)
        {
            Destroy(grid[i,j].transform.Find("1").gameObject);
            Destroy(grid[a,b].transform.Find("3").gameObject);
        }
        else if (i<a)
        {
            Destroy(grid[i,j].transform.Find("3").gameObject);
            Destroy(grid[a,b].transform.Find("1").gameObject);
        }
        else if (j>b)
        {
            Destroy(grid[i,j].transform.Find("0").gameObject);
            Destroy(grid[a,b].transform.Find("2").gameObject);
        }
        else if (j<b)
        {
            Destroy(grid[i,j].transform.Find("2").gameObject);
            Destroy(grid[a,b].transform.Find("0").gameObject);
        }
    }
}
