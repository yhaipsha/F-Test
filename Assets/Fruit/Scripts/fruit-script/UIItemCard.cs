using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("NGUI/Game/UI Item Card")]
public class UIItemCard : MonoBehaviour
{
    public int maxItemCount = 8;
    public int rows = 4;
    public int columns = 4;
    public int pages = 1;
    public GameObject template;
    public int spacing = 128;
    public int padding = 10;

    /****
     * 
     * 
     * 
     * ***/
    bool _isBounds = false;
    bool _firstSwitch = true;
    public string[] atlasSpriteNames;


    /// <summary>
    /// Initialize the container and create an appropriate number of UI slots.
    /// </summary>
    void Start()
    {


    }

    public void initLevel()
    {
        int __endNum = maxItemCount;
        _firstSwitch = true;

        for (int _currentPage = 1; _currentPage <= pages; _currentPage++)
        {

            __endNum = rows * columns * (_currentPage + 1) + 1;

            if (maxItemCount < __endNum)
            {
                __endNum = maxItemCount;
            }
            //从0开始计算
            //permutation((_currentPage - 1) * rows * columns, __endNum - 1, _currentPage);
            //从1开始计算
            permutation((_currentPage - 1) * rows * columns, __endNum , _currentPage);

        }
    }

    GameObject AddGameObject(string spriteName, int number)
    {
		print(spriteName);
        GameObject tempObj = (GameObject)Instantiate(template);
        tempObj.transform.parent = transform;
        tempObj.transform.localScale = new Vector3(1f, 1f, 1f);

        UILabel title = tempObj.transform.FindChild("LblTitle").GetComponent<UILabel>();
        title.text = number.ToString();

        if (spriteName == atlasSpriteNames[1])
        {
            return tempObj;
        }

        UIButtonTween bt = null;
        Transform target = Globe.getPanelOfParent(transform, 3, "Panel - GamePlay");
        // transform.parent.parent.parent.Find ("Panel - GamePlay").gameObject;

        if (spriteName != atlasSpriteNames[1])
        {
			#region 添加脚本 UIButtonTween

			/*
            bt = tempObj.AddComponent<UIButtonTween>();
            bt.tweenTarget = transform.parent.parent.gameObject;
            bt.includeChildren = true;
            bt.resetOnPlay = false;
            bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
            bt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterForward;
            bt.trigger = AnimationOrTween.Trigger.OnClick;
            bt.playDirection = AnimationOrTween.Direction.Forward;
            bt.eventReceiver = target.gameObject;
            bt.callWhenFinished = "OnLayer";

            bt = tempObj.AddComponent<UIButtonTween>();
            bt.tweenTarget = target.gameObject;
            bt.includeChildren = false;
            bt.resetOnPlay = false;
            bt.ifDisabledOnPlay = AnimationOrTween.EnableCondition.EnableThenPlay;
            bt.disableWhenFinished = AnimationOrTween.DisableCondition.DisableAfterReverse;
            bt.trigger = AnimationOrTween.Trigger.OnClick;
            bt.playDirection = AnimationOrTween.Direction.Forward;
			*/
			#endregion
			
            tempObj.AddComponent("SelectedLevel");
            
        }

        UISprite sp = tempObj.transform.FindChild("level1").GetComponent<UISprite>();
        sp.spriteName = spriteName;
        sp.MakePixelPerfect();


        return tempObj;
    }


    /// <summary>
    /// Permutation the specified _beginNumber, _endNumber and _page.
    /// </summary>
    /// <param name='_beginNumber'>
    /// _begin number.
    /// </param>
    /// <param name='_endNumber'>
    /// _end number.
    /// </param>
    /// <param name='_page'>
    /// _page.
    /// </param>
    int permutation(int _beginNumber, int _endNumber, int _page)
    {

        if (template != null)
        {
            GameObject objGo = null;

            int count = 0;
            Bounds b = new Bounds();
            string __modeHeader = Globe.Compare(PlayerPrefs.GetInt("NowMode"));


            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int _number = x + y * columns + _beginNumber + 1;
                    string tmp = __modeHeader + _number;

                    if ((PlayerPrefs.GetInt(tmp) == 0) && (_firstSwitch == false))
                    {

                        //objGo = Other ("level1", template, _number + 1);
                        objGo = AddGameObject(atlasSpriteNames[1], _number);
                    }
                    if ((PlayerPrefs.GetInt(tmp) == 0) && (_firstSwitch == true))
                    {
                        //第一次加载
                        //objGo = Other ("level0", template, _number + 1);
                        objGo = AddGameObject(atlasSpriteNames[0], _number);
                        _firstSwitch = false;
                    }
                    if (PlayerPrefs.GetInt(tmp) > 0)
                    {
                        switch (PlayerPrefs.GetInt(tmp))
                        {
                            case 1:
                                //objGo = Other ("level1-1", template, _number + 1);
                                objGo = AddGameObject(atlasSpriteNames[2], _number);
                                break;
                            case 2:
                                //objGo = Other ("level1-2", template, _number + 1);
                                objGo = AddGameObject(atlasSpriteNames[3], _number);
                                break;
                            case 3:
                                //objGo = Other ("level1-3", template, _number + 1);
                                objGo = AddGameObject(atlasSpriteNames[4], _number);
                                break;
//                            default:
//                                objGo = AddGameObject(atlasSpriteNames[0], _number);
//                                break;
                        }
                    }


                    //GameObject go = NGUITools.AddChild (gameObject, objGo);


                    Transform t = objGo.transform;
                    t.localPosition = new Vector3((padding + (x + 0.5f) * spacing) + (_page - 1) * columns * spacing, -padding - (y + 0.5f) * spacing, 0f);


                    b.Encapsulate(new Vector3(padding * 2f + (x + 1) * spacing, -padding * 2f - (y + 1) * spacing, 0f));

                    if (++count >= maxItemCount)
                    {
                        return 0;
                    }

                    if (_number == _endNumber)
                    {
                        return 0;
                    }
                }

            }

            return _page;

        }
        return 0;
    }

    public void cleaner()
    {
        for (int i = 0; i < transform.GetChildCount(); i++)
        {
            Destroy(transform.GetChild(i).gameObject, 1.0f);
        }
    }

    public void UpdateArrange()
    {
        NGUITools.SetActive(gameObject, true);
    }

    /// <summary>
    /// 已过时
    /// </summary>
    /// <param name='name'>
    /// Name.
    /// </param>
    /// <param name='tempObj'>
    /// Temp object.
    /// </param>
    /// <param name='number'>
    /// Number.
    /// </param>
    GameObject Other(string name, GameObject tempObj, int number)
    {
        GameObject go = tempObj;
        //		GameObject level1, other;
        //		level1 = other = null;

        UILabel title = tempObj.GetComponentInChildren<UILabel>();
        title.text = number.ToString();

        foreach (UISprite item in tempObj.GetComponentsInChildren<UISprite>())
        {
            if (name != item.name)
            {
                item.enabled = false;
            }
            else
            {
                item.enabled = true;
            }
            if (name == "level1")
            {

                foreach (UIButtonTween bt in item.transform.parent.GetComponentsInChildren<UIButtonTween>())
                {
                    bt.enabled = false;
                }
            }
        }

        return tempObj;
    }

    void OnBecameInvisible()
    {
        /*foreach (UISprite item in transform.GetComponentsInChildren<UISprite>()) {
            if (name != item.name) {
                item.enabled = false;	
            } else {				
                item.enabled = true;					
            }
            if (name == "level1") {
				
                foreach (UIButtonTween bt in item.transform.parent.GetComponentsInChildren<UIButtonTween>()) {
                    bt.enabled = false;					
                }					
            }			
        }*/
    }

    void LateUpdate()
    {

    }
    /// <summary>
    /// Transes for plane. DOESN'T USE
    /// </summary>
    /// <param name='trans'>
    /// Trans.
    /// </param>
    /// <param name='BeforeSize'>
    /// Before size.
    /// </param>
    /// <param name='AfterSize'>
    /// After size.
    /// </param>
    public void TransForPlane(Transform trans, Vector3 BeforeSize, Vector3 AfterSize)
    {
        Vector3 v = Camera.mainCamera.WorldToScreenPoint(trans.position + BeforeSize);

        if (v.y <= 0)
        {
            v.y = 0;
            trans.position = Camera.mainCamera.ScreenToWorldPoint(v) + AfterSize;
        }
        else if (v.y >= Screen.height)
        {
            v.y = Screen.height;
            trans.position = Camera.mainCamera.ScreenToWorldPoint(v) + AfterSize;
        }


        if (v.x <= 0)
        {
            v.x = 0;
            trans.position = Camera.mainCamera.ScreenToWorldPoint(v) + AfterSize;
        }
        else if (v.x >= Screen.width)
        {
            v.x = Screen.width;
            trans.position = Camera.mainCamera.ScreenToWorldPoint(v) + AfterSize;
        }
    }
}