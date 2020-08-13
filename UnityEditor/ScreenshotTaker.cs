//C# Example
using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class Screenshot : EditorWindow
{

	public Camera myCamera;
	int scale = 1;

	string path = "";
	bool showPreview = true;
	RenderTexture renderTexture;

	bool isTransparent = false;

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Tools/Screenshot/Instant High-Res Screenshot")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow editorWindow = EditorWindow.GetWindow(typeof(Screenshot));
		editorWindow.autoRepaintOnSceneChange = true;
		editorWindow.Show();
		editorWindow.title = "Screenshot";
	}

	float lastTime;


	void OnGUI()
	{
		EditorGUILayout.LabelField ("Resolution", EditorStyles.boldLabel);

		EditorGUILayout.Space();

		scale = EditorGUILayout.IntSlider ("Scale", scale, 1, 15);

		EditorGUILayout.HelpBox("The default mode of screenshot is crop - so choose a proper width and height. The scale is a factor " +
			"to multiply or enlarge the renders without loosing quality.",MessageType.None);

		
		EditorGUILayout.Space();
		
		
		GUILayout.Label ("Save Path", EditorStyles.boldLabel);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.TextField(path,GUILayout.ExpandWidth(false));
		if(GUILayout.Button("Browse",GUILayout.ExpandWidth(false)))
			path = EditorUtility.SaveFolderPanel("Path to Save Images",path,Application.dataPath);

		EditorGUILayout.EndHorizontal();

		EditorGUILayout.HelpBox("Choose the folder in which to save the screenshots ",MessageType.None);
		EditorGUILayout.Space();



		//isTransparent = EditorGUILayout.Toggle(isTransparent,"Transparent Background");



		GUILayout.Label ("Select Camera", EditorStyles.boldLabel);


		myCamera = EditorGUILayout.ObjectField(myCamera, typeof(Camera), true,null) as Camera;


		if(myCamera == null)
		{
			myCamera = Camera.main;
		}

		isTransparent = EditorGUILayout.Toggle("Transparent Background", isTransparent);


		EditorGUILayout.HelpBox("Choose the camera of which to capture the render. You can make the background transparent using the transparency option.",MessageType.None);

		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField ("Default Options", EditorStyles.boldLabel);




		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();

		if(GUILayout.Button("Take Screenshot",GUILayout.MinHeight(60)))
		{
			if(path == "")
			{
				path = EditorUtility.SaveFolderPanel("Path to Save Images",path,Application.dataPath);
				Debug.Log("Path Set");
				TakeHiResShot();
			}
			else
			{
				TakeHiResShot();
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Open Last Screenshot",GUILayout.MaxWidth(160),GUILayout.MinHeight(40)))
		{
			if(lastScreenshot != "")
			{
				Application.OpenURL("file://" + lastScreenshot);
				Debug.Log("Opening File " + lastScreenshot);
			}
		}

		if(GUILayout.Button("Open Folder",GUILayout.MaxWidth(100),GUILayout.MinHeight(40)))
		{

			Application.OpenURL("file://" + path);
		}

		EditorGUILayout.EndHorizontal();


		if (takeHiResShot) 
		{
			string filename = ScreenShotName();
			ScreenCapture.CaptureScreenshot(ScreenShotName());
			
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
			Application.OpenURL(filename);
			takeHiResShot = false;
		}
	}


	
	private bool takeHiResShot = false;
	public string lastScreenshot = "";
	
		
	public string ScreenShotName() {

		string strPath="";

		strPath = string.Format("{0}/screen_{1}.png", 
		                     path, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
		lastScreenshot = strPath;
	
		return strPath;
	}



	public void TakeHiResShot() {
		Debug.Log("Taking Screenshot");
		takeHiResShot = true;
	}

}

