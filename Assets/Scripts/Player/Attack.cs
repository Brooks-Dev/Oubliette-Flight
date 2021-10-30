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
            hit.Damage();
            _hitInSwing = true;
            StartCoroutine(SwingHitCooldown());
        }
    }

    private IEnumerator SwingHitCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        _hitInSwing = false;
    }
}
