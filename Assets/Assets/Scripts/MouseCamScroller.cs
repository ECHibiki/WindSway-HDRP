using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamScroller : MonoBehaviour
{
    public float x_sensitivity = 100, y_sensitivity = 50, zoom_sensitivity = 50f;
    public Vector3 center = new Vector3(0.0f,0.0f,0.0f);
	public float cam_bottom = 0.5f;
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
		
		
		if (Mathf.Abs(this.transform.rotation.x) > Mathf.Abs(this.transform.rotation.w) || target.transform.position.y + center.y - cam_bottom > this.transform.position.y )
        {//OoB
            this.transform.RotateAround(new Vector3(
                target.transform.position.x + center.x,
                target.transform.position.y + center.y,
                target.transform.position.z + center.z),
            this.transform.right,
            -Input.GetAxis("Mouse Y") * y_sensitivity * Time.deltaTime);
            //Debug.Log("zzz");
        }
        //Debug.Log("----");
    }
}
