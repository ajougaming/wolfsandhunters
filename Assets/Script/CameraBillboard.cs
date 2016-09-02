using UnityEngine;
using System.Collections;

public class CameraBillboard : MonoBehaviour {

    public Camera mainCamera;

	
	// Update is called once per frame
    void Awake()
    {
        mainCamera = Camera.main;
    }
	void Update () {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back,
            mainCamera.transform.rotation * Vector3.up);
	}
}
