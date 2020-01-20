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
// This class handles "Demo 01 - Landscape - Intro & Loading" and "Demo 01 - Portrait - Intro & Loading"
// ######################################################################

public class Demo01_IntroAndLoading : MonoBehaviour
{
	#region Variables
	
		// Intro
		public GameObject			m_Intro				= null;

		// Loading
		public GameObject			m_Loading			= null;
		public Slider				m_Slider			= null;

		// Time
		public float				m_ShowLogoTime		= 2.0f;
		public float				m_ShowLoadingTime	= 1.0f;
		public float				m_IdleLoadingTime	= 1.0f;

		// Loading Progress
		private AsyncOperation  m_Async				= null;
	
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

			// Deactivates m_Intro and m_Loading GameObjects.
			m_Intro.SetActive(false);
			m_Loading.SetActive(false);
		}

		// Use this for initialization
		void Start()
		{
			 // Update Loading progress bar.
			 StartCoroutine(ShowLogo());

			 // Reset Loading progress bar to zero.
			 m_Slider.value = 0.0f;
		}
	
		// Update is called once per frame.
		void Update ()
		{
			 // Update Loading progress bar.
			 if (m_Async != null)
			 {
					// Update Loading progress bar.
					if (m_Async.progress<0.9f)
						m_Slider.value = m_Async.progress;
					else
						m_Slider.value = 1.0f;
			 }
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator ShowLogo()
		{
			 // Activate m_Intro GameObject then animate MoveIn animation.
			 m_Intro.gameObject.SetActive(true);

			// Play MoveIn animation
			 GEAnimSystem.Instance.MoveIn(m_Intro.transform, true);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			 yield return new WaitForSeconds(m_ShowLogoTime);

			 // Start ShowLoading() coroutine.
			 StartCoroutine(ShowLoading());
		}

		IEnumerator ShowLoading()
		{
			 // Play Move Out animation
			 GEAnimSystem.Instance.MoveOut(m_Intro.transform, true);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			 yield return new WaitForSeconds(m_ShowLoadingTime);

			 // Start IdleLoading() coroutine.
			 StartCoroutine(IdleLoading());
		}

		IEnumerator IdleLoading()
		{
			 // Activate m_Loading GameObject then animate MoveIn animation.
			 m_Loading.SetActive(true);

			// Play MoveIn animation
			 GEAnimSystem.Instance.MoveIn(m_Loading.transform, true);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			 yield return new WaitForSeconds(m_IdleLoadingTime);

			 // Start LoadNextScene() coroutine.
			 StartCoroutine(LoadNextScene());
		}

		IEnumerator LoadNextScene()
		{
			 // Reset Loading progress bar to zero.
			 m_Slider.value = 0.0f;

			 // Load next scene asynchronously in the background.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			m_Async = Application.LoadLevelAsync("Demo 02 - " + OrientationName + " - Home");
			yield return m_Async;
		}

	#endregion //UI functions
}
