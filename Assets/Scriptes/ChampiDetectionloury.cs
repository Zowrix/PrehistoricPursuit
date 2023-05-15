using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampiDetectionloury : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {

        Heroloury.onPlayerTouchingMushroom += ActivateMushroomAnimation;
        Heroloury.onPlayerExitMushroom += DeactivateMushroomAnimation;
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