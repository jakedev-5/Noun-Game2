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
// This class handles "Demo 10 - Landscape - Shop" and "Demo 10 - Portrait - Shop"
// ######################################################################

public class Demo10_Shop : MonoBehaviour {
	
	#region Variables

		// Tabs
		public Image			m_ImageButtonItems		= null;
		public Image			m_ImageButtonGold		= null;
		public Image			m_ImageButtonGems		= null;

		// Texts
		public Text				m_TextButtonItems		= null;
		public Text				m_TextButtonGold		= null;
		public Text				m_TextButtonGems		= null;
		public Text				m_TextGold				= null;
	
		// GameObjects
		public GameObject		m_PanelItems			= null;
		public GameObject		m_PanelGold				= null;
		public GameObject		m_PanelGems				= null;

		// Contents
		public Image			m_PanelContentItems		= null;
		public Image			m_PanelContentGold		= null;
		public Image			m_PanelContentGems		= null;

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
			// Init Gold Text
			m_TextGold.text = string.Format("{0:n0}", (Random.Range(1, 1000) * 100));

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

		public void Button_Items()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Set current tab to 0
			m_CurrentTab = 0;

			// Swith tab, show/hide and update information of each content
			SwitchTab();
		}

		public void Button_Gold()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Set current tab to 1
			m_CurrentTab = 1;

			// Swith tab, show/hide and update information of each content
			SwitchTab();
		}

		public void Button_Gems()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Set current tab to 2
			m_CurrentTab = 2;

			// Swith tab, show/hide and update information of each content
			SwitchTab();
		}

		public void Button_PlaySoundClick()
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
				// Update content in Mask
				if(m_CurrentTab==0)
				{ 
					RectTransform pRectTransform = m_PanelContentItems.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 1150.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==1)
				{ 
					RectTransform pRectTransform = m_PanelContentGold.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 580.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==2)
				{
					RectTransform pRectTransform = m_PanelContentGems.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 580.0f * m_Scrollbar.value, 0);
				}
			}
			// Portrait demo scene
			else
			{ 
				// Update content in Mask
				if(m_CurrentTab==0)
				{ 
					RectTransform pRectTransform = m_PanelContentItems.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 580.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==1)
				{ 
					RectTransform pRectTransform = m_PanelContentGold.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 580.0f * m_Scrollbar.value, 0);
				}
				else if(m_CurrentTab==2)
				{ 
					RectTransform pRectTransform = m_PanelContentGems.transform.GetComponent<RectTransform>();
					if(pRectTransform!=null)
						pRectTransform.anchoredPosition = new Vector3(0, 580.0f * m_Scrollbar.value, 0);
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
				// Set Tab 0 colors
				m_ImageButtonItems.color = new Color(1,1,1,1);
				m_TextButtonItems.color = new Color(1,1,1,1);

				// Set Tab 1 colors
				m_ImageButtonGold.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonGold.color = new Color(0.78f,0.78f,0.78f,1);

				// Set Tab 2 colors
				m_ImageButtonGems.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonGems.color = new Color(0.78f,0.78f,0.78f,1);
			
				// Reset Scrollbar
				m_Scrollbar.gameObject.SetActive(true);
				m_Scrollbar.value = 0;

				// Set content Panels to active/inactive
				m_PanelItems.SetActive(true);
				m_PanelGold.SetActive(false);
				m_PanelGems.SetActive(false);

				GEAnimSystem.Instance.PlayParticle(m_PanelItems.transform);	// Play particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.StopParticle(m_PanelGold.transform);		// Stop particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.StopParticle(m_PanelGems.transform);		// Stop particles in the hierarchy of given transfrom
			}
			else if(m_CurrentTab == 1)
			{
				// Set Tab 0 colors
				m_ImageButtonItems.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonItems.color = new Color(0.78f,0.78f,0.78f,1);
				
				// Set Tab 1 colors
				m_ImageButtonGold.color = new Color(1,1,1,1);
				m_TextButtonGold.color = new Color(1,1,1,1);
				
				// Set Tab 2 colors
				m_ImageButtonGems.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonGems.color = new Color(0.78f,0.78f,0.78f,1);
			
				// Reset Scrollbar
				m_Scrollbar.gameObject.SetActive(true);
				m_Scrollbar.value = 0;

				// Set content Panels to active/inactive
				m_PanelItems.SetActive(false);
				m_PanelGold.SetActive(true);
				m_PanelGems.SetActive(false);

				GEAnimSystem.Instance.StopParticle(m_PanelItems.transform);	// Stop particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.PlayParticle(m_PanelGold.transform);		// Play particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.StopParticle(m_PanelGems.transform);		// Stop particles in the hierarchy of given transfrom
			}
			else if(m_CurrentTab == 2)
			{
				// Set Tab 0 colors
				m_ImageButtonItems.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonItems.color = new Color(0.78f,0.78f,0.78f,1);
				
				// Set Tab 1 colors
				m_ImageButtonGold.color = new Color(0.5f,0.5f,0.5f,1);
				m_TextButtonGold.color = new Color(0.78f,0.78f,0.78f,1);
				
				// Set Tab 2 colors
				m_ImageButtonGems.color = new Color(1,1,1,1);
				m_TextButtonGems.color = new Color(1,1,1,1);
			
				// Reset Scrollbar
				m_Scrollbar.gameObject.SetActive(true);
				m_Scrollbar.value = 0;

				// Set content Panels to active/inactive
				m_PanelItems.SetActive(false);
				m_PanelGold.SetActive(false);
				m_PanelGems.SetActive(true);

				GEAnimSystem.Instance.StopParticle(m_PanelItems.transform);	// Stop particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.StopParticle(m_PanelGold.transform);		// Stop particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.PlayParticle(m_PanelGems.transform);		// Play particles in the hierarchy of given transfrom
			}
		}

	#endregion //UI functions
}
