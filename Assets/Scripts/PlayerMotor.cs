using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    public Rigidbody rb;
    public Camera cam;

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    private Vector3 thrustForce;
    private float cameraRotationAngleLimit = 45f;   // this is the angle for the rotation of the camera starting by initial position

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        rotation = Vector3.zero;
        cameraRotation = Vector3.zero;
        thrustForce = Vector3.zero;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation) // used also for check that the angle of camera's rotation is not out of range
    {
        cameraRotation = cameraRotation - _cameraRotation;
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
        DoRotation();
        DoCamRotation();
        DoFlight();
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
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }

    private void DoCamRotation()
    {
        if (cameraRotation == Vector3.zero)
            return;
        cam.transform.localEulerAngles = cameraRotation;
    }

    private void DoFlight()
    {
        if (thrustForce == Vector3.zero)
            return;
        rb.AddForce(thrustForce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

}
