using UnityEngine;
using StarterAssets;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private StarterAssetsInputs inputs;

    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        // Sprint funciona con Shift Y con el Left Trigger de un control
        if (inputs.sprint)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (inputs.breakdoor == false)
        {
            animator.SetTrigger("Idle");
            animator.ResetTrigger("BreakDoor");
        }
        else
        {
            animator.SetTrigger("BreakDoor");
            animator.ResetTrigger("Idles");
        }
    }
}


