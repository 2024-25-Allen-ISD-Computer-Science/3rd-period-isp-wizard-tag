using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/ShrinkDown")]
public class ShrinkDown : PowerupEffect
{
    public Vector3 shrinkScale = new Vector3(0.5f, 0.5f, 1f); 
    public float speedBoostMultiplier = 1.5f;      
    public float duration = 10f;                            

    public override void Apply(GameObject target)
    {
        playerController player = target.GetComponent<playerController>();
        if (player != null)
        {
            player.StartCoroutine(ApplyShrinkDown(player));
        }
    }

    private IEnumerator ApplyShrinkDown(playerController player)
    {
        // Store original properties
        Vector3 originalScale = player.transform.localScale; 
        float originalSpeed = player.speed;             

        // effect
        player.transform.localScale = new Vector3(
            Mathf.Abs(originalScale.x) * shrinkScale.x,
            Mathf.Abs(originalScale.y) * shrinkScale.y,
            originalScale.z
        ); // Ensure positive scale to avoid flipping direction
        player.speed *= speedBoostMultiplier;

        // Wait for the effect duration
        yield return new WaitForSeconds(duration);

        // Revert to original properties
        player.transform.localScale = originalScale;
        player.speed = originalSpeed;
    }
}