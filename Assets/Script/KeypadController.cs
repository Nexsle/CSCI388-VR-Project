using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class KeypadController : MonoBehaviour
{
    public static KeypadController Instance;

    [SerializeField] private DoorInteractable door;

    [Header("Settings")]
    [SerializeField] private string correctCode;
    [SerializeField] private float displayResultTime;
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Renderer screenPanel;

    [Header("Color")]
    [SerializeField] private float screenIntensity = 2.0f;
    [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); //orange
    [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f); //red
    [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f); //greenish

    private void Awake()
    {
        Instance = this;
        screenPanel.material.SetVector("_EmissionColor", screenIntensity * screenNormalColor);
    }

    private string currentInput = string.Empty;
    public void ReceiveInput(string input)
    {
        if(input == "DEL")
        {
            if(currentInput.Length > 0)
                currentInput = currentInput.Substring(0, currentInput.Length - 1);

        }
        else if(input == "ENTER")
        {
            CheckCode();
            return;
        }
        else
        {
            currentInput += input;
        }

        displayText.text = currentInput; 
    }

    private void CheckCode()
    {
        if (currentInput.Equals(correctCode))
        {
            CorrectCode();
            CompleteTask();
        }
        else
        {
            StartCoroutine(WrongCode());
        }
           


    }

    private IEnumerator WrongCode()
    {
        Debug.Log("Wrong Code");
        screenPanel.material.SetVector("_EmissionColor", screenIntensity * screenDeniedColor);
        displayText.text = "WRONG";

        yield return new WaitForSeconds(displayResultTime);

        screenPanel.material.SetVector("_EmissionColor", screenIntensity * screenNormalColor);
        currentInput = string.Empty;
        displayText.text = currentInput;
    }

    private void CorrectCode()
    {
        Debug.Log("Correct Code");
        screenPanel.material.SetVector("_EmissionColor", screenIntensity * screenGrantedColor);
        displayText.text = "CORRECT";
    }

    private void CompleteTask()
    {
        door.OpenDoor();
    }
}
