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
        if (animator != null)
        {
            animator.SetBool("isChampiTrigger", true);
        }

    }

    private void DeactivateMushroomAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isChampiTrigger", false);
        }
    }
}