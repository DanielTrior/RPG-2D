using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>(); // Reference to the Player component in the parent GameObject
    private void AnimationTrigger() // This method is called when the animation trigger is activated, used for attack animations
    {
        player.AnimationTrigger();
    }
}
