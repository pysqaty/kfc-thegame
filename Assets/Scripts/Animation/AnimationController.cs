using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetToggleAnimation(Enum animationType, bool value)
    {
        animator.SetBool(AnimatorParameterAttribute.GetParameterName(animationType), value);
    }

    public void SetFloatAnimation(Enum animationType, float value)
    {
        animator.SetFloat(AnimatorParameterAttribute.GetParameterName(animationType), value);
    }

    public void TriggerAnimation(Enum animationType)
    {
        animator.SetTrigger(AnimatorParameterAttribute.GetParameterName(animationType));
    }
}
