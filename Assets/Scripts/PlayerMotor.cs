using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRotation;
    public Rigidbody rb;
    public Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        rotation = Vector3.zero;
        cameraRotation = Vector3.zero;
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    private void FixedUpdate() // used instead Update because it's controlled by Phisics Engine
    {
        DoMoviment();
        DoRotation();
        DoCamRotation();
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
        cam.transform.Rotate(-cameraRotation); // - is used for invert the vector, and so the axis for the rotation
    }

}
