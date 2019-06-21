using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("Life Properties")]
    public Image life;
    public Sprite[] lifes;

    [Header("Stars Properties")]
    public Text stars;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        life.sprite = lifes[GameManager.gm.GetPlayerHealth()];
        stars.text = GameManager.gm.GetStars().ToString();
    }


}
