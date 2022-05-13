using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]

public class CharacterCombat : MonoBehaviour
{

    CharacterStats myStats;
    public float attackSpeed;
    private float attackCoolDown = 0;
    public float attackDelay;

    public event System.Action OnAttack;
    private void Start()
    {
        myStats = GetComponent<CharacterStats>();

        if (OnAttack != null) {

            OnAttack();
        }
        attackSpeed = myStats.attackSpeed.getValue();
        attackDelay = 0.6f;
    }

    private void Update()
    {
        attackCoolDown -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats) {

        if (attackCoolDown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats,attackDelay));
            attackCoolDown = 1 / attackSpeed;
        }
    
    
    
    }

    IEnumerator DoDamage(CharacterStats stats, float delay) {

        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.getValue());
    }

}
