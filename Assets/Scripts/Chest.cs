using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private PassiveManager passiveManager;

    private enum ChestState
    {
        Closed, Opened
    }
    private ChestState state = ChestState.Closed;
    private bool isInRange;

    [SerializeField] private GameObject buttonPrompt;

    private void Start()
    {
        isInRange = false;
        passiveManager = FindObjectOfType<PassiveManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInRange == true)
        {
            passiveManager.GivePassiveSelection();
            state = ChestState.Opened;
            ShowPrompt(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == ChestState.Closed && collision.tag == "Player")
        {
            isInRange = true;
            ShowPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInRange = false;
            ShowPrompt(false);
        }
    }

    private void ShowPrompt(bool boolean)
    {
        if(boolean == true)
        {
            buttonPrompt.SetActive(true);
            return;
        }
        buttonPrompt.SetActive(false);
    }
}
