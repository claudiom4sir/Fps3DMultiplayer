using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMotor : MonoBehaviour {

    public Camera cam;
    public ParticleSystem[] thrusters;

    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    private Vector3 thrustForce;
    private float cameraRotationAngleLimit = 45f;   // this is the angle for the rotation of the camera starting by initial position
    private Animator animator;
    private float thrustersDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        rotation = Vector3.zero;
        cameraRotation = Vector3.zero;
        thrustForce = Vector3.zero;
        animator = GetComponent<Animator>();
        thrustersDirection = 0f;
    }

    public void Move(Vector3 _velocity, float _thrustersDirection)
    {
        velocity = _velocity;
        thrustersDirection = _thrustersDirection;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation) // used also for check that the angle of camera's rotation is not out of range
    {
        cameraRotation = cameraRotation - _cameraRotation * Time.fixedDeltaTime; // is x is positive, the rotation goes down and if is negative, the rotation goes up
        float x = Mathf.Clamp(cameraRotation.x, -cameraRotationAngleLimit, cameraRotationAngleLimit);
        cameraRotation = new Vector3(x, 0f, 0f);
    }

    public void Fly(Vector3 _thrustForce) // it takes the thrust for to fly
    {
        thrustForce = _thrustForce;
    }

    private void FixedUpdate() // used instead Update because it's controlled by Phisics Engine
    {
        DoMoviment();
        AnimateThrusters();
        DoRotation();
        DoCamRotation();
        DoFlight();
    }

    private void AnimateThrusters()
    {
        animator.SetFloat("MovimentDirection", thrustersDirection);
    }

    private void DoMoviment()
    {
        if (velocity == Vector3.zero)
            return;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void DoRotation()
    {
        if (rotation == Vector3.zero)
            return;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation * Time.fixedDeltaTime));
    }

    private void DoCamRotation()
    {
        if (cameraRotation == Vector3.zero)
            return;
        cam.transform.localEulerAngles = cameraRotation; // if rotation is already equals to the limit, nothing happens
        // localEulerAngles rotates on the x axis the camera, where up rotation is negative x and down rotation is positive x
    }

    private void DoFlight()
    {
        if (thrustForce == Vector3.zero)
            return;
        rb.AddForce(thrustForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

}
