using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedBuff")]
public class SpeedBuff : PowerupEffect
{
    public float amount;
    public override void Apply(GameObject target)
    {
        playerController player = target.GetComponent<playerController>();
        if (player != null) // CHecks if player isnt null
        {
            player.StartCoroutine(TemporarySpeedBoost(player));
        }
    }
    private IEnumerator TemporarySpeedBoost(playerController player)
    {
        player.speed += amount; // Applys the speed buff
        yield return new WaitForSeconds(10f); 
        player.speed -= amount; 
    }
}
