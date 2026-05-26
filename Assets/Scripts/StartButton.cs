using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void StartGame()
    {
        RoomOrderManager.Instance.SetNormalOrder();

        SceneManager.LoadScene(
            RoomOrderManager.Instance.GetNextRoom()
        );
    }
}