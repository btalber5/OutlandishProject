using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offSet;
    public LayerMask wallMask;
    GameObject prevObj;
    Camera cam;
    bool doneChanging;
    public float zoomSpeed = 10f;
    private float currentZoom = 10f;

    public bool orbitPlayer;
    [SerializeField] public float orbitSpeed;

    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float pitch = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        //Debug.Log(cam.name);
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        RaycastHit hit;


        
        if (Physics.Raycast(ray, out hit,100))
        {
            GameObject wall = hit.transform.gameObject;

            //.Log(hit.transform.name);
            //Debug.Log(MathF.Log(wallMask.value, 2));
            if (wall.layer == MathF.Log(wallMask.value, 2))
            {

                prevObj = wall;
                Color color = wall.GetComponent<MeshRenderer>().material.color;
                if (color.a >= 0.1f)
                {
                    color.a -= 0.4f * (Time.deltaTime * 10f);
                }


                wall.GetComponent<MeshRenderer>().material.color = color;


            }

            

            



        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (orbitPlayer == true) {

            Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * orbitSpeed, Vector3.up);

            offSet = cameraTurnAngle * offSet;
        }
        
            transform.position = target.position - offSet * currentZoom;
            transform.LookAt(target.position + Vector3.down * pitch);
        

        

    }

    internal void setOrbitBool(bool boolIn) {

        this.orbitPlayer = boolIn;
    
    }
}
