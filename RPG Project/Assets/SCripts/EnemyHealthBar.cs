using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image heathBar;
    EnemyManager enemyManager;
    GameObject enemyCurrent;
    PlayerManager playerManager;
    public int currentHealth;
    public int maxHealth;

    public void Start()
    {
        playerManager = PlayerManager.instance;
        heathBar = GetComponent<Image>();
        maxHealth = enemyManager.Enemy.GetComponent<CharacterStats>().health.getValue();
    }

    public void Update()
    {
        if (enemyManager.Enemy != null)
        {
            currentHealth = enemyManager.Enemy.GetComponent<CharacterStats>().currentHealth;
        }
        heathBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

}
