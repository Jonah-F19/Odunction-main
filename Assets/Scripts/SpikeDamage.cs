using System.Collections;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 25;
    public float cooldown = 1.0f;

    private bool canDamage = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canDamage) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null)
            {
                pc.TakeDamage(damage);
                pc.ApplySpikeKnockback(); // NEW helper method in PlayerController
                StartCoroutine(ResetCooldown());
            }
        }
    }

    IEnumerator ResetCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(cooldown);
        canDamage = true;
    }
}
