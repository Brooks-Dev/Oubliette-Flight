using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiderAcid : MonoBehaviour
{
    private Spider _spider;

    private void Start()
    {
        _spider = GameObject.Find("Spider_Enemy").GetComponent<Spider>();
    }

    public void FireAcid()
    {
        _spider.Attack();
    }
}
