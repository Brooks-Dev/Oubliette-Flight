using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _hitInSwing;
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamagable hit = other.GetComponent<IDamagable>();
        if (hit != null && _hitInSwing == false)
        {
            int pain = 1;
            if (!other.CompareTag("Player"))
            {
                if (GameManager.Instance.FlamingSword == true)
                {
                    pain++;
                }
            }
            hit.Damage(pain);
            _hitInSwing = true;
            StartCoroutine(SwingHitCooldown());
        }
    }

    private IEnumerator SwingHitCooldown()
    {
        yield return new WaitForSeconds(1.94f);
        _hitInSwing = false;
    }
}
