using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedStar2Controller : MonoBehaviour
{
    public Vector3 OrigPos;
    public float Radius;
    private float TotalTime = 0f;
   // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        spin();
    }


    [Header("圆周速度调控")]
    public float w;
    void spin()
    {

        //时间递加
        TotalTime += Time.deltaTime * w;
        //用x y 写圆周
        var x = Radius * Mathf.Cos(Mathf.PI / 2 + TotalTime);
        var y = Radius * Mathf.Sin(Mathf.PI / 2 + TotalTime);
        //位置移动
        transform.position = new Vector3(x, y, 0);

    }

}
