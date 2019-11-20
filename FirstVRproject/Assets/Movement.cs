using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Movement : MonoBehaviour
{

    public float Maxspeed = 1f;
    public float Sensitivity = 0.1f;

    public SteamVR_Action_Boolean M_MovePress = null;
    public ISteamVR_Action_Vector2 M_MoveValue = null;

    private float m_speed = 0.0f;
    RaycastHit hit;
    private CharacterController m_CharacterController = null;
    private Transform VRController;
    private Transform m_CameraRig;
    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
        m_CameraRig = SteamVR_Render.Top().origin;
        VRController = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }
    private void HandleHead()
    {
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        transform.eulerAngles = new Vector3(0.0f, VRController.rotation.eulerAngles.y, 0.0f);


        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
    }
    private void CalculateMovement()
    {
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        if (M_MovePress.state)
        {
            m_speed += M_MoveValue.axis.y * Sensitivity;
            m_speed = Mathf.Clamp(m_speed, -Maxspeed, Maxspeed);

            movement += orientation * (m_speed * Vector3.forward) * Time.deltaTime;

        }

        m_CharacterController.Move(movement);
    }
    private void HandleHeight()
    {
        float headHeight = Mathf.Clamp(VRController.localPosition.y, 1, 2);
        m_CharacterController.height = headHeight;

        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterController.height / 2;
        newCenter.y += m_CharacterController.skinWidth;

        newCenter.x = VRController.localPosition.x;
        newCenter.z = VRController.localPosition.z;

        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;


        m_CharacterController.center = newCenter;
    }
}
