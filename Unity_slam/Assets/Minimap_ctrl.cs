using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Minimap_ctrl : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //划线的例程（假设地图分辨率为1000*1000）：
        Vector3 orgPos = new Vector3(0, 0);
        Vector3 endPos = new Vector3(500, 500);
        Player.GetComponent<Minimap_draw>().GLDrawLine(orgPos, endPos);


        //鼠标响应的例程
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse button up");
        }
    }



}
