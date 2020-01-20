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
// This class handles "Demo 02 - Landscape - Home" and "Demo 02 - Portrait - Home"
// ######################################################################

public class Demo02_Home : MonoBehaviour
{
	#region Variables

		// Demo_Settings
		public Demo_Settings	m_Settings					= null;

		// Demo_News
		public Demo_News		m_News						= null;

		// Demo_Help
		public Demo_Help		m_Help						= null;
	
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
			if (m_Help.gameObject.activeSelf == false)
					m_Help.gameObject.SetActive(true);

			if (m_News.gameObject.activeSelf == false)
					m_News.gameObject.SetActive(true);

			if (m_Settings.gameObject.activeSelf == false)
					m_Settings.gameObject.SetActive(true);

		}

		// Use this for initialization
		void Start()
		{
			 // Update Loading progress bar.
			 StartCoroutine(Show());
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

		public void Button_Help()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Show this panel
			m_Help.Show();
		}

		public void Button_Credits()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 09 - " + OrientationName + " - Credits", 1.5f);
		}

		public void Button_Social()
		{
			// Play Click sound
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
			GEAnimSystem.Instance.LoadLevel("Demo 07 - " + OrientationName + " - Social", 1.5f);
		}

		public void Button_News()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Show this panel
			m_News.Show();
		}

		public void Button_CheckoutGUIAnimator()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			//Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/28709");
			Application.ExternalEval("window.open('https://www.assetstore.unity3d.com/en/#!/content/28709','GUI Animator for Unity UI')");
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
		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 03 - " + OrientationName + " - Level Select", 1.5f);
		}

		public void Button_Settings()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Show this panel
			m_Settings.Show();
		}

		public void Button_Ranks()
		{		
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 05 - " + OrientationName + " - Ranks", 1.5f);
		}

		public void Button_Achievement()
		{
			// Play Click sound
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
			GEAnimSystem.Instance.LoadLevel("Demo 06 - " + OrientationName + " - Achievements", 1.5f);
		}

		public void Button_More()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 08 - " + OrientationName + " - More Samples", 1.5f);
		}

		public void Button_Shop()
		{
			// Play Click sound
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

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator Show()
		{
			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(this.transform, true);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(1.0f);

			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(this.transform);
		}

	#endregion //UI functions
}
