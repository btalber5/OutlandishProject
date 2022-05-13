using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;
    bool canAttack;
    void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();

        //attack enemy
        CharacterCombat playerCombat = playerManager.Player.GetComponent<CharacterCombat>();
        

        Animator animator = playerManager.Player.GetComponentInChildren<Animator>();

        if (playerCombat != null)
        {
            animator.SetBool("canAttack",true);
            playerCombat.Attack(myStats);

        }
    }
}
