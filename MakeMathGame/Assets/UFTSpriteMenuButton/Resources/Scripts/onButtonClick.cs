using UnityEngine;
using System.Collections;

public delegate void onButtonClick(UFTButton button);
public delegate void OnButtonHover(UFTButton button);

public class UFTButton : MonoBehaviour {
	public UFTAtlasMetadata atlasMetadata;
	public UFTAtlasEntryMetadata normalStateEntryMetadata;
	public UFTAtlasEntryMetadata onHoverStateEntryMetadata;
	public UFTAtlasEntryMetadata onClickStateEntryMetadata;
	
	public static onButtonClick onButtonClick;
	public static OnButtonHover onButtonHover;
	
	
	void Awake(){
		applyMetadata(normalStateEntryMetadata);
	}
	
	
	void OnMouseEnter(){
		if (onButtonHover != null)
			onButtonHover(this);
		applyMetadata(onHoverStateEntryMetadata);	
	}
	
	void OnMouseExit(){
		applyMetadata(normalStateEntryMetadata);	
	}
	
	void OnMouseDown(){
		if (onButtonClick != null)
			onButtonClick(this);
		applyMetadata(onClickStateEntryMetadata);	
	}
	
	void OnMouseUp(){
		applyMetadata(normalStateEntryMetadata);	
	}
	
	/*
	 *  triangles on plane
	 *	1,5 	4
	 * 	
	 *	0		2,3 
	 */
	
	public void applyMetadata(UFTAtlasEntryMetadata metadata){
		Mesh sharedMesh= renderer.GetComponent<MeshFilter>().sharedMesh;	
		Vector2[] uvs=sharedMesh.uv;
		Rect rect=metadata.uvRect;
		uvs[0]   =        new Vector2( rect.x             , rect.y);
		uvs[1] = uvs[5] = new Vector2( rect.x             , rect.y + rect.height);
		uvs[4]   =        new Vector2( rect.x + rect.width, rect.y + rect.height);
		uvs[2] = uvs[3] = new Vector2( rect.x + rect.width, rect.y);
		sharedMesh.uv=uvs;
	}
	
	
}
