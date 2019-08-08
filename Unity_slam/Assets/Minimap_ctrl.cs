using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class Minimap_ctrl : MonoBehaviour
{
    public GameObject Player;
    public GameObject minimap_quad;
    private bool entext = false;
    enum textcolor { red = 1, green, blue,black };
    private string[] vtext = new string[10] ;
    private string[] vcolor = new string[10] ;
    private string[] vsize = new string[10] ;
    public RenderTexture rt;

    void Start()
    {
        entext = true;
        for (int n = 0; n < 10; n++)
        {
            vtext[n] = "";
            vcolor[n] = "<color=#000000>";
            vsize[n] = "<size=30>";
        }
    }

    // Update is called once per frame
    void Update()
    {

        //划线的例程（假设地图分辨率为1000*1000）：
        drawline(100,100, 500, 500);

        //设备信息显示的例程
        settext(1, "设备状态", textcolor.blue, 40);
        settext(2, "摄像头：正常", textcolor.green, 30);
        settext(4, "地图加载正常", textcolor.green, 30);

        //保存小地图例程
        saveminimap("g:/minimap", "minimap");


        //鼠标响应的例程
        if (Input.GetMouseButtonDown(0))
        {
            //设定小地图例程
            setminimap("slam_map2");

            Debug.Log(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("mouse button up");
        }
    }

    void OnGUI()

    {
        if (entext)
        {
            for (int n = 0; n < 10; n++) 
                GUI.Label(new Rect(Screen.width * 0.7f, Screen.height * (1.0f - 0.9f+0.05f*n), 400, 80), vcolor[n]+vsize[n] + vtext[n] + "</size></color>");  
        }
    }

    //画线函数 (x1,x2) to (x2,y2)
    private void drawline(int x1,int y1,int x2, int y2)
    {
        x1 = Player.GetComponent<Minimap_draw>().width - x1;
        x2 = Player.GetComponent<Minimap_draw>().width - x2;
        y1 = Player.GetComponent<Minimap_draw>().height - y1;
        y2 = Player.GetComponent<Minimap_draw>().height - y2;
        Vector3 orgPos = new Vector3(x1, y1);
        Vector3 endPos = new Vector3(x2, y2);
        Player.GetComponent<Minimap_draw>().GLDrawLine(orgPos, endPos);
    }

    //文本显示函数 settext（第几行，内容，颜色，大小） 最多显示十行内容，即line最大是10
    private void settext(int line, string text, textcolor color, int size)
    {
        if (line > 0 && line < 11)
            line -= 1;
        vtext[line] = text;
        if (color == textcolor.red)
            vcolor[line] = "<color=#ff0000>";
        else if (color == textcolor.green)
            vcolor[line] = "<color=#00ff00>";
        else if (color == textcolor.blue)
            vcolor[line] = "<color=#0000ff>";
        else if (color == textcolor.black)
            vcolor[line] = "<color=#000000>";
        vsize[line] = "<size="+size.ToString()+ ">";
    }

    //加载小地图
    private void setminimap(string name)
    {
        Texture img = (Texture)Resources.Load(name);
        minimap_quad.GetComponent<Renderer>().material.mainTexture = img;
    }

    //保存小地图函数
    private bool saveminimap( string contents, string pngName)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.ARGB32, false);
        png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        byte[] bytes = png.EncodeToPNG();
        if (!Directory.Exists(contents))
            Directory.CreateDirectory(contents);
        FileStream file = File.Open(contents + "/" + pngName + ".png", FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        Texture2D.DestroyImmediate(png);
        png = null;
        RenderTexture.active = prev;
        return true;

    }
}
