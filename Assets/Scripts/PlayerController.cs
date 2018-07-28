using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float lookSensibility = 5f;
    public PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // calculate the moviment on the x (right, left) and/or z (forward, backwards)
        // with directional keys or moviment's keys (a,w,s,d)
        float xMoviment = Input.GetAxis("Horizontal");
        float zMoviment = Input.GetAxis("Vertical");
        Vector3 horizontalMoviment = transform.right * xMoviment;
        Vector3 verticalMoviment = transform.forward * zMoviment;
        Vector3 velocity = (horizontalMoviment + verticalMoviment).normalized * speed;

        // apply the moviment
        motor.Move(velocity);

        // calculate the rotation moviment with mouse
        float yRotation = Input.GetAxis("Mouse X"); // when you move mouse left or right, we have to rotate on the Y axis
        Vector3 rotationOnY = new Vector3(0f, yRotation, 0f) * lookSensibility;
        float xRotation = Input.GetAxis("Mouse Y");
        Vector3 rotationOnX = new Vector3(xRotation, 0f, 0f) * lookSensibility;

        // apply the rotation
        motor.Rotate(rotationOnY);
        motor.RotateCamera(rotationOnX);
    }

}
