using UnityEngine;
using System.Collections;
using UnityEditor;

public class UFTButtonEditor : EditorWindow {
	private UFTAtlasMetadata atlasMetadata;
	Material buttonMaterial;
	string buttonName;
	
	private UFTAtlasEntryMetadata normalStateEntryMetadata;
	private UFTAtlasEntryMetadata onHoverStateEntryMetadata;
	private UFTAtlasEntryMetadata onClickStateEntryMetadata;
			
	private int width;
	private int height;
	private bool isAllTexturesHasSameSize=false;
	
	private static string PLAYERPREFS_ATLASMETA_KEY="uft3layerbutton.atlasmeta";
	private static string PLAYERPREFS_BUTTON_MATERIAL="uft3layerbutton.material";
			
	[MenuItem ("Window/UFT New Button")]
    static void ShowWindow () {    		
		UFTButtonEditor window= EditorWindow.GetWindow <UFTButtonEditor>("UFT New Button");
		window.Initialize();
    }
	
	public void Initialize(){
		
		string atlasAssetPath=EditorPrefs.GetString(PLAYERPREFS_ATLASMETA_KEY,null);
		if (atlasAssetPath !=null){
			atlasMetadata = (UFTAtlasMetadata) AssetDatabase.LoadAssetAtPath(atlasAssetPath,typeof(UFTAtlasMetadata));
		}
		string buttonMaterialAssetPath=EditorPrefs.GetString(PLAYERPREFS_BUTTON_MATERIAL,null);
		if (buttonMaterialAssetPath != null){
			buttonMaterial=	 (Material) AssetDatabase.LoadAssetAtPath(buttonMaterialAssetPath,typeof(Material));
		}		
	}

	void setParameter (UnityEngine.Object objectRef, string key)
	{
		string atlasAssetPath=EditorPrefs.GetString(key,null);
		if (atlasAssetPath!=null){
			UnityEngine.Object objectFromPrefs=(UFTAtlasMetadata) AssetDatabase.LoadAssetAtPath(atlasAssetPath,typeof(UFTAtlasMetadata));	
			if (objectFromPrefs!=null){
				objectRef=objectFromPrefs;	
			} else {
				EditorPrefs.DeleteKey(key);	
			}
		}
	}

	
	void OnGUI(){		
		
		
		UFTAtlasMetadata newAtlasMetadata = (UFTAtlasMetadata) EditorGUILayout.ObjectField(atlasMetadata,typeof(UFTAtlasMetadata),false);		
		if (newAtlasMetadata !=atlasMetadata){
			atlasMetadata=newAtlasMetadata;
			EditorPrefs.SetString(PLAYERPREFS_ATLASMETA_KEY,AssetDatabase.GetAssetPath(atlasMetadata));
		}	
		
		
		GUI.enabled = (atlasMetadata != null);
		EditorGUILayout.BeginHorizontal();
		showMetadataGui(ref normalStateEntryMetadata,"Normal State");
		showMetadataGui(ref onHoverStateEntryMetadata,"On Hover");
		showMetadataGui(ref onClickStateEntryMetadata,"On Click");		
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Toggle("all textures has the same size",isAllTexturesHasSameSize);
		width = EditorGUILayout.IntField("width", width);
		height = EditorGUILayout.IntField("height", height);
		
		Material newButtonMaterial=(Material) EditorGUILayout.ObjectField(buttonMaterial,typeof(Material),false);
		if (newButtonMaterial != buttonMaterial){
			Debug.Log("save material");
			buttonMaterial=newButtonMaterial;
			EditorPrefs.SetString(PLAYERPREFS_BUTTON_MATERIAL,AssetDatabase.GetAssetPath(buttonMaterial));
		}	
		
		buttonName=EditorGUILayout.TextField("button name",buttonName);		
		
		GUI.enabled = isAllMandatoryOptionSet();
		Rect buttonRect=EditorGUILayout.BeginHorizontal("Button");
		if ( GUI.Button(buttonRect, GUIContent.none) ){
			createButton();
		}		
		GUILayout.Label ("Create Button");
		EditorGUILayout.EndHorizontal();
		
	}
	
	private bool isAllMandatoryOptionSet(){
		return atlasMetadata  !=null &&
			   buttonName     !=null &&
			   buttonMaterial !=null;			  
	}	

	void createButton ()
	{
	 	GameObject go = UFTMeshUtil.createPlane(width,height);
		go.AddComponent<MeshCollider>();
		UFTButton uftButton= go.AddComponent<UFTButton>();
		uftButton.atlasMetadata=atlasMetadata;
		uftButton.normalStateEntryMetadata=normalStateEntryMetadata;
		uftButton.onClickStateEntryMetadata=onClickStateEntryMetadata;
		uftButton.onHoverStateEntryMetadata=onHoverStateEntryMetadata;
		uftButton.applyMetadata(uftButton.normalStateEntryMetadata);
		go.renderer.material=buttonMaterial;
		go.name=buttonName;
		AssetDatabase.CreateAsset(go.GetComponent<MeshFilter>().sharedMesh, AssetDatabase.GenerateUniqueAssetPath("Assets/"+buttonName+".asset") );
        AssetDatabase.SaveAssets();
		Close();
	}
	
	
	private bool isAllStateHasSameSize(){
		return normalStateEntryMetadata !=null &&  
				onHoverStateEntryMetadata !=null &&
				onClickStateEntryMetadata !=null &&
				normalStateEntryMetadata.pixelRect.width == onHoverStateEntryMetadata.pixelRect.width &&
				onHoverStateEntryMetadata.pixelRect.width == onClickStateEntryMetadata.pixelRect.width &&
				normalStateEntryMetadata.pixelRect.height == onHoverStateEntryMetadata.pixelRect.height &&
				onHoverStateEntryMetadata.pixelRect.height == onClickStateEntryMetadata.pixelRect.height; 
	}
	
	private void showMetadataGui(ref UFTAtlasEntryMetadata atlasEntryMeta, string caption){
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField(caption);
		UFTAtlasEntryMetadata newAtlasEntryMeta = UFTEditorGUILayout.atlasEntryMetadata(atlasMetadata,atlasEntryMeta,this.GetInstanceID());		
		if (newAtlasEntryMeta !=atlasEntryMeta ){
			atlasEntryMeta=newAtlasEntryMeta;
			width=(int) atlasEntryMeta.pixelRect.width;
			height=(int) atlasEntryMeta.pixelRect.height;
			buttonName=atlasEntryMeta.name;
			isAllTexturesHasSameSize=isAllStateHasSameSize();
		}
		EditorGUILayout.EndVertical();
	}
}
