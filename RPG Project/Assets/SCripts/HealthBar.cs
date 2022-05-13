using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Image heathBar;
    PlayerManager playerManger;
    public int currentHealth;
    public int maxHealth;

    public void Start()
    {
        playerManger = PlayerManager.instance;
        heathBar = GetComponent<Image>();
        maxHealth = playerManger.Player.GetComponent<CharacterStats>().health.getValue();
    }

    public void Update()
    {

        currentHealth = playerManger.Player.GetComponent<CharacterStats>().currentHealth;
        heathBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

}
