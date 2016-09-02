using UnityEngine;
using System.Collections;

public class CameraReference : MonoBehaviour {


    private float h = 0.0f;
    private float v = 0.0f;
    private Transform tr;
    private Vector3 moveDir;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;
    private float currentY = 60.0f;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;
    // Use this for initialization
    void Start()
    {
       // tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;
        currentY = Mathf.Clamp(currentY, minimumY, maximumY);
        Quaternion rotation = Quaternion.Euler(0, 0, currentY);
        // tr.Rotate(Vector3.right * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"), Space.Self);
        transform.localEulerAngles = new Vector3(-currentY, transform.localEulerAngles.y, 0);
        // tr.position.y = Mathf.Clamp();
    }
}
