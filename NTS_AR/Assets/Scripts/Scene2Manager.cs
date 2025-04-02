using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class Scene2Manager : MonoBehaviour
{

    public ARRaycastManager RaycastManager;
    public TrackableType TypeToTrack = TrackableType.PlaneWithinBounds;
    public GameObject PrefabToInstantiate;
    public PlayerInput PlayerInput;
    public List<Material> Materials;
    public TMP_Text InputField;
    
    
    private InputAction touchPressAction;
    private InputAction touchPosAction;
    private InputAction touchPhaseAction;


    
    private int cubeCount;
    private List<GameObject> instantiatedCubes;
    // Start is called before the first frame update
    void Start()
    {
        touchPressAction = PlayerInput.actions["TouchPress"];
        touchPosAction = PlayerInput.actions["TouchPos"];
        instantiatedCubes = new List<GameObject>();
        touchPhaseAction = PlayerInput.actions["TouchPhase"];
        cubeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (touchPressAction.WasPerformedThisFrame())
        {
            var touchPhase = touchPhaseAction.ReadValue<UnityEngine.InputSystem.TouchPhase>();
            if (touchPhase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                OnTouch();
            }
        }
    }

    private void OnTouch()
    {
        var touchPos = touchPosAction.ReadValue<Vector2>();
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        RaycastManager.Raycast(touchPos, hits, TypeToTrack);
        
        if (hits.Count > 0)
        {
            ARRaycastHit firstHit = hits[0];
            GameObject cube = Instantiate(PrefabToInstantiate, firstHit.pose.position, firstHit.pose.rotation);
            instantiatedCubes.Add(cube);
            update_text();

        }
    }

    private void update_text()
    {
        cubeCount++;
        InputField.text = "cubes : " + cubeCount;
    }
    
    public void ChangeColor()
    {
        foreach (GameObject cube in instantiatedCubes)
        {
            int randomIndex = Random.Range(0, Materials.Count);
            Material randomMaterial = Materials[randomIndex];
            cube.GetComponent<MeshRenderer>().material = randomMaterial;
        }
    }
}
