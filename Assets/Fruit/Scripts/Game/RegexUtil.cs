using System.Text.RegularExpressions;

public class RegexUtil
{

	/// <summary>  
	/// 去掉字符串中的数字  
	/// </summary>  
	/// <param name="key"></param>  
	/// <returns></returns>  
	public static string RemoveNumber (string key)
	{  
		return Regex.Replace (key, @"\d", "");  
	}       
	
	/// <summary>  
	/// 去掉字符串中的非数字  
	/// </summary>  
	/// <param name="key"></param>  
	/// <returns></returns>  
	public static string RemoveNotNumber (string key)
	{  
		return Regex.Replace (key, @"[^\d]*", "");  
	}
}
