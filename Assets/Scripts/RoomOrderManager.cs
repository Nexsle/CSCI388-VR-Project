using System.Collections.Generic;
using UnityEngine;

public class RoomOrderManager : MonoBehaviour
{
    public static RoomOrderManager Instance;

    public List<string> roomOrder = new List<string>();

    private int currentRoomIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNormalOrder()
    {
        roomOrder.Clear();

        roomOrder.Add("FindTheButton");
        roomOrder.Add("LV's Scene");
        roomOrder.Add("Room_Simao");
        roomOrder.Add("Victory");

        currentRoomIndex = 0;
    }

    public void SetRandomOrder()
    {
        roomOrder.Clear();

        List<string> randomRooms = new List<string>()
        {
            "FindTheButton",
            "LV's Scene",
            "Room_Simao"
        };

        for (int i = 0; i < randomRooms.Count; i++)
        {
            string temp = randomRooms[i];
            int randomIndex = Random.Range(i, randomRooms.Count);

            randomRooms[i] = randomRooms[randomIndex];
            randomRooms[randomIndex] = temp;
        }

        roomOrder.AddRange(randomRooms);

        roomOrder.Add("Victory");

        currentRoomIndex = 0;
    }

    public string GetNextRoom()
    {
        string nextRoom = roomOrder[currentRoomIndex];
        currentRoomIndex++;

        return nextRoom;
    }
}