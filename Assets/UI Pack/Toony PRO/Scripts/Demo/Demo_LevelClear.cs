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
// This class handles Level Clear canvas.
// It is used in "Demo 04 - Landscape - Gameplay" and "Demo 04 - Portrait - Gameplay"
// ######################################################################

public class Demo_LevelClear : Demo_GUIPanel {
	
	#region Variables

		// Texts
		public Text				m_Text_Score		= null;
		public Text				m_Text_BestScore	= null;
		public Text				m_Text_Time			= null;
		public Text				m_Text_Coin			= null;

		// GEAnims	
		public GEAnim		m_BestScore			= null;
		public GEAnim		m_Star1				= null;
		public GEAnim		m_Star2				= null;
		public GEAnim		m_Star3				= null;

		// Toggles
		bool					m_ReportScore		= false;
		bool					m_ReportCoin		= false;

		// Info
		private int				m_Score				= 0;
		private float			m_RemainingTime		= 0;
		private int				m_Coin				= 0;

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
			// Reset all Texts
			m_Text_Score.text			= "0";
			m_Text_BestScore.text		= "0";
			m_Text_Time.text			= "0";
			m_Text_Coin.text			= "0";

			// Stop particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.StopParticle(this.transform);
		}
	
		// Update is called once per frame
		void Update ()
		{	
			// Count-up Score Text
			if(m_ReportScore==true)
			{ 
				int Score = int.Parse(m_Text_Score.text);
				if(Score<m_Score)
				{
					int AddScore = (m_Score-Score)/4;
					if(AddScore<=0) AddScore = 1;
					Score+=AddScore;
					if(Score>m_Score)
						Score=m_Score;
					m_Text_Score.text = Score.ToString();
				}
				else if(Score>m_Score)
				{
					Score-=1;
					if(Score<m_Score)
						Score=m_Score;
					m_Text_Score.text = Score.ToString();
				}
			}

			// Count-up Coin Text
			if(m_ReportCoin==true)
			{ 
				int Coin = int.Parse(m_Text_Coin.text.Replace(",",""));
				if(Coin<m_Coin)
				{
					int AddCoin = (m_Coin-Coin)/4;
					if(AddCoin<=0) AddCoin = 1;
					Coin+=AddCoin;
					if(Coin>m_Coin)
						Coin=m_Coin;
					m_Text_Coin.text = string.Format("{0:n0}", Coin);
				}
			}
		}
	
	#endregion //MonoBehaviour functions

	// ######################################################################
	// Respond functions
	// ######################################################################

	#region Respond functions

		public void Button_Home()
		{
			// Hide this panel
			Hide();

			// Play Back button sound
			SoundController.Instance.Play_SoundBack();
		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 02 - " + OrientationName + " - Home", 1.5f);
		}

		public void Button_Play()
		{
			// Hide this panel
			Hide();

			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 03 - " + OrientationName + " - Level Select", 1.5f);
		}

		public void Button_Replay()
		{		
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(this.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 04 - " + OrientationName + " - Gameplay", 1.5f);
		}

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		public void Show(int Score, float RemainingTime, int Coin)
		{ 
			// Update some Text objects
			m_Score	= Score;
			m_RemainingTime = RemainingTime;
			m_Coin	= Coin;
		
			// Begin report Text animations
			StartCoroutine(Report());

			// Show this panel
			this.gameObject.GetComponent<Demo_GUIPanel>().Show();
		}

		// Hide this panel
		public void HideMe()
		{
			// Hide this panel
			this.gameObject.GetComponent<Demo_GUIPanel>().Hide();
		}

		IEnumerator Report()
		{
			// Random a best score
			int BestScore				= m_Score - (Random.Range(1,10) * 100);
			if(BestScore<0)
				BestScore	= 0;

			// Set Texts
			m_Text_BestScore.text		= BestScore.ToString();
			m_Text_Time.text			= string.Format("{00:00}", (int)m_RemainingTime/60) + ":" + string.Format("{00:00}", (int)m_RemainingTime%60);

			// Disable all Star animation
			m_Star1.enabled		= false;
			m_Star2.enabled		= false;
			m_Star3.enabled		= false;
			m_BestScore.enabled	= false;
		
			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(this.transform);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(1.0f);
		
			// Start count up number for m_Text_Score
			m_ReportScore = true;
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(1.0f);
		
			// Start count up number for m_Text_Coin
			m_ReportCoin = true;
		
			// Start m_Star1 MoveIn animation
			m_Star1.enabled = true;
			m_Star1.MoveIn();	// Play MoveIn animation
		
			// Start m_Star2 MoveIn animation
			m_Star2.enabled = true;
			m_Star2.MoveIn();	// Play MoveIn animation
		
			// Start m_Star3 MoveIn animation
			m_Star3.enabled = true;
			m_Star3.MoveIn();	// Play MoveIn animation
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);
		
			// Start m_BestScore MoveIn animation
			m_BestScore.enabled = true;
			m_BestScore.MoveIn();	// Play MoveIn animation
		}

	#endregion //UI functions
}
