using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour {
    public static float distance3 = 5.0f; 
    private Transform tr;
	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();  
	 
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (Physics.Raycast(tr.position, tr.TransformDirection(Vector3.forward), out hit))
        {
            distance3 = hit.distance;
        }

    }
}
