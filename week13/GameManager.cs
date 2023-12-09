using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    void GetCoin()
    {
        coinCount++;
        coinText.text = coinCount + "";
        Debug.Log("µ¿Àü : " + coinCount);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void RedCoinStart()
    {
        DestroyObstacles();
    }
    void DestroyObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            Destroy(obstacles[i]);
        }
    }
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
