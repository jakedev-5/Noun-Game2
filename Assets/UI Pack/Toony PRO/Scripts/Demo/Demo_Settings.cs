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
// This class handle Settings Canvas in "Demo 02 - Landscape - Home", "Demo 02 - Portrait - Home" demo scenes, "Demo 04 - Landscape - Gameplay" and "Demo 04 - Portrait - Gameplay"
// ######################################################################

public class Demo_Settings : Demo_GUIPanel {
	
	#region Variables

		// Sliders
		public Slider m_Music				=	null;
		public Slider m_Sound				=	null;
		public Slider m_Vibration			=	null;
	
		// Toggles
		public Toggle m_AutoUpdate			=	null;
		public Toggle m_Notifications		=	null;
		public Toggle m_GraphicQualityHigh	=	null;
		public Toggle m_GraphicQualityMed	=	null;
		public Toggle m_GraphicQualityLow	=	null;

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
			// Load configs from PlayerPrefs
			Demo_Config.Instance.Load();

			// Update UIs using variables in Demo_Config
			UpdateUIs();
		}
	
		// Update is called once per frame
		void Update () {
	
		}
	
	#endregion //MonoBehaviour Functions

	// ######################################################################
	// Respond functions
	// ######################################################################

	#region Respond functions

		public void Slider_Music()
		{ 
			Demo_Config.Instance.m_Music = m_Music.value;

			SoundController.Instance.SetMusicVolume(m_Music.value);
		}

		public void Slider_Sound()
		{ 
			Demo_Config.Instance.m_Sound = m_Sound.value;

			SoundController.Instance.SetSoundVolume(m_Sound.value);

			// Play Yes sound
			SoundController.Instance.Play_SoundYes();
		}

		public void Button_DecreaseMusic()
		{ 
			// Minus 0.1 to Music volume
			m_Music.value -= 0.1f;
			if(m_Music.value<0)
				m_Music.value = 0.0f;
		}

		public void Button_IncreaseMusic()
		{ 
			// Plus 0.1 to Music volume
			m_Music.value += 0.1f;
			if(m_Music.value>1.0f)
				m_Music.value = 1.0f;
		}

		public void Button_DecreaseSound()
		{ 
			// Minus 0.1 to Sound volume
			m_Sound.value -= 0.1f;
			if(m_Sound.value<0)
				m_Sound.value = 0.0f;
		}

		public void Button_IncreaseSound()
		{ 
			// Plus 0.1 to Sound volume
			m_Sound.value += 0.1f;
			if(m_Sound.value>1.0f)
				m_Sound.value = 1.0f;
		}

		public void Slider_Vibration()
		{ 
			if(m_Vibration.value>0)
			{
				Demo_Config.Instance.m_Vibration = true;
			}
			else
			{
				Demo_Config.Instance.m_Vibration = false;
			}
		
			// Play Tap button sound
			SoundController.Instance.Play_SoundTap();
		}

		public void Toggle_AutoUpdate()
		{ 		
			Demo_Config.Instance.m_AutoUpdate = m_AutoUpdate.isOn;

			if(Demo_Config.Instance.m_AutoUpdate)
			{
				// Play Yes button sound
				SoundController.Instance.Play_SoundYes();
			}
			else
			{ 
				// Play No button sound
				SoundController.Instance.Play_SoundNo();
			}
		}

		public void Toggle_Notification()
		{ 		
			Demo_Config.Instance.m_Notifications = m_Notifications.isOn;

			if(Demo_Config.Instance.m_Notifications)
			{
				// Play Yes button sound
				SoundController.Instance.Play_SoundYes();
			}
			else
			{ 
				// Play No button sound
				SoundController.Instance.Play_SoundNo();
			}
		}

		public void Toggle_GraphicHigh()
		{ 		
			if(m_GraphicQualityHigh.isOn==true)
			{ 
				Demo_Config.Instance.m_Quality = eGraphicQuality.High;
				SoundController.Instance.Play_SoundYes();
			}
		}

		public void Toggle_GraphicMed()
		{ 		
			if(m_GraphicQualityMed.isOn==true)
			{ 
				Demo_Config.Instance.m_Quality = eGraphicQuality.Medium;
				SoundController.Instance.Play_SoundYes();
			}
		}

		public void Toggle_GraphicLow()
		{ 		
			if(m_GraphicQualityLow.isOn==true)
			{ 
				Demo_Config.Instance.m_Quality = eGraphicQuality.Low;
				SoundController.Instance.Play_SoundYes();
			}
		}

		public void Button_ResetToDefault()
		{ 
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			Demo_Config.Instance.LoadDefault();
			
			// Update UIs using variables in Demo_Config
			UpdateUIs();
		}

		public void Button_FacebookLogin()
		{ 
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		}

		public void Button_Close()
		{ 
			// Play Back button sound
			SoundController.Instance.Play_SoundBack();		
		
			// Save configs from PlayerPrefs
			Demo_Config.Instance.Save();

			// Hide this panel
			Hide();
		}
	
	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		// Update UIs using variables in Demo_Config
		void UpdateUIs()
		{
			m_Music.value = Demo_Config.Instance.m_Music;

			m_Sound.value = Demo_Config.Instance.m_Sound;

			if(Demo_Config.Instance.m_Vibration == true)
			{
				m_Vibration.value = 1;
			}
			else
			{
				m_Vibration.value = 0;
			}

			m_AutoUpdate.isOn = Demo_Config.Instance.m_AutoUpdate;

			m_Notifications.isOn = Demo_Config.Instance.m_Notifications;
		
			if(Demo_Config.Instance.m_Quality == eGraphicQuality.High)
				m_GraphicQualityHigh.isOn	= true;
			else if(Demo_Config.Instance.m_Quality == eGraphicQuality.Medium)
				m_GraphicQualityMed.isOn	= true;
			else if(Demo_Config.Instance.m_Quality == eGraphicQuality.Low)
				m_GraphicQualityLow.isOn	= true;
		}
	
	#endregion //UI functions
}
