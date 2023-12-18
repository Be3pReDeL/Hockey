using UnityEngine;

public class DisableAttackParameter : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        animator.SetBool("Attack", false); 
    }
}
