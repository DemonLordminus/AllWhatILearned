using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected Animator anim;

    PhysicsCheck physicsCheck;


    [Header("基本参数")]
    public float normalSpeed;
    public float chaseSpeed;
    public float currentSpeed;
    public Vector3 faceDir;

    public float hurtForce;
    public Transform attacker;

    [Header("计时器")]
    
    public float waitTime;
    public float waitTimeCounter;
    public bool wait;
    
    [Header("状态")]
    
    public bool isHurt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;//设置等待计时器等于等待时间

    }

    public void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        if((physicsCheck.touchLeftWall&&faceDir.x<0)||(physicsCheck.touchRightWall&&faceDir.x>0))
        {
            wait = true;
            anim.SetBool("walk", false);
        }
        TimeCounter();
    }

    private void FixedUpdate()
    {
        if(!isHurt)
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    public void TimeCounter()
    {
        if(wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0)
            {
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
            }
        }
    }

    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //转身
        if(attackTrans.position.x-transform.position.x>0)
            transform.localScale = new Vector3(-1, 1, 1);
        if(attackTrans.position.x - transform.position.x < 0)
            transform.localScale = new Vector3(1, 1, 1);

        //受伤被击退
        isHurt = true;
        anim.SetTrigger("hurt");

        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;

        rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);

    }


}
