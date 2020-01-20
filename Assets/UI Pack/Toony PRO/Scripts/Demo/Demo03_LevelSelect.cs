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
// This class handles "Demo 03 - Landscape - Level Select" and "Demo 03 - Portrait - Level Select"
// ######################################################################

public class Demo03_LevelSelect : MonoBehaviour
{
	#region Variables
	
		// UIs
		public GEAnim		m_Banner				= null;
		public GEAnim		m_Page0					= null;
		public GEAnim		m_Page1					= null;
		public GEAnim		m_ArrowLeft				= null;
		public GEAnim		m_ArrowRight			= null;
		public GEAnim		m_Home					= null;
		public GEAnim		m_Play					= null;
		public GEAnim		m_Shop					= null;
		public GEAnim		m_Page_0_Dot			= null;
		public GEAnim		m_Page_1_Dot			= null;

		// Buttons
		public Button			m_ArrowLeftButton		= null;
		public Button			m_ArrowRightButton		= null;

		// Pages
		private int				m_CurrentPage			= 0;
		//private int				m_PageCount				= 4;
	
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
			// Update Loading progress bar.
			StartCoroutine(ShowLevelSelect());

			if(m_Page0.gameObject.activeSelf == false)
				m_Page0.gameObject.SetActive(true);
			if(m_Page1.gameObject.activeSelf == false)
				m_Page1.gameObject.SetActive(true);


		}
	
		// Update is called once per frame.
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

			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 02 - " + OrientationName + " - Home", 1.5f);
		}

		public void Button_Play(GameObject goButton)
		{
			// Play Play button sound
			SoundController.Instance.Play_SoundPlay();
		
			int ParticleIndex = Random.Range(0,2);
			if(ParticleIndex==0)
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);
			else
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_02);
		
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 04 - " + OrientationName + " - Gameplay", 1.5f);
		}

		public void Button_Shop()
		{
			SoundController.Instance.Play_SoundClick();
		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);

			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 10 - " + OrientationName + " - Shop", 1.5f);
		}

		public void Button_PreviousPage()
		{
			SoundController.Instance.Play_SoundClick();

			if(m_CurrentPage!=0)
			{
				m_Page0.m_MoveOut.Enable = true;
				m_Page0.m_MoveIn.Enable = true;
				m_Page0.m_MoveIn.MoveFrom = ePosMove.LeftScreenEdge;
				m_Page0.m_MoveIn.Time = 1.5f;
				m_Page0.m_MoveIn.Delay = 0;
				m_Page0.Reset();					// Reset all animations' information of before replay
				m_Page0.MoveIn(eGUIMove.Self);		// Play MoveIn animation
				m_Page_0_Dot.gameObject.GetComponent<Image>().color = new Color(1,1,0,1);
			
				m_Page1.m_MoveIn.Enable = true;
				m_Page1.m_MoveOut.Enable = true;
				m_Page1.m_MoveOut.MoveTo = ePosMove.RightScreenEdge;
				m_Page1.m_MoveOut.Time = 1.5f;
				m_Page1.m_MoveOut.Delay = 0;
				m_Page1.Reset();					// Reset all animations' information of before replay
				m_Page1.MoveOut(eGUIMove.Self);		// Play Move Out animation
				m_Page_1_Dot.gameObject.GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,0.4f);
			}

			m_CurrentPage--;
			UpdateArrowButtons();

		}

		public void Button_NextPage()
		{
			SoundController.Instance.Play_SoundClick();

			if(m_CurrentPage!=1)
			{
				m_Page1.m_MoveOut.Enable = false;
				m_Page1.m_MoveIn.Enable = true;
				m_Page1.m_MoveIn.MoveFrom = ePosMove.RightScreenEdge;
				m_Page1.m_MoveIn.Time = 1.5f;
				m_Page1.m_MoveIn.Delay = 0;
				m_Page1.Reset();					// Reset all animations' information of before replay
				m_Page1.MoveIn(eGUIMove.Self);		// Play MoveIn animation
				m_Page_0_Dot.gameObject.GetComponent<Image>().color = new Color(0.4f,0.4f,0.4f,0.4f);
			
				m_Page0.m_MoveIn.Enable = false;
				m_Page0.m_MoveOut.Enable = true;
				m_Page0.m_MoveOut.MoveTo = ePosMove.LeftScreenEdge;
				m_Page0.m_MoveOut.Time = 1.5f;
				m_Page0.m_MoveOut.Delay = 0;
				m_Page0.Reset();					// Reset all animations' information of before replay
				m_Page0.MoveOut(eGUIMove.Self);		// Play Move Out animation
				m_Page_1_Dot.gameObject.GetComponent<Image>().color = new Color(1,1,0,1);
			}

			m_CurrentPage++;
			UpdateArrowButtons();
		}

		public void Button_ClearedLevel()
		{
			// Play No button sound
			SoundController.Instance.Play_SoundNo();
		}

		public void Button_CurrentLevel(GameObject goButton)
		{
			// Play Play button sound
			SoundController.Instance.Play_SoundPlay();
		
			int ParticleIndex = Random.Range(0,2);
			if(ParticleIndex==0)
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);
			else
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_02);
		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);

			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 04 - " + OrientationName + " - Gameplay", 1.5f);
		}

		public void Button_LockedLevel()
		{
			// Play Disable button sound
			SoundController.Instance.Play_SoundDisable();
		}

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator ShowLevelSelect()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.25f);

			// Play MoveIn animation
			m_Banner.MoveIn();

			// Update Loading progress bar.
			StartCoroutine(ShowPage0());
		}

		IEnumerator ShowPage0()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);

			foreach (Transform child in m_Page0.transform)
			{
				GEAnim pGEAnim = child.gameObject.GetComponent<GEAnim>(); 
				if(pGEAnim!=null)
				{
					pGEAnim.m_ScaleIn.Delay = Random.Range(0.0f, 1.0f);
					pGEAnim.m_ScaleIn.Time = Random.Range(0.5f, 1.5f);

					// Play MoveIn animation
					pGEAnim.MoveIn();
				}
			}

			// Play MoveIn animation
			m_Page1.MoveIn(eGUIMove.Children);

			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(m_Page0.transform);

			// Update Loading progress bar.
			StartCoroutine(ShowBottomScreenButtons());
		}

		IEnumerator ShowBottomScreenButtons()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.25f);

			m_Play.m_ScaleIn.Delay = Random.Range(0.0f, 0.5f);
			m_Play.m_ScaleIn.Time = Random.Range(0.5f, 1.0f);

			// Play MoveIn animation
			m_Play.MoveIn(eGUIMove.Self);

			float WaitForPlayButton = (m_Play.m_ScaleIn.Delay + m_Play.m_ScaleIn.Time)/2.0f;

			m_Home.m_MoveIn.Delay = WaitForPlayButton + Random.Range(0.5f, 1.0f);
			m_Home.m_MoveIn.Time = WaitForPlayButton + Random.Range(0.5f, 1.0f);
			m_Home.m_FadeIn.Delay = m_Home.m_MoveIn.Delay;
			m_Home.m_FadeIn.Time = m_Home.m_MoveIn.Time;

			// Play MoveIn animation
			m_Home.MoveIn();

			m_Shop.m_MoveIn.Delay = WaitForPlayButton + Random.Range(0.5f, 1.0f);
			m_Shop.m_MoveIn.Time = WaitForPlayButton + Random.Range(0.5f, 1.0f);
			m_Shop.m_FadeIn.Delay = m_Shop.m_MoveIn.Delay;
			m_Shop.m_FadeIn.Time = m_Shop.m_MoveIn.Time;

			// Play MoveIn animation
			m_Shop.MoveIn();

			// Update Loading progress bar.
			StartCoroutine(ShowArrows());
		}

		IEnumerator ShowArrows()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);

			m_ArrowLeft.m_ScaleIn.Delay = Random.Range(0.5f, 1.0f);
			m_ArrowLeft.m_ScaleIn.Time = Random.Range(0.5f, 1.0f);

			// Play MoveIn animation
			m_ArrowLeft.MoveIn();

			m_ArrowRight.m_ScaleIn.Delay = Random.Range(0.5f, 1.0f);
			m_ArrowRight.m_ScaleIn.Time = Random.Range(0.5f, 1.0f);

			// Play MoveIn animation
			m_ArrowRight.MoveIn();

			// Play MoveIn animations
			m_Page_0_Dot.MoveIn();
			m_Page_1_Dot.MoveIn();

			UpdateArrowButtons();
		}

		void UpdateArrowButtons()
		{
			// Limit page to 0 and 1
			if(m_CurrentPage<=0)
			{
				m_CurrentPage = 0;

				// Disable Left Arrow button
				m_ArrowLeftButton.enabled = false;
				m_ArrowLeftButton.interactable = false;
				SetImageColor(m_ArrowLeftButton.gameObject, new Color(0.5f,0.5f,0.5f,0.5f));			

				// Enable Right Arrow button
				m_ArrowRightButton.enabled = true;
				m_ArrowRightButton.interactable = true;
				SetImageColor(m_ArrowRightButton.gameObject, new Color(1.0f,1.0f,1.0f,1.0f));
			}
			else if(m_CurrentPage>=1)
			{
				m_CurrentPage = 1;

				// Enable Left Arrow button
				m_ArrowLeftButton.enabled = true;
				m_ArrowLeftButton.interactable = true;
				SetImageColor(m_ArrowLeftButton.gameObject, new Color(1.0f,1.0f,1.0f,1.0f));

				// Disable Right Arrow button
				m_ArrowRightButton.enabled = false;
				m_ArrowRightButton.interactable = false;
				SetImageColor(m_ArrowRightButton.gameObject, new Color(0.5f,0.5f,0.5f,0.5f));
			}
		}

		// Change color of Image UI
		void SetImageColor(GameObject gObj, Color Color)
		{
			Image pImage = gObj.GetComponent<Image>();
			if(pImage!=null)
			{
				pImage.color = Color;
			}

			foreach(Transform child in gObj.transform)
			{
				SetImageColor(child.gameObject, Color);
			}
		}

	#endregion //UI functions
}
