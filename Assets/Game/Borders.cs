using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    private Camera cam;


    void Awake()
    {
        cam = Camera.main;  
    }

    
    void Update()
    {
        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;

        Vector3 leftBot = cam.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));

        float x_left = leftBot.x - 2;
        float x_right = rightTop.x + 2;
        float z_top = rightTop.z + 2;
        float z_bot = leftBot.z - 2;

        

        if (transform.position.x < x_left)
        {
            transform.position = new Vector3(x_right-2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > x_right)
        {
            transform.position = new Vector3(x_left+2, transform.position.y, transform.position.z);
        }
        else if (transform.position.z < z_bot-1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_top);
        }
        else if (transform.position.z > z_top+1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z_bot );
        }
    }
}
