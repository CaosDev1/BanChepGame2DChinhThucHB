using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;

    private bool isFacingRight = true;

    private Character target;
    public Character Target => target;
    
    private IState currtentState;


    private void Update()
    { 
        if(currtentState != null &&  !IsDead)
        {
            currtentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
    }

    
    
    public void ChangeState(IState newState)
    {
        if(currtentState != null)
        {
            currtentState.OnExit(this);
        }
        
        currtentState = newState;
        
        if(currtentState != null)
        {
            currtentState.OnEnter(this);
        }
        
    }

    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");

        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange()
    {
        if(target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == ("EnemyWall"))
        {
            ChangeDirection(!isFacingRight);
        }
    }

    public void ChangeDirection(bool isFacingRight)
    {
        this.isFacingRight = isFacingRight;

        transform.rotation = isFacingRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }else if(target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
