using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KillingCam : MonoBehaviour
{
    public GameObject ParticleEffect;
    private Vector2 touchPos;
    private RaycastHit hit;
    private Camera cam;
    public PlayerInput playerInput;
    private InputAction touchPressAction;
    private InputAction touchPosAction;

    private int killed = 0;
    public TMP_Text InputField;

    public AudioClip soundEffect;
    private AudioSource audioSource;

    public bool Win = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundEffect;

        InputField.text = "killed =  0";

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
        touchPos = touchPosAction.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(touchPos);
        
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.tag == "Enemy")
            {
                killed += 1;
                audioSource.Play();
                var clone = Instantiate(ParticleEffect, hitObj.transform.position, Quaternion.identity);
                clone.transform.localScale = hitObj.transform.localScale;
                Destroy(hitObj);
            }
            if (hitObj.tag == "king")
            {
                killed += 1000;
                audioSource.Play();
                Destroy(hitObj);
                Win = true;

            }
        }

        if (killed > 10)
        {
            Win = true;
        }

        if (Win)
        {
            InputField.text = "GG YOU WIN";
        }
        else
        {
            InputField.text = "killed = " + killed;
        }


    }

    
}
