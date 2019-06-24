using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    public static GameManager gm = null;

    [SerializeField] private int playerHealth;
    public void SetPlayerHealth(int value)
    {
        if (value <= 0)
            playerHealth = 0;
        else if (value >= 3)
            playerHealth = 3;
        else
            playerHealth = value;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    [SerializeField]  private int stars = 0;
    public void SetStars(int value)
    {
        if (value < 100)
            stars = value;
        else
        {
            stars = 0;
            SetPlayerHealth(GetPlayerHealth() + 1);
        }
    }
    public int GetStars()
    {
        return stars;
    }

    void Start()
    {
        if (gm == null)
            gm = this;
        else if (gm != null)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (playerHealth <= 0)
            Invoke("RestartLevel", 2f);

    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

   
}
