using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeartsUI : MonoBehaviour
{
    public Player player;
    int maxHealth;
    int actualHealth;
    Image[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        maxHealth = player.maxHealth;
        actualHealth = player.health;
        GenerateHearts();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void GenerateHearts()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
    }
}
