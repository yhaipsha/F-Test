
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections;

namespace Sheep.para
{
	public class SysData
	{
		public SysData ()
		{
            item = new ArrayList();
		}
		
		public SysData (int id, float volume)
		{
			this.Volume = volume;
//			this.Colour = color;
			this.Number = id;
            item = new ArrayList();
		}

        [XmlElement("SysName")]
        public string SysName { set; get; }
		/// <summary>
		/// Gets or sets the Number
		/// </summary>
		/// <value>
		/// 序号这里的属性写在root下
		/// </value>
        //[XmlAttribute("Number")]
		public int Number {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the color array.
		/// </summary>
		/// <value>
		/// 颜色数组
		/// </value>
		//public Colour[] ColorArray{ set; get; }
		/// <summary>
		/// Gets or sets the color list.
		/// </summary>
		/// <value>
		/// 颜色列表
		/// </value>
        //[XmlElement("Color")]
        public ArrayList ColorList
        {
            get { return item; }
            set { item = value; }
        }
        private ArrayList item;
		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		/// <value>
		/// 设置系统音量
		/// </value>
		public float Volume {
			get;
			set;
		}
		
		public DemoData _iUser;
        
        public struct DemoData
        {
            public float x;
            public float y;
            public float z;
            public string name;
        } 
		
	}
}