using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats
{
    Animator animator;

    private void Start()
    {
        animator = PlayerManager.instance.Player.GetComponentInChildren<Animator>();
        
    }

    public override void Die()
    {
        PlayerManager.instance.enemiesKilled += 1;
        base.Die();

        //some death effect
        animator.SetBool("canAttack",false);
        if ( gameObject == GameObject.Find("Enemy 2 (2)")) {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        Destroy(gameObject);
    }

    
}
