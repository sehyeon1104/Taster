using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AI_Mob_Nurse : AI_Mob_Default
{
    private int hashMove = Animator.StringToHash("Move");
    private int hashTrigger = Animator.StringToHash("Trigger");

    Coroutine motionCoroutine = null;
    private void Update()
    {
    }
    public override int CurrentHp { get => currentHp; set => currentHp =value; }
    public override int MaxHp { get; set; }
    public override float Speed { get => speed; set=>speed=value; }

    public override void Action(Transform target)
    {
        if (motionCoroutine != null)
            motionCoroutine = null;
        agent.isStopped = true;
        anim.SetBool(hashMove, false);
    }

    public override void GetHit(int damage, GameObject damgeDelear)
    {
        
    }

    public override void Idle()
    {
        anim.SetBool(hashMove, false);
        if (motionCoroutine == null)
            motionCoroutine = StartCoroutine(Motion());
    }

    public override void Move(Vector3 targetPos)
    {
        if (motionCoroutine != null)
            motionCoroutine = null;
        Debug.Log("����!");
        agent.isStopped = false;
        anim.SetBool(hashMove, true);
        agent.SetDestination((transform.position-targetPos) * agent.speed);
    }

    private IEnumerator Motion()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            anim.SetTrigger(hashTrigger);
        }
    }

}
