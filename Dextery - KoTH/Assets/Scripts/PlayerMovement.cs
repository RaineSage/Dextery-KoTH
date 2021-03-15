using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    #region --- Variables ---
    [Header("Movement Settings")]

    [SerializeField] [Range(1, 10)]
    private float m_movementSpeed = 5f;
    [SerializeField] [Range(1, 10)] [Tooltip("Declares how smooth the Player rotates")]
    private float m_smoothness = 5f;


    [Header("Camera Settings")]

    [SerializeField]
    private Transform m_camera = null;
    [SerializeField] [Tooltip("Which Mesh is supposed to rotate?")]
    private Transform m_dexteryModel = null;

    private InputActions m_input = null;
    private Rigidbody m_rigidbody;
    #endregion



    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_input = new InputActions();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // get Input values
        Vector2 movementInput = m_input.Player.Movement.ReadValue<Vector2>();

        Vector3 direction = movementInput.x * transform.right + movementInput.y * transform.forward;
        direction = direction.normalized * m_movementSpeed;

        // move in relation to camera view
        direction = m_camera.TransformDirection(direction);

        direction.y = m_rigidbody.velocity.y;
        m_rigidbody.velocity = direction;

        direction.y = 0;
        if(direction != Vector3.zero)
        {
            m_dexteryModel.transform.rotation = Quaternion.Slerp(m_dexteryModel.transform.rotation, 
                                                                 Quaternion.LookRotation(direction), m_smoothness * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        m_input.Player.Enable();
    }

    private void OnDisable()
    {
        m_input.Player.Disable();
    }
}
