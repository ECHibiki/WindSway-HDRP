using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCam : MonoBehaviour
{
    public float x_sensitivity = 100, y_sensitivity = 50;
    public Vector3 center = new Vector3(0.0f,0.0f,0.0f);
    public float max_zoom = 0.5f;
    public float zoom_rate = 0.5f;
    public GameObject target;

    bool start = false;
    float max_cam_dist;

    // Start is called before the first frame update
    // Don't start from Start()

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {

            start = true;
            max_cam_dist = Vector3.Distance(this.transform.position, new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z));
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
        if (Mathf.Abs(this.transform.rotation.x) > Mathf.Abs(this.transform.rotation.w) )//target.transform.position.y + center.y - 0.5f > this.transform.position.y )
        {//OoB
            this.transform.RotateAround(new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            this.transform.right,
            -Input.GetAxis("Mouse Y") * y_sensitivity * Time.deltaTime);
            //Debug.Log("zzz");
        }



        //check and resolve issues
        RaycastHit ray_info;
        // check if cam is in things
        if (Physics.Raycast(
            new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            this.GetComponent<BoxCollider>().bounds.center - new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            out ray_info,
            Mathf.Infinity)) {

            Debug.DrawRay(new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
                (-new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z) + this.GetComponent<BoxCollider>().bounds.center).normalized
                    * ray_info.distance, Color.yellow);
           // Debug.Log(ray_info.collider.tag);

            //shrink distance. cam is in something
            Vector3 zoom = (this.GetComponent<BoxCollider>().bounds.center - new Vector3(
                    target.transform.position.x + center.x,
                    target.transform.position.y + center.y,
                    target.transform.position.z + center.z)).normalized * Time.deltaTime * zoom_rate;
            if (ray_info.collider.tag != "MainCamera" && Vector3.Distance(this.GetComponent<BoxCollider>().bounds.center, new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z)) > max_zoom) {
                this.transform.position -= zoom;
                Physics.Raycast(
                                new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
                     (this.GetComponent<BoxCollider>().bounds.center + zoom) - new Vector3(
                        target.transform.position.x + center.x,
                        target.transform.position.y + center.y,
                        target.transform.position.z + center.z),
                    out ray_info,
                    Mathf.Infinity);
                if (ray_info.collider == null || ray_info.collider.tag == "MainCamera")
                {
                    this.transform.position += zoom;
                }
            }
            //expand distance. it's not
            else if (ray_info.collider.tag == "MainCamera" && Vector3.Distance(this.GetComponent<BoxCollider>().bounds.center, new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z)) < max_cam_dist)
            {
                this.transform.position += zoom;
                Physics.Raycast(new Vector3(
                    target.transform.position.x + center.x,
                    target.transform.position.y + center.y,
                    target.transform.position.z + center.z),
                        (this.GetComponent<BoxCollider>().bounds.center + zoom) - new Vector3(
                            target.transform.position.x + center.x,
                            target.transform.position.y + center.y,
                            target.transform.position.z + center.z),
                        out ray_info,
                        Mathf.Infinity);
                //Debug.Log("Grow" + ray_info.collider);
                if (ray_info.collider == null || ray_info.collider.tag != "MainCamera")
                {
                    this.transform.position -= zoom;
                }
            }
            else
            {
                //Debug.Log("B");
            }
        }
        //Debug.Log("----");
    }
}
