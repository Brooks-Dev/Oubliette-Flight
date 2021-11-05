using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAcid : MonoBehaviour
{
    [SerializeField]
    private float _acidVelocity = 10.0f;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.right * _acidVelocity;
        Destroy(this.gameObject, 5.0f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamagable hit = other.GetComponent<IDamagable>();
            if (hit != null)
            {
                hit.Damage();
                Destroy(this.gameObject);
            } 
        }
    }
}
