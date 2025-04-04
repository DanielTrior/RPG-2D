using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player player; // Reference to the Player script
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>(); // Get the Player script component from the parent object
    }

    private void AnimationTrigger() // Animation trigger method
    {
        player.AttackOver(); // Call the Attack method from the Player script
    }
}
