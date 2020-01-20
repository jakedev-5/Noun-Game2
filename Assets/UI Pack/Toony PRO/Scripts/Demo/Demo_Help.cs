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

// Help texts class describes title and details of each help.
[System.Serializable]
public class HelpText
{
	public string Title;
	public string Details;
}

// ######################################################################
// This class contains HelpText array to display helps in Helps panel.
// ######################################################################

public class Demo_Help : Demo_GUIPanel {
	
	#region Variables

		// Texts
		public Text				m_HelpTitle				= null;
		public Text				m_HelpDetails			= null;

		// Help texts
		public HelpText[]		m_HelpText;

		// status
		private int				m_ShowingHelpTextIndex	= 0;

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
			// Set Help index to first one.
			m_ShowingHelpTextIndex = 0;

			// Replace <br> in details with new line 
			m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");			// Replace <br> in details with new line 
			m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");		// Replace <br> in details with new line 
		}
	
		// Update is called once per frame
		void Update ()
		{	
		}
	
	#endregion // MonoBehaviour functions

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

		public void Button_Left()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Decrease index
			m_ShowingHelpTextIndex--;
			if(m_ShowingHelpTextIndex<0)
				m_ShowingHelpTextIndex = m_HelpText.Length-1;

			// Update Title and Deatils according to current index
			m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");			// Replace <br> in details with new line 
			m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");		// Replace <br> in details with new line 
		}

		public void Button_Right()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
			
			// Increase index
			m_ShowingHelpTextIndex++;
			if(m_ShowingHelpTextIndex>=m_HelpText.Length)
				m_ShowingHelpTextIndex = 0;
			
			// Update Title and Deatils according to current index
			m_HelpTitle.text = m_HelpText[m_ShowingHelpTextIndex].Title.Replace("<br>", "\n");			// Replace <br> in details with new line 
			m_HelpDetails.text = m_HelpText[m_ShowingHelpTextIndex].Details.Replace("<br>", "\n");		// Replace <br> in details with new line 
		}

	#endregion //Respond functions
}
