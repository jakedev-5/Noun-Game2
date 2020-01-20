// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

#region Namespaces

	using UnityEngine;

	using System.Collections;
	using UnityEngine.UI;

#endregion

// ######################################################################
// This class handles News Canvas in "Demo 02 - Landscape - Home" and "Demo 02 - Portrait - Home" demo scenes.
// ######################################################################

public class Demo_News : Demo_GUIPanel {
	
	#region Variables
	
		// Texts
		public Text				m_NewsTitle				= null;
		public Text				m_NewsDetails			= null;

		// News text
		public string			m_NewsTitleText			= "";
		public string			m_NewsDetailsText		= "";

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
		}

		// Use this for initialization
		void Start ()
		{
			// Init Texts
			m_NewsTitle.text = m_NewsTitleText;			
			m_NewsDetails.text = m_NewsDetailsText.Replace("<br>", "\n");	// Replace <br> in details with new line 
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	
	#endregion //MonoBehaviour functions

	// ######################################################################
	// Respond functions
	// ######################################################################

	#region Respond functions

		public void Button_Close()
		{ 
			// Play Back button sound
			SoundController.Instance.Play_SoundBack();
	
			// Hide this panel
			Hide();
		}
	
	#endregion //Respond functions
}
