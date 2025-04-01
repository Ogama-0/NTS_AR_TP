using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Scene2Manager : MonoBehaviour
{

    public ARRaycastManager RaycastManager;
    public TrackableType TypeToTrack = TrackableType.PlaneWithinBounds;
    public GameObject PrefabToInstantiate;
    public PlayerInput PlayerInput;
    public List<Material> Materials;
    
    private InputAction touchPressAction;
    private InputAction touchPosAction;
    
    private List<GameObject> instantiatedCubes;
    // Start is called before the first frame update
    void Start()
    {
        touchPressAction = PlayerInput.actions["TouchPress"];
        touchPosAction = PlayerInput.actions["TouchPos"];
        instantiatedCubes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchPressAction.WasPerformedThisFrame())
        {
            OnTouch();
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
        }
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
