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
// This class handles "Demo 08 - Landscape - More Samples" and "Demo 08 - Portrait - More Samples"
// ######################################################################

public class Demo08_MoreSamples : MonoBehaviour
{
	#region Variables

		// GameObject
		public GameObject		m_PanelBottom			= null;

		// Text
		public Text				m_TextIndex				= null;
	
		// UIs
		public GEAnim[]	m_Samples				= null;

		// Buttons
		public Button			m_ArrowLeftButton		= null;
		public Button			m_ArrowRightButton		= null;

		// Status
		public int				m_SampleUI_Index		= 0;
		public int				m_SampleUI_IndexOld		= 0;

		// Button wait time
		float					m_ButtonWaitTimeCount	= 0;
		float					m_ButtonWaitTime		= 2.0f;
	
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
		void Start()
		{		
			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(m_PanelBottom.transform, true);

			// Stop particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.StopParticle(this.transform);
		
			StartCoroutine(ShowNextSample());
		}

		// Update is called once per frame.
		void Update ()
		{
			// Count down m_ButtonWaitTimeCount
			if(m_ButtonWaitTimeCount>0)
			{ 
				m_ButtonWaitTimeCount -= Time.deltaTime;

				// Enable Interact of Left/Right Arrow buttons
				if(m_ButtonWaitTimeCount<=0)
				{
					GEAnimSystem.Instance.EnableButton(m_ArrowLeftButton.transform, true);
					m_ArrowLeftButton.interactable = true;

					GEAnimSystem.Instance.EnableButton(m_ArrowRightButton.transform, true);
					m_ArrowRightButton.interactable = true;
				}
				return;
			}
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

		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(m_Samples[m_SampleUI_Index].transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_Samples[m_SampleUI_Index].transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelBottom.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 02 - " + OrientationName + " - Home", 1.0f);
		}

		public void Button_LeftArrow()
		{
			if(m_ButtonWaitTimeCount>0)
			{ 
				return;
			}

			m_ButtonWaitTimeCount = m_ButtonWaitTime;
			GEAnimSystem.Instance.EnableButton(m_ArrowLeftButton.transform, false);
			m_ArrowLeftButton.interactable = false;
			GEAnimSystem.Instance.EnableButton(m_ArrowRightButton.transform, false);
			m_ArrowRightButton.interactable = false;

			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			m_SampleUI_Index--;
			if(m_SampleUI_Index<0)
				m_SampleUI_Index=m_Samples.Length-1;

			if(m_SampleUI_Index!=m_SampleUI_IndexOld)
			{ 
				StartCoroutine(ShowNextSample());
			}

			m_TextIndex.text = (m_SampleUI_Index+1).ToString() + "/" + m_Samples.Length.ToString();
		}

		public void Button_RightArrow()
		{
			if(m_ButtonWaitTimeCount>0)
			{ 
				return;
			}

			m_ButtonWaitTimeCount = m_ButtonWaitTime;
			GEAnimSystem.Instance.EnableButton(m_ArrowLeftButton.transform, false);
			m_ArrowLeftButton.interactable = false;
			GEAnimSystem.Instance.EnableButton(m_ArrowRightButton.transform, false);
			m_ArrowRightButton.interactable = false;

			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			m_SampleUI_Index++;
			if(m_SampleUI_Index>m_Samples.Length-1)
				m_SampleUI_Index=0;

			if(m_SampleUI_Index!=m_SampleUI_IndexOld)
			{ 
				StartCoroutine(ShowNextSample());
			}

			m_TextIndex.text = (m_SampleUI_Index+1).ToString() + "/" + m_Samples.Length.ToString();
		}

		public void Button_PlaySoundClick()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		}

		public void Button_AssetStore_GUIAnimatorForUnityUI()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			//Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/28709");
			Application.ExternalEval("window.open('https://www.assetstore.unity3d.com/en/#!/content/28709','GUI Animator for Unity UI')");
		}

		public void Button_AssetStore_FirstFantasyForMobile()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			//Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/10822");
			Application.ExternalEval("window.open('https://www.assetstore.unity3d.com/#!/content/10822','First Fantasy for Mobile')");
		}

		public void Button_AssetStore_FXQuest()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			//Application.OpenURL("https://www.assetstore.unity3d.com/#!/content/21073");
			Application.ExternalEval("window.open('https://www.assetstore.unity3d.com/#!/content/21073','FX Quest')");
		}

	#endregion //Respond functions


	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator ShowFirstUI()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);

			// Reset all animations' information of before replay
			m_Samples[m_SampleUI_Index].Reset();

			// Play MoveIn animation
			m_Samples[m_SampleUI_Index].MoveIn();
		}

		IEnumerator ShowNextSample()
		{ 
			if(m_SampleUI_IndexOld>=0 && m_SampleUI_IndexOld<=m_Samples.Length)
			{
				m_Samples[m_SampleUI_IndexOld].MoveOut();

				// Stop particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.StopParticle(m_Samples[m_SampleUI_IndexOld].transform);
			}
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);
		
			if(m_SampleUI_Index>=0 && m_SampleUI_Index<=m_Samples.Length)
			{
				// Reset all animations' information of before replay
				m_Samples[m_SampleUI_Index].Reset();

				// Play MoveIn animation
				m_Samples[m_SampleUI_Index].MoveIn();

				// Play particles in the hierarchy of given transfrom
				GEAnimSystem.Instance.PlayParticle(m_Samples[m_SampleUI_Index].transform);
			}

			m_SampleUI_IndexOld = m_SampleUI_Index;
		}

	#endregion //UI functions	
}
