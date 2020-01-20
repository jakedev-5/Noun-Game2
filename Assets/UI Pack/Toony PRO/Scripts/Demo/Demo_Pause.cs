// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

using UnityEngine;

	using System.Collections;

	#region Namespaces
	using UnityEngine.UI;

#endregion

// ######################################################################
// This class handles Pause canvas in "Demo 02 - Landscape - Home", "Demo 02 - Portrait - Home" demo scenes, "Demo 04 - Landscape - Gameplay" and "Demo 04 - Portrait - Gameplay"
// ######################################################################
public class Demo_Pause : Demo_GUIPanel {
	
	#region Variables

		public Demo_Settings	m_Settings	= null;	// Keep Demo_Settings class to show it when user presses Settings button

	#endregion //Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Awake()
		{
			//////////////////////////////////////////////////////////////////////
			// If GEAnimSystem.Instance.m_AutoAnimation is false, all GEAnim elements in the scene will be controlled by scripts.
			// Otherwise, they will be animated automatically.
			//////////////////////////////////////////////////////////////////////
			if (enabled)
			{
				GEAnimSystem.Instance.m_GUISpeed = 4.0f;
				GEAnimSystem.Instance.m_AutoAnimation = false;
			}
		
			// If this class is not running on Unity Editor, the resolution will be change to 960x600px for Lanscape demo scene or 540x960px for Portrait demo scene
			if(Application.isEditor==false)
			{
				if(Application.platform==RuntimePlatform.WindowsPlayer || Application.platform==RuntimePlatform.OSXPlayer)
				{
					string CurrentLevel = Application.loadedLevelName;
					if(CurrentLevel.Contains("Landscape")==true)
						Screen.SetResolution(960, 600, false);
					else
						Screen.SetResolution(540, 960, false);
				}
			}

			// Activate all UI Canves GameObjects.
			if (m_Settings.gameObject.activeSelf == false)
				m_Settings.gameObject.SetActive(true);
		}

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{	
		}
	
	#endregion //MonoBehaviour functions

	// ######################################################################
	// Respond functions
	// ######################################################################

	#region Respond functions

		public void Button_Resume()
		{ 
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Hide this panel
			Hide();
		}

		public void Button_Settings()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Show this panel
			m_Settings.Show();
		}

		public void Button_Levels()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Resume everything
			Time.timeScale = 1.0f;

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);

			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 03 - " + OrientationName + " - Level Select", 1.0f);
		}

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		// Show this panel
		public void ShowMe()
		{
			// Pause everything
			Time.timeScale = 0.0f;

			// Show this panel
			this.gameObject.GetComponent<Demo_GUIPanel>().Show();
		}

		// Hide this panel
		public void HideMe()
		{
			// Resume everything
			Time.timeScale = 1.0f;

			// Hide this panel
			this.gameObject.GetComponent<Demo_GUIPanel>().Hide();
		}
	
	#endregion	//UI functions
}
