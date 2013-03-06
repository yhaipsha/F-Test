using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class ReverseNormals : MonoBehaviour
{

	void Start ()
	{
		MeshFilter filter = GetComponent (typeof(MeshFilter)) as MeshFilter;
		if (filter != null) {
			Mesh mesh = filter.mesh;
            
			Vector3[] normals = mesh.normals;
			for (int i=0; i<normals.Length; i++)
				normals [i] = -normals [i];
			mesh.normals = normals;
            
			for (int m=0; m<mesh.subMeshCount; m++) {
				int[] triangles = mesh.GetTriangles (m);
				for (int i=0; i<triangles.Length; i+=3) {
					int temp = triangles [i + 0];
					triangles [i + 0] = triangles [i + 1];
					triangles [i + 1] = temp;
				}
				mesh.SetTriangles (triangles, m);
			}
		}
		
		
		
		
	}
	
	private Texture2D MirPic (string path, string textureName)
	{ 

		Texture2D texture2d = (Texture2D)Resources.Load (path + "/" + textureName, typeof(Texture2D)); //获取原图片 

		int width = texture2d.width;  //得到图片的宽度.   

		int height = texture2d.height;//得到图片的高度 

		Texture2D NewTexture2d = new Texture2D (width, height);//创建一张同等大小的空白图片 

		int i_start = 0; 

		while (i_start  <width) {//如果是垂直翻转的话将width  换成 height 
			i_start++; 
			NewTexture2d.SetPixels (i_start, 0, 1, height, texture2d.GetPixels (width - i_start - 1, 0, 1, height)); 
		} 
		NewTexture2d.Apply (); 

		return NewTexture2d; 
	}
	
}
