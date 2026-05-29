using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;
 
public class RoomOrderManager : MonoBehaviour
{
    public static RoomOrderManager Instance;
    public List<string> roomOrder = new List<string>();
    private int currentRoomIndex = 0;
    private bool isFirstScene = true;
 
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
            return;
        }
 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isFirstScene)
        {
            isFirstScene = false;
            return;
        }
 
        StartCoroutine(RepositionXROrigin());
    }
 
    private IEnumerator RepositionXROrigin()
    {
        yield return null;
 
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPoint");
        XROrigin xrOrigin = FindFirstObjectByType<XROrigin>();
 
        Vector3 cameraWorldPos = xrOrigin.Camera.transform.position;
        Vector3 originPos = xrOrigin.transform.position;
 
        Vector3 cameraFloorPos = new Vector3(cameraWorldPos.x, originPos.y, cameraWorldPos.z);
        Vector3 delta = originPos - cameraFloorPos;
 
        xrOrigin.transform.position = spawnPoint.transform.position + delta;
        xrOrigin.transform.rotation = spawnPoint.transform.rotation;
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
