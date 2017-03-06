using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    public ParticleSystem glintParticle;
    public Rigidbody arrowHeadRB;
    public Rigidbody shaftRB;

    private Vector3 prevPosition;
    private Quaternion prevRotation;
    private Vector3 prevVelocity;
    private Vector3 prevHeadPosition;

    public bool isFired = false;
    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Rigidbody not found");
    }

    void Update()
    {
        if (isFired)
        {
            transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime);
        }
    }
}
