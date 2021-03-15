using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    private InputActions m_input = null;
    private Vector2 m_lookDelta;


    void Awake()
    {
        m_input = new InputActions();
    }


    void Update()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string _axisName)
    {
        m_lookDelta = m_input.Camera.Rotation.ReadValue<Vector2>();
        m_lookDelta.Normalize();

        if(_axisName == "Mouse X")
        {
            return m_lookDelta.x;
        }
        else if (_axisName == "Mouse Y")
        {
            return m_lookDelta.y;
        }

        return 0;
    }

    private void OnEnable()
    {
        m_input.Camera.Enable();
    }

    private void OnDisable()
    {
        m_input.Camera.Disable();
    }
}
