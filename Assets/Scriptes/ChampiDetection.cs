using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampiDetection : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {

        Heroloury.onPlayerTouchingMushroom += ActivateMushroomAnimation;
        Heroloury.onPlayerExitMushroom += DeactivateMushroomAnimation;
    }

    private void ActivateMushroomAnimation()
    {
        animator.SetBool("isChampiTrigger", true);

    }

    private void DeactivateMushroomAnimation()
    {
        animator.SetBool("isChampiTrigger", false);

        Debug.Log(animator);
    }
}