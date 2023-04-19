using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampiDetection : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {

        Hero.onPlayerTouchingMushroom += ActivateMushroomAnimation;
        Hero.onPlayerExitMushroom += DeactivateMushroomAnimation;
    }

    private void ActivateMushroomAnimation()
    {
        animator.SetBool("isChampiTrigger", true);

    }

    private void DeactivateMushroomAnimation()
    {
        animator.SetBool("isChampiTrigger", false);
    }
}