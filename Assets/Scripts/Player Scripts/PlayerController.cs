using System;
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

    [SerializeField]
    private float thrusterFuelBurnSpeed = 1f;

    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;

    private float thrusterFuelAmount = 1f;

    [SerializeField]
	private LayerMask environmentMask;

    [Header("Spring Settings:")]
    
    [SerializeField]
    private float jointSpring = 20f;
    
    [SerializeField]
    private float jointMaxForce = 40f;

    public bool groundBelowTrigger = true;

    private PlayerMotor motor;
    private ConfigurableJoint joint;

    private void Movement() {


        RaycastHit _hit;
		if (Physics.Raycast (transform.position, Vector3.down, out _hit, 100f, environmentMask))
		{
			joint.targetPosition = new Vector3(0f, -_hit.point.y, 0f);
		} else
		{
			joint.targetPosition = new Vector3(0f, Mathf.Infinity, 0f);
		}


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
        if (Input.GetButton("Jump") & (thrusterFuelAmount > 0.05f)) {
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0);
        } else {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;
            SetJointSettings(jointSpring);
        }

        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        if (!groundBelowTrigger) {
            SetJointSettings(0);
            groundBelowTrigger = true;
        } 

        motor.ApplyThruster(_thrusterForce);
    }

    void Die()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.RestartGame();
    }
    
    public float GetFuelAmount() {
        return thrusterFuelAmount;
    }

    public float GetHealthAmount()
    {
        return (float)currentHealth / (float)maxHealth;
    }
    
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        Debug.Log(currentHealth);
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

        if (currentHealth <= 0) 
            Die();
    }

    private void SetJointSettings (float _jointSpring) {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
