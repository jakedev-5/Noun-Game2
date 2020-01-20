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
// This class handles "Demo 09 - Landscape - Credits" and "Demo 09 - Portrait - Credits"
// ######################################################################

public class Demo09_Credits : MonoBehaviour
{

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
			// Stop particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.StopParticle(this.transform); 

			// Show Credits Panel
			StartCoroutine(ShowCredits());
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
			GEAnimSystem.Instance.LoadLevel("Demo 02 - " + OrientationName + " - Home", 1.0f);
		}

	#endregion //Respond functions
	
	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator ShowCredits()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);

			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(this.transform);

			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(this.transform, true);
		}

	#endregion //UI functions
}
