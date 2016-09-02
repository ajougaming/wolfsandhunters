using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
    public Transform targetTr;
    public float dist = 10.0f;
    public float heightSpeed = 0.5f;
    public float height = 3.0f;
    public float dampTrace = 20.0f;
    public float rotSpeed = 100.0f;
    //for the Raycast cam;
    public RaycastHit hit;
    public float distanceToGround;
    public bool isRaycast =true ;
    public float area = 1.00f;
    private Transform tr;
    Vector3 camY;
    Vector3 camX;
    // Use this for initialization

    private float currentX = 0.0f;
    private float currentY = 60.0f;
    private int layerMask;
    public string layerName;
	void Start () {
        tr = GetComponent<Transform>();
        layerMask = 1 << LayerMask.NameToLayer(layerName);
  
    }
	
	// Update is called once per frame
	void Update () {
        //Ray ray;
        //ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //ray.origin = ray.GetPoint(0.5f);
        if (isRaycast)
        {
            Vector3 fwd = targetTr.TransformDirection(Vector3.forward);
            if (Physics.Raycast(targetTr.position, -targetTr.TransformDirection(Vector3.forward), out hit, Mathf.Infinity,layerMask))
            {
                distanceToGround = hit.distance * 0.1f;
               
               
            }
            Debug.DrawRay(targetTr.position, -fwd, Color.green, 0, true);
        }
        //currentX +=  Time.deltaTime *rotSpeed *Input.GetAxis("Mouse X");
        currentY += Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y");
		currentY = Mathf.Clamp(currentY, -90.0f, 90.0f);

    }

    void LateUpdate()
    {
        float distance = dist * distanceToGround;
        Vector3 dir;
        Quaternion rotation = Quaternion.Euler(0, 0, currentY);
        if (isRaycast)
        {

            if (distance >= 2.0f)
            {
                distance = 2.0f;
            }
            else if (distance < 0.1f)
            {
                distance = 0.1f;
            }
            dir = new Vector3(0, 0, -distance);
            //tr.position = Vector3.Lerp(tr.position, targetTr.position -  rotation * dir, Time.deltaTime * dampTrace);
            tr.position = Vector3.Lerp(tr.position, targetTr.position -  targetTr.forward  * distance, Time.deltaTime * dampTrace);
            // Debug.Log(tr.position);
        }
        else{
            dir = new Vector3(0, 0, -dist);
            //tr.position = Vector3.Lerp(tr.position, targetTr.position  -    targetTr.forward * dist   , Time.deltaTime * dampTrace);
            tr.position = Vector3.Lerp(tr.position, targetTr.position - targetTr.forward  * dist, Time.deltaTime * dampTrace);
        }

        tr.LookAt(targetTr.position);
      



    
        
    }
}
