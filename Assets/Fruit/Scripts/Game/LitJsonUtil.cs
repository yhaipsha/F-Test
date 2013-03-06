using System.IO;
using System.Text;
using UnityEngine;
using System.Collections;
using LitJson;

public class LitJsonUtil
{
	public static string json_path = Globe.jsonURL.Replace ("file://", "");
	
	public static string show ()
	{		
		JsonData data = new JsonData ();
		data ["name"] = "peiandsky";
		data ["age"] = 28;
		data ["sex"] = "male";
		string json1 = data.ToJson ();
		return json1;
	}

	public static string readAll (int jsonLable)
	{
		FileStream fs = File.OpenRead (json_path);
		byte[] data = new byte[fs.Length];
		fs.Read (data, 0, data.Length);
		string json_text = System.Text.Encoding.Default.GetString (data);
		JsonReader jr = new JsonReader (json_text);

		JsonData root = JsonMapper.ToObject (json_text);
		JsonData lf = root ["level"];

		return lf [jsonLable].ToJson ();
	}

	public static void addNextLevel (int lastLevelStar, int jsonLable)
	{
		FileStream fs = File.OpenRead (json_path);
		byte[] data = new byte[fs.Length];
		fs.Read (data, 0, data.Length);
		
		StringBuilder sb_jsontxt = new StringBuilder ();
		sb_jsontxt.Append (System.Text.Encoding.Default.GetString (data));
		
		JsonData root = JsonMapper.ToObject (sb_jsontxt.ToString ());
		sb_jsontxt.Remove (0, sb_jsontxt.Length);
		JsonData star = root ["level"] [jsonLable];
//		sb_jsontxt.Replace(star.ToJson(),"");
		star.Add (lastLevelStar);
		root ["level"] [jsonLable] = star;
		sb_jsontxt.Append (root.ToJson ());
		
//		return sb_jsontxt.ToString();
		
		JsonWriter writer = new JsonWriter (sb_jsontxt); 
//		writer.WriteArrayStart (); 
//		writer.Write (1); writer.Write (2); writer.Write (3); 
//		writer.WriteObjectStart (); 
//		writer.WritePropertyName ("color"); 
//		writer.Write ("blue"); 
//		writer.WriteObjectEnd (); 
//		writer.WriteArrayEnd (); 
//		writer.Write (sb_jsontxt.ToString ());

//		Console.WriteLine (sb.ToString ()); // [1,2,3,{"color":"blue"}]
		
		
		WriteFile (WriteJson ());
		
	}
	
	public static string WriteJson ()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		JsonWriter writer = new JsonWriter (sb);
		writer.WriteObjectStart ();
		writer.WritePropertyName ("level");
		
		writer.WriteObjectStart ();
		writer.WritePropertyName ("first");
		writer.WriteArrayStart ();		
		writer.Write (1);
		writer.Write (2);
		writer.Write (3);
		writer.WriteArrayEnd ();
				
		
		writer.WritePropertyName ("second");
		writer.WriteArrayStart ();		
		writer.Write (1);
		writer.Write (2);
		writer.WriteArrayEnd ();
		writer.WriteObjectEnd ();
		
		writer.WriteObjectEnd ();
		
		return sb.ToString ();
//		Console.WriteLine (sb.ToString ());

	}

	static void WriteFile (string file_content)
	{
		FileInfo t = new FileInfo (json_path);
//		if (File.Exists (json_path)) {
//			File.Delete (json_path);
//		}
		
		using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(file_content))) {
			
//			XmlSerializer progressSerializer = new XmlSerializer (typeof(ProgressObject));
//			progressSerializer.Serialize (stream, ProgressStore);
			using (FileStream fileWriter = File.Create(json_path)) {
				byte[] originalData = stream.GetBuffer ();
				fileWriter.Write (originalData, 0, originalData.Length);
				fileWriter.Close ();
			}
		}
		
//		StreamWriter sw = new StreamWriter (json_path);
//		sw.Write (file_content);
//		sw.Close ();
//		sw.Dispose ();
	}
	
	/// <summary> 读取文件，并返回ArrayList集合 </summary>  
/// <param name="path"> 读取文件的路径,路径之间用//隔开 </param>  
/// <param name="name"> 读取文件名，包括扩展名如.txt </param>  
	ArrayList LoadFile (string path, string name)
	{  
		ArrayList arrList = new ArrayList ();  
		//使用流读取  
		StreamReader sr = null;  
		string line;  
  
		try {  
			sr = File.OpenText (path + name);  
		} catch (System.Exception ex) {  
			ex.ToString ();  
			return null;  
		}  
		while ((line = sr.ReadLine()) != null) {  
			arrList.Add (line);  
		}  
		//关闭流  
		sr.Close ();  
		//销毁流  
		sr.Dispose ();  
		//返回数组链表容器  
		return arrList;  
	}  
  
/// <summary> 修改文件，如果成功返回true </summary>  
/// <param name="path"> 修改文件的路径,路径之间用//隔开 </param>  
/// <param name="name"> 修改文件名，包括扩展名如.txt </param>  
/// <param name="strList">根据strList来修改</param>  
	bool WriteFile (string path, string name, ArrayList sourceList)
	{  
		//使用流读取  
		FileStream fs;  
		StreamWriter sw;  
		try {  
			fs = new FileStream (path + name, FileMode.Create);  
			sw = new StreamWriter (fs);  
		} catch (System.Exception ex) {  
			ex.ToString ();  
			return false;  
		}  
		foreach (string line in sourceList) {  
			sw.WriteLine (line);  
		}  
		//关闭流  
		sw.Flush ();  
		sw.Close ();  
		fs.Close ();  
		//销毁流  
		return true;  
	}

	static public bool JsonDataContainsKey (JsonData data, string key)
	{
		//if (data is IDictionary) {
		//         writer.WriteObjectStart ();
		//         foreach (DictionaryEntry entry in (IDictionary) obj) {
		//            writer.WritePropertyName ((string) entry.Key);
		//               if (entry.Key is DateTime)
		//                   writer.WritePropertyName ( entry.Key.ToString() );
		//            else
		//                   writer.WritePropertyName ((string) entry.Key);

		//             WriteValue (entry.Value, writer, writer_is_private,
		//                         depth + 1);
		//         }

		//-----
		bool result = false;
		if (data == null)
			return result;
		if (!data.IsObject) {
			return result;
		}
		IDictionary tdictionary = data as IDictionary;
		if (tdictionary == null)
			return result;
		if (tdictionary.Contains (key)) {
			result = true;
		}
		return result;
	}
	/// <summary>
	/// byte[]流转十六进制字符串 0x34---->"34"
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	public static string ToHexString (byte[] bytes)
	{
		char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
		char[] chars = new char[bytes.Length * 2];
		for (int i = 0; i < bytes.Length; i++) {
			int b = bytes [i];//十六进制转化为十进制 0x34->52
			chars [i * 2] = hexDigits [b >> 4];
			chars [i * 2 + 1] = hexDigits [b & 0xF];
		}
		return new string (chars);
	}

	/// <summary>
	///  字符串转byte    "34"---->52 (0x34)
	/// </summary>
	/// <param name="strAsccii"></param>
	/// <returns></returns>
	public static byte ConvertAscciiToByte (string strAsccii)
	{
		strAsccii = strAsccii.Trim ();
		if (strAsccii.Length > 2 || strAsccii.Length < 1)
			return 0x00;
		return byte.Parse (strAsccii, System.Globalization.NumberStyles.AllowHexSpecifier);
	}
}
