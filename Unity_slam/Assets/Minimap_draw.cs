using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joint
{
    public Vector3 org;
    public Vector3 end;
}

public class Minimap_draw : MonoBehaviour
{
    private Vector3 orgPos;
    private Vector3 endPos;
    private ArrayList posAL;
    public Material lineMaterial;
    public int width = 1000;
    public int height = 1000;
    // Start is called before the first frame update
    void Start()
    {
        posAL = new ArrayList();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void GLDrawLine(Vector3 beg, Vector3 end)
    {
        GL.PushMatrix();
        GL.LoadOrtho();

        beg.x = beg.x/ width ;
        end.x = end.x / width;
        beg.y = beg.y / height;
        end.y = end.y / height;
        joint tmpJoint = new joint();
        tmpJoint.org = beg;
        tmpJoint.end = end;

        posAL.Add(tmpJoint);

        lineMaterial.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(new Color(255, 0, 0, 0.5f));
        for (int i = 0; i < posAL.Count; i++)
        {
            joint tj = (joint)posAL[i];
            Vector3 tmpBeg = tj.org;
            Vector3 tmpEnd = tj.end;
            GL.Vertex3(tmpBeg.x, tmpBeg.y, tmpBeg.z);
            GL.Vertex3(tmpEnd.x, tmpEnd.y, tmpEnd.z);
        }
        GL.End();
        GL.PopMatrix();
    }
    public void ClearLines()
    {
        posAL.Clear();
    }



    void OnPostRender()
    {
        GLDrawLine(orgPos, endPos);
    }
}
