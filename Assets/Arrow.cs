using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour {
    public bool isFired = false;
    private Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Rigidbody not found");
    }

	void Update () {
	    if(isFired)
        {
            transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime); 
        }
    }
}
