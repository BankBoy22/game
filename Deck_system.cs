using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deck_system : MonoBehaviour
{
    public void OnGameBackButtonClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
