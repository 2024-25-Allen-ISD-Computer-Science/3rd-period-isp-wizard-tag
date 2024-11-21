using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/JumpBoost")]
public class JumpBoost : PowerupEffect
{
    public float jumpBoostAmount; // The amount to increase jump height
    public float duration = 10f;  // Duration of the effect

    public override void Apply(GameObject target)
    {
        playerController player = target.GetComponent<playerController>();
        if (player != null)
        {
            
            player.StartCoroutine(ApplyJumpBoost(player));
        }
    }

    private IEnumerator ApplyJumpBoost(playerController player)
    {
        // Increase jump height
        player.jumpingPower += jumpBoostAmount;

        yield return new WaitForSeconds(duration);

        // Revert the jump height
        player.jumpingPower -= jumpBoostAmount;
    }
}