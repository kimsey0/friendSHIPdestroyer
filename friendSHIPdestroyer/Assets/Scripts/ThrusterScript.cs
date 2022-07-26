using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrusterScript : MonoBehaviour
{
    public bool IsFirst;
    public string ThrustInputAxisName = "Thrust1";
    public InputActionAsset InputActionAsset;
    public float RotationForce = 1;
    public float ThrustForce = 1000;
    private Rigidbody _rigidbody;
    private @Input _input;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = new @Input();
        _input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var rotationInput = IsFirst ? _input.Thruster1.Rotation : _input.Thruster2.Rotation;
        var rotationForce = rotationInput.ReadValue<float>() * RotationForce;
        _rigidbody.AddForce(transform.right * rotationForce);

        var thrustInput = IsFirst ? _input.Thruster1.Thrust : _input.Thruster2.Thrust;
        var thrustForce = thrustInput.ReadValue<float>() * ThrustForce;
        _rigidbody.AddForce(transform.up * thrustForce);
    }
}
