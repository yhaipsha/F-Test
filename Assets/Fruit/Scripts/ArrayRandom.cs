using System.Collections;
using System;

public class ArrayRandom
{
	
	public ArrayRandom (int size)
	{
		arrNum = new int[size];
	}
	
	int[] arrNum = null;//new int[10];
	int tmp = 0;

	public int[] NonRepeatArray (int minValue, int maxValue)
	{
		Random ra = new Random (unchecked((int)DateTime.Now.Ticks));
		for (int i=0; i<arrNum.Length; i++) { 
			tmp = ra.Next (minValue, maxValue); //随机取数 
			arrNum [i] = getNum (arrNum, tmp, minValue, maxValue, ra); //取出值赋到数组中 
		} 
		return arrNum;
	}
	
	int getNum (int[] arrNum, int tmp, int minValue, int maxValue, Random ra)
	{ 
		int n = 0; 
		while (n<=arrNum.Length-1) { 
			if (arrNum [n] == tmp) { //利用循环判断是否有重复 
				tmp = ra.Next (minValue, maxValue); //重新随机获取。 
				getNum (arrNum, tmp, minValue, maxValue, ra);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。 
			} 
			n++; 
		} 
		return tmp; 
	}
	
	/// <summary>
	///  随机排列数组中的数值顺序
	///  技巧：顺次把数组中的数值和数组随机值置换位置
	/// </summary>
	/// <param name="strarr">被排列的数组strarr</param>
	public static void FillRandomArray  (ref string[] strarr)
	{
		Random ran = new Random ();
		int k = 0;
		string strtmp = "";
		int MaxLength = strarr.Length;
		for (int i = 0; i < strarr.Length; i++) { 
			// k = ran.Next(0, 7);  
			k = ran.Next (MaxLength);
			if (k != i) {
				strtmp = strarr [i];
				strarr [i] = strarr [k];
				strarr [k] = strtmp;
			}
		}
	}
	
}
