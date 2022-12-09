using UnityEngine;
using Unity.Netcode;    

public class FistAttack : Attack
{
    public ActionControl actionControl;
    public int damage = 5;
    public Animator animator;
    public Animator networkAnimator;

    //[Client]
    public override void DealDamage(string id, bool isBlocked)
    {
        if (!isBlocked)
        {
            actionControl.Damage(id, damage, "Fist");
        }
        else
        {
            animator.SetTrigger("Deflected");
            networkAnimator.SetTrigger("Deflected");
            actionControl.Block(id, "Fist");
        }
    }
}
