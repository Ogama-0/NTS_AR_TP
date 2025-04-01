using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillingCam : MonoBehaviour
{
    public GameObject ParticleEffect;
    private Vector2 touchPos;
    private RaycastHit hit;
    private Camera cam;
    public PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPosAction;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        touchPressAction = playerInput.actions["TouchPress"];
        touchPosAction = playerInput.actions["TouchPos"];

    }

    // Update is called once per frame
    void Update()
    {
        if (!touchPressAction.WasPerformedThisFrame())
        {
            return;
        }

    }
}
