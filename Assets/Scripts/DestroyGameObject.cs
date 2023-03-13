using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    void Update()
    {
        Destroy(gameObject, 2f);
    }

}
