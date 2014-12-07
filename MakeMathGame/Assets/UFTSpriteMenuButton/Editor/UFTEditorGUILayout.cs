using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class UFTEditorGUILayout {
	
	private static float RECT_WIDTH  = 100.0f ;
	private static float RECT_HEIGH  = 130.0f ;
	private static float MARGIN      = 5.0f   ;
	private static float TEXT_HEIGHT = 15.0f  ;
	
	private static GUIStyle _boxStubStyle;
	private static GUIStyle boxStubStyle {
		get {
			if (_boxStubStyle == null){								
				_boxStubStyle = new GUIStyle(GUI.skin.GetStyle("Box"));				
				_boxStubStyle.alignment=TextAnchor.MiddleCenter;
				_boxStubStyle.fontSize=70;
			}			
			return _boxStubStyle;
		}		
	}
	
	private static GUIStyle _noneButonStyle;
	private static GUIStyle noneButonStyle {
		get {
			if (_noneButonStyle == null){								
				_noneButonStyle = new GUIStyle(GUI.skin.GetStyle("Label"));				
				_noneButonStyle.alignment = TextAnchor.MiddleCenter;				
			}			
			return _noneButonStyle;
		}		
	}
	
	private static GUIStyle _labelStyle;

	public static GUIStyle labelStyle {
		get {
			if (_labelStyle == null){
				_labelStyle = new GUIStyle( GUI.skin.GetStyle("Label"));
				_labelStyle.alignment=TextAnchor.MiddleLeft;				
			}
			return _labelStyle;
		}
		
	}	
	
	static Dictionary<string,UFTAtlasEntryMetadata> resultDict=new Dictionary<string,UFTAtlasEntryMetadata>();
	
	
	public delegate void OnUFTAtlasEntryClick(UFTAtlasMetadata atlasMetadata, UFTAtlasEntryMetadata entryMetadata, Vector2 parentWindowPosition, string parentWindowKey);
	
	
	
	
	public static UFTAtlasEntryMetadata atlasEntryMetadata(UFTAtlasMetadata      atlasMetadata, 
														  UFTAtlasEntryMetadata atlasEntryMetadata, 
														  int?                  parentWindowInstanceId = null){
		
		return renderAtlasEntryGUI(atlasMetadata,atlasEntryMetadata,showAtlasEntryChooseWindow, parentWindowInstanceId);
	}		
		
	/*
	 * Directly render atlasEntryMetadata, with custom delegate OnUFTAtlasEntryClick
	 */	
	public static UFTAtlasEntryMetadata renderAtlasEntryGUI(UFTAtlasMetadata    atlasMetadata, 
													      UFTAtlasEntryMetadata atlasEntryMetadata, 
														  OnUFTAtlasEntryClick  onUFTAtlasEntryClick, 
														  int?                  parentWindowInstanceId = null){
		
		Rect rt = GUILayoutUtility.GetRect(RECT_WIDTH,RECT_HEIGH);
		rt.width = RECT_WIDTH;	
		
		string controlKey=parentWindowInstanceId+rt.ToString();
		
		if (resultDict.ContainsKey(controlKey)){		
			atlasEntryMetadata=resultDict[controlKey];			
			resultDict.Remove(controlKey);			
		}
		
		
		Rect textureRect = new Rect(rt.x+MARGIN,
								rt.y + MARGIN,
								rt.width - (MARGIN * 2),
								rt.height - (MARGIN * 3) - TEXT_HEIGHT);
		
			
			
		Rect labelRect = new Rect( rt.x + MARGIN,
								rt.y + rt.height - TEXT_HEIGHT - MARGIN,
								rt.width - (MARGIN * 2),
								TEXT_HEIGHT);
		
			
		string tooltipText="";//"controlKey="+controlKey+"\n";
		
		if (atlasEntryMetadata !=null){
			tooltipText=tooltipText + atlasEntryMetadata.name      + "\n" +
							 atlasEntryMetadata.assetPath 		   + "\n" +
							 atlasEntryMetadata.pixelRect 		   + "\n" +
							 atlasEntryMetadata.uvRect    		   + "\n" +
							 "isTrimmed = "+atlasEntryMetadata.isTrimmed;				
		}
					
		
		if (GUI.Button(rt,new GUIContent("", tooltipText)) && onUFTAtlasEntryClick != null){
			Vector2 parentWindowPosition=Vector2.zero;
			if (parentWindowInstanceId!=null){
				EditorWindow window=(EditorWindow) EditorUtility.InstanceIDToObject((int)parentWindowInstanceId);
				parentWindowPosition=new Vector2(window.position.x,window.position.y);
			}
			 onUFTAtlasEntryClick(atlasMetadata,atlasEntryMetadata,parentWindowPosition, controlKey);				
		}
		
		if (atlasEntryMetadata != null){		
			
			float factor= atlasEntryMetadata.pixelRect.width / atlasEntryMetadata.pixelRect.height;			
			if (factor > 1){
				
				float newHeight=textureRect.height / factor;
				textureRect.y=textureRect.y + ((textureRect.height - newHeight) / 2);
				textureRect.height=newHeight;
			} else  {
				float newWidth= textureRect.width * factor;
				textureRect.x = textureRect.x + ((textureRect.width  - newWidth) / 2);
				textureRect.width = newWidth;		
				
			}		
			GUI.DrawTextureWithTexCoords(textureRect,atlasMetadata.texture,atlasEntryMetadata.uvRect);		
			GUI.Box(textureRect,GUIContent.none,UFTTextureUtil.borderStyle);
			
			GUI.Label(labelRect,atlasEntryMetadata.name,labelStyle);
		} else {
			
			GUI.Box(textureRect,new GUIContent("x","select atlasEntryMetadata from atlas"),boxStubStyle);			
			GUI.Label(labelRect, "(None)",noneButonStyle);
		}
		
		return atlasEntryMetadata;
	}
	
	
	
	
	
	private static void showAtlasEntryChooseWindow(UFTAtlasMetadata      atlasMetadata, 
												   UFTAtlasEntryMetadata entryMetadata, 
												   Vector2  			 parentWindowPosition,
												   string                controlKey){
		
		
		UFTSpriteChooseWindow window=UFTSpriteChooseWindow.Initialize(atlasMetadata, entryMetadata, parentWindowPosition, controlKey);
		window.onSpriteChooserClick+=onSpriteChooserClick;
	}

	private static void onSpriteChooserClick (UFTAtlasEntryMetadata atlasEntryMetadata, string parentControlKey)
	{		
		resultDict.Add(parentControlKey,atlasEntryMetadata);
	}
			
	
	

	
}
