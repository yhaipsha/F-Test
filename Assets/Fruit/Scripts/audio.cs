using UnityEngine;
using System.Collections;

public class audio : MonoBehaviour {

	//音乐文件  
    public AudioSource music;     
    //音量  
    public float musicVolume;     
	
	UILabel mWidget;
	UISlider vSlider;
    
    void Start() {  
        //设置默认音量  
		
		PlayerPrefs.GetFloat("voice");
        musicVolume = 0.5F;     
		if(music != null)
		{
			vSlider = music.transform.GetComponent<UISlider>();
			vSlider.sliderValue = musicVolume;	
			
			if(vSlider.name == "Slider-music")
				music.bypassEffects=true;
			
//			Debug.Log("?"+vSlider.name);
		}
		
    }
	void OnClick()
	{
		if(!music.isPlaying){
			music.Play();
		}else if(music.isPlaying)
		{
//			music.Stop();
			music.Pause();			
		}		
	}
	void OnPress()
	{
		/*if(vSlider == null){
			vSlider = GameObject.Find("Slider").GetComponent<UISlider>();
			vSlider.sliderValue = musicVolume;
//			vSlider.onValueChange +=Slider
		}*/
		if(music != null)
		{
			vSlider = music.transform.GetComponent<UISlider>();
			vSlider.sliderValue = musicVolume;	
		}
		
		Debug.Log("press"+ vSlider.name );		
	}
	void OnSliderChange (float val)
	{
		if (music.isPlaying){  
            //音乐播放中设置音乐音量 取值范围 0.0F到 1.0   
            music.volume = val;  
        }  
		
			//if(vSlider.name == "vSlider-sound")
			
		//Debug.Log("in the slider :"+music.volume);
	}
	void OnBecameInvisible()
	{
		Debug.Log("vvvd");
	}
	void OnDisable() {
//		print("script was removed");
	}
//    void OnGUI()
	void ShowIt()
	{  
          
        //播放音乐按钮  
        if (GUI.Button(new Rect(10, 10, 100, 50), "Play music"))  {  
              
            //没有播放中  
            if (!music.isPlaying){  
                //播放音乐  
                music.Play();  
            }  
              
        }  
          
        //关闭音乐按钮  
        if (GUI.Button(new Rect(10, 60, 100, 50), "Stop music"))  {  
              
            if (music.isPlaying){  
                //关闭音乐  
                music.Stop();  
            }  
        }  
        //暂停音乐  
        if (GUI.Button(new Rect(10, 110, 100, 50), "Pause music"))  {  
            if (music.isPlaying){  
                //暂停音乐  
                //这里说一下音乐暂停以后  
                //点击播放音乐为继续播放  
                //而停止以后在点击播放音乐  
                //则为从新播放  
                //这就是暂停与停止的区别  
                music.Pause();  
            }  
        }  
  
        //创建一个横向滑动条用于动态修改音乐音量  
        //第一个参数 滑动条范围  
        //第二个参数 初始滑块位置  
        //第三个参数 起点  
        //第四个参数 终点  
        musicVolume = GUI.HorizontalSlider (new Rect(160, 10, 100, 50), musicVolume, 0.0F, 1.0F);  
      
        //将音量的百分比打印出来  
        GUI.Label(new Rect(160, 50, 300, 20), "Music Volueme is " + (int)(musicVolume * 100) + "%");  
          
        if (music.isPlaying){  
            //音乐播放中设置音乐音量 取值范围 0.0F到 1.0   
            music.volume = musicVolume;  
        }  
    }  
	
	
}
