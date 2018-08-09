using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [Header("Movement Settings")]
    public float speed = 5f;
    public float lookSensibility = 500f;
    public float thrustForce = 1000f;

    public Animator animator;

    [Header("Thruster Fuel Settings")]
    public float thrusterFuelSpeedUp = 0.5f;
    public float thrusterFuelConsume = 2f;
    private float currentThrusterFuel = 1f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    public float GetCurrentThrusterFuel()
    {
        return currentThrusterFuel;
    }

    private void Update()
    {
        if (PauseMenuUI.isActive)
            return;
        // calculate the moviment on the x (right, left) and/or z (forward, backwards)
        // with directional keys or moviment's keys (a,w,s,d)
        float xMoviment = Input.GetAxis("Horizontal");
        float zMoviment = Input.GetAxis("Vertical");
        Vector3 horizontalMoviment = transform.right * xMoviment;
        Vector3 verticalMoviment = transform.forward * zMoviment;
        Vector3 velocity = (horizontalMoviment + verticalMoviment) * speed;

        motor.Move(velocity, zMoviment);    // apply the moviment

        // calculate the rotation moviment with mouse
        float yRotation = Input.GetAxis("Mouse X"); // when you move mouse left or right, we have to rotate on the Y axis
        Vector3 rotationOnY = new Vector3(0f, yRotation, 0f) * lookSensibility;
        float xRotation = Input.GetAxis("Mouse Y");
        Vector3 rotationOnX = new Vector3(xRotation, 0f, 0f) * lookSensibility;

        motor.Rotate(rotationOnY);  // apply the rotation
        motor.RotateCamera(rotationOnX);    // apply camera rotation

        // calculate jump force
        Vector3 _thrustForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            if(currentThrusterFuel > 0f)
            {
                currentThrusterFuel = currentThrusterFuel - thrusterFuelConsume * Time.fixedDeltaTime;
                _thrustForce = Vector3.up * thrustForce; // when you press jump key, you make a thrustForce
            }
        }
        else
        {
            float fuel = currentThrusterFuel + thrusterFuelSpeedUp * Time.fixedDeltaTime;
            currentThrusterFuel = Mathf.Clamp(fuel, 0f, 1f);
        }
        
        motor.Fly(_thrustForce); // apply the thrust force for to fly - player can fly with a force equals to thrustForce
    }

}
