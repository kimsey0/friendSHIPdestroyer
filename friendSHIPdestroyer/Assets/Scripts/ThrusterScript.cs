using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : MonoBehaviour
{
    public string RotationInputAxisName = "Horizontal";
    public float RotationForce = 1;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var rotationForce = Input.GetAxis(RotationInputAxisName) * RotationForce;
        _rigidbody.AddForce(transform.right * rotationForce);

    }
}
