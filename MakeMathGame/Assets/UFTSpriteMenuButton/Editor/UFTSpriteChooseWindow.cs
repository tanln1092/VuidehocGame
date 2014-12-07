using UnityEngine;
using System.Collections;
using UnityEditor;

public delegate void OnSpriteChooserClick(UFTAtlasEntryMetadata atlasEntryMetadata, string parentControlKey);

public class UFTSpriteChooseWindow : EditorWindow {
	UFTAtlasMetadata atlasMetadata;
	UFTAtlasEntryMetadata atlasEntryMetadata;
	public OnSpriteChooserClick onSpriteChooserClick;
	
	string parentControlKey;
	
	
	Vector2 scrollPosition=Vector2.zero;
	bool checkScrollPosition=true;
	
	
	public static UFTSpriteChooseWindow Initialize(UFTAtlasMetadata atlasMetadata, UFTAtlasEntryMetadata atlasEntryMetadata, Vector2 parentWindowPosition, string parentControlKey){
		UFTSpriteChooseWindow window=ScriptableObject.CreateInstance<UFTSpriteChooseWindow>();
		window.atlasMetadata=atlasMetadata;
		window.atlasEntryMetadata=atlasEntryMetadata;		
		window.parentControlKey=parentControlKey;
		
		Rect position=new Rect(Event.current.mousePosition.x + parentWindowPosition.x -30,
							   parentWindowPosition.y-200,
							   250,
							   50);
		window.position=position;
		//window.ShowAsDropDown(window.position,new Vector2(120,500)); //here is i'm getting System.Collections.Stack.Peek exception
		window.Show();//  It works well
		return window;
	}
	
	
	void OnGUI () {			
		showAllSprites ();			
	}
	
	void showAllSprites ()
	{
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
		
		for (int i = 0; i < atlasMetadata.entries.Length; i++) {
			UFTEditorGUILayout.renderAtlasEntryGUI(atlasMetadata,atlasMetadata.entries[i], onAtlasClick);	
			
			if (checkScrollPosition && Event.current.type == EventType.Repaint){
				if (atlasEntryMetadata!=null && atlasEntryMetadata.Equals(atlasMetadata.entries[i])){
	
					scrollPosition.y= GUILayoutUtility.GetLastRect().y;
					checkScrollPosition=false;
				}
				
			}
		}
				
		EditorGUILayout.EndScrollView();		
	}
	
	
	private void onAtlasClick(UFTAtlasMetadata       atlasMetadata, 
							  UFTAtlasEntryMetadata  entryMetadata, 
							  Vector2				 parentWindowPosition,
							  string                 controlKey){
		if (onSpriteChooserClick!=null)
			onSpriteChooserClick(entryMetadata,parentControlKey);
		
		Close();
	}
	
	
	private void OnLostFocus(){
		Focus();
	}
	
	
}
