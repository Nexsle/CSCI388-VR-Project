using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomButton : MonoBehaviour
{
    public void StartRandomGame()
    {
        RoomOrderManager.Instance.SetRandomOrder();

        SceneManager.LoadScene(
            RoomOrderManager.Instance.GetNextRoom()
        );
    }
}