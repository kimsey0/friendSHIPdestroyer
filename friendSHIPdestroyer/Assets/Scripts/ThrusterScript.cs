using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class ThrusterScript : MonoBehaviour
{
    public bool IsFirst;
    public string ThrustInputAxisName = "Thrust1";
    public InputActionAsset InputActionAsset;
    public float RotationSpeed = 1;
    public float ThrustForce = 1000;
    public Rigidbody Rigidbody;
    private @Input _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = new @Input();
        _input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var rotationInput = IsFirst ? _input.Thruster1.Rotation : _input.Thruster2.Rotation;
        float newRotation = CalculateNewRotation(rotationInput);
        transform.localEulerAngles = new Vector3(0, 0, newRotation);

        var thrustInput = IsFirst ? _input.Thruster1.Thrust : _input.Thruster2.Thrust;
        var thrustForce = thrustInput.ReadValue<float>() * ThrustForce * Time.deltaTime;
        var direction = transform.up + transform.right;
        Rigidbody.AddForceAtPosition(direction * thrustForce, transform.position);
    }

    private float CalculateNewRotation(InputAction rotationInput)
    {
        var newRotation = transform.localEulerAngles.z + rotationInput.ReadValue<float>() * RotationSpeed * Time.deltaTime;
        if (IsFirst)
        {
            if (newRotation is > 90 and < 180)
            {
                newRotation = 90;
            }
            if (newRotation is > 180 and < 270)
            {
                newRotation = 270;
            }
        }
        else
        {
            newRotation = math.clamp(newRotation, 90, 270);
        }

        return newRotation;
    }
}
