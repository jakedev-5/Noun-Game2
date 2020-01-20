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
// This class handles "Demo 07 - Landscape - Social" and "Demo 07 - Portrait - Social"
// ######################################################################

public class Demo07_Social : MonoBehaviour {
	
	#region Variables

		// Tabs
		public Image			m_ImageButtonFriends	= null;
		public Image			m_ImageButtonGifts		= null;

		// Texts
		public Text				m_TextFriendDetails		= null;
		public Text				m_TextButtonFriends		= null;
		public Text				m_TextButtonGifts		= null;
	
		// GameObjects
		public GameObject		m_PanelFriends			= null;
		public GameObject		m_PanelGifts			= null;

		// Contents
		public Image			m_PanelContentFriends	= null;
		public Image			m_PanelContentGifts		= null;

		// Scroll
		public Scrollbar		m_Scrollbar				= null;

		// Status
		private int				m_CurrentTab			= 0;

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
			// Swith tab, show/hide and update information of each content
			SwitchTab();

			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(this.transform, true);
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

		public void Button_Home()
		{
			// Play Back button sound
			SoundController.Instance.Play_SoundBack();

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);

			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 02 - " + OrientationName + " - Home", 1.5f);
		}

		public void Button_Friends()
		{
			// Play Tap button sound
			SoundController.Instance.Play_SoundTap();

			// Set current tab to 0
			m_CurrentTab = 0;

			// Swith tab, show/hide and update information of each content
			SwitchTab();
		}

		public void Button_Gifts()
		{
			// Play Tap button sound
			SoundController.Instance.Play_SoundTap();

			// Set current tab to 1
			m_CurrentTab = 1;

			// Swith tab, show/hide and update information of each content
			SwitchTab();
		}

		public void Button_Invite()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		}

		public void Button_Send()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		}

		public void Scrollbar_ValueChanged()
		{
			string CurrentLevel = Application.loadedLevelName;

			// Lanscape demo scene
			if(CurrentLevel.Contains("Landscape")==true)
			{ 
				if(m_CurrentTab==0)
				{ 
					RectTransform pRectTransform = m_PanelContentFriends.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 840.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==1)
				{ 
					RectTransform pRectTransform = m_PanelContentGifts.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 840.0f * m_Scrollbar.value, 0);
				}
			}
			// Portrait demo scene
			else
			{
				if(m_CurrentTab==0)
				{ 
					RectTransform pRectTransform = m_PanelContentFriends.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 280.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==1)
				{ 
					RectTransform pRectTransform = m_PanelContentGifts.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 280.0f * m_Scrollbar.value, 0);
				}
			}
		}

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		// Swith tab, show/hide and update information of each content
		void SwitchTab()
		{ 
			if(m_CurrentTab == 0)
			{
				m_ImageButtonFriends.color = new Color(1,1,1,1);
				m_TextButtonFriends.color = new Color(1,1,1,1);

				m_ImageButtonGifts.color = new Color(0.75f,0.75f,0.75f,1);
				m_TextButtonGifts.color = new Color(0.0f,0.0f,0.0f,1);

				m_TextFriendDetails.text = "5/50 friends";

				m_Scrollbar.value = 0;
				m_PanelFriends.SetActive(true);
				m_PanelGifts.SetActive(false);
			}
			else if(m_CurrentTab == 1)
			{
				m_ImageButtonFriends.color = new Color(0.75f,0.75f,0.75f,1);
				m_TextButtonFriends.color = new Color(0.0f,0.0f,0.0f,1);

				m_ImageButtonGifts.color = new Color(1,1,1,1);
				m_TextButtonGifts.color = new Color(1,1,1,1);
			
				m_TextFriendDetails.text = "5/50 friends";

				m_Scrollbar.value = 0;
				m_PanelFriends.SetActive(false);
				m_PanelGifts.SetActive(true);
			}
		}

	#endregion //UI functions
}
