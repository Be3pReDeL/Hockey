using UnityEngine;

[OPS.Obfuscator.Attribute.DoNotObfuscateClass]
public class DisableAttackParameter : StateMachineBehaviour
{
    [OPS.Obfuscator.Attribute.DoNotRename]
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
        animator.SetBool("Attack", false); 
    }
}
