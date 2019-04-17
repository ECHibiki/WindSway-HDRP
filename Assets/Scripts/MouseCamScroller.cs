using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamScroller : MonoBehaviour
{
    public float x_sensitivity = 100, y_sensitivity = 50, zoom_sensitivity = 50f;
    public Vector3 center = new Vector3(0.0f,0.0f,0.0f);
	public float cam_bottom = 0.5f;
    public float min_zoom = 0.5f;
    public float max_zoom = 10.0f;
    public float zoom_rate = 1.0f;
    public GameObject target;

    bool start = false;
    float max_cam_dist;

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            start = true;
        }

        //Debug.Log(this.transform.rotation);

        //left and right
        this.transform.RotateAround(
            new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            Vector3.up,
            Input.GetAxis("Mouse X") * x_sensitivity * Time.deltaTime);

        //up and down
            this.transform.RotateAround(new Vector3(
                    target.transform.position.x + center.x,
                    target.transform.position.y + center.y,
                    target.transform.position.z + center.z),
                this.transform.right,
                Input.GetAxis("Mouse Y") * y_sensitivity * Time.deltaTime);
        //in and out	
        this.transform.localPosition += this.transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoom_sensitivity * Time.deltaTime;

        //too in or out
        if (Vector3.Distance(this.transform.position, target.transform.position) < min_zoom
            || Vector3.Distance(this.transform.position, target.transform.position) > max_zoom)
        {
            this.transform.localPosition -= this.transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoom_sensitivity * Time.deltaTime;
        }

        //Out of BoundsY
        if (Mathf.Abs(this.transform.rotation.x) > Mathf.Abs(this.transform.rotation.w) 
            || target.transform.position.y + center.y - cam_bottom > this.transform.position.y)
        {
            this.transform.RotateAround(new Vector3( 
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            this.transform.right,
            -Input.GetAxis("Mouse Y") * y_sensitivity * Time.deltaTime);
            //Debug.Log("zzz");
        }
    }

    bool CheckCameraInside()
    {
        //resolve instersect with buildings
        //check and resolve issues
        RaycastHit ray_info;
        // check if cam is in things
        if (Physics.Raycast(
            this.transform.position,
            new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z) - this.transform.position,
            out ray_info,
            Mathf.Infinity))
        {
            if (ray_info.collider.gameObject != target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
