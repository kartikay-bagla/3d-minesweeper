using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{

    [Header("Basic")]
    [SerializeField]
    int maxHealth = 100;
    int currentHealth;

    [Header("Flight")]
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float lookSensitivity = 15f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring Settings:")]
    
    [SerializeField]
    private float jointSpring = 20f;
    
    [SerializeField]
    private float jointMaxForce = 40f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private void Movement() {
        // Movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movVertical + _movHorizontal).normalized * speed;

        motor.Move(_velocity);

        // Rotation
        float _yRot= Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        float _xRot= Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        motor.RotateCamera(_cameraRotationX);

        // Thruster Jump
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump")) {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0);
        } else {
            SetJointSettings(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

    
    public void TakeDamage(int damage) {
        // currentHealth -= damage;
        // Debug.Log(currentHealth);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    private void SetJointSettings (float _jointSpring) {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
