using UnityEngine;
using System.Collections;

public class anim_xuanwo : MonoBehaviour {

	public string movieName = "YouFolderName" ;
  
    //动画数组    
    private Object[] anim;  
      
    //帧序列   
    private int nowFram;  
    //动画帧的总数   
    private int mFrameCount;  
    //限制一秒多少帧   
    private float fps = 15;  
    //限制帧的时间    
    private float time = 0;  
      
    void Start(){  
        if(anim == null){  
            anim = Resources.LoadAll(movieName);  
            mFrameCount = anim.Length;  
            nowFram = 0;  
            time = 0;  
        }  
    }  
      
    void Update () {  
        //绘制帧动画   
        DrawAnimation(anim);  
    }  
      
    void DrawAnimation(Object[] img){  
        //方法①：  
        this.renderer.material.mainTexture = (Texture)anim[nowFram];  
  
  
        //方法②：  
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), (Texture)anim[nowFram], ScaleMode.ScaleAndCrop);  
  
  
        //计算限制帧时间    
        time += Time.deltaTime;  
         //超过限制帧则切换图片   
        if(time >= 1.0 / fps){  
            //帧序列切换   
            nowFram++;  
            //限制帧清空   
            time = 0;  
            //超过帧动画总数从第0帧开始   
            if(nowFram >= mFrameCount)  
            {  
                nowFram = 0;  
            }  
        }   
    }  
}
