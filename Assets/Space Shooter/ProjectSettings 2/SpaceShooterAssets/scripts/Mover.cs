using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    Rigidbody physic;

    [SerializeField] float speed;

    void Start()
    {
        physic = GetComponent<Rigidbody>();  //* bizim rigidbody'ye erişmemizi sağlıyor

        physic.linearVelocity = transform.forward * speed;
    }

    
}
