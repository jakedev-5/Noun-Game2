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
// This class handles "Demo 05 - Landscape - Ranks" and "Demo 05 - Portrait - Ranks"
// ######################################################################

public class Demo05_Ranks : MonoBehaviour {
	
	#region Variables

		// Tabs
		public Image			m_ImageButtonFriends	= null;
		public Image			m_ImageButtonAllPlayers	= null;

		// Texts
		public Text				m_TextHeader			= null;
		public Text				m_TextButtonFriends		= null;
		public Text				m_TextButtonAllPlayers	= null;

		// Scroll
		public Scrollbar		m_Scrollbar				= null;

		// Content
		public Image			m_PanelContent			= null;
	
		// Prefab Sprite
		public Sprite[]			m_PrefabPortraits		= null;

		// Infos
		public Demo_RankInfo[]	m_RankItems				= null;

		// Ranks
		[System.Serializable]
		public class Rank
		{
			public string	m_Name;
			public int		m_Score;
			[HideInInspector]
			public Sprite	m_PortraitSprite;
		}
		public Rank[]			m_RankOfFriends		= null;
		public Rank[]			m_RankOfAllPlayers	= null;

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
			// Random Players' portrait
			int RandNumOld = -1;
			int RandNumNew = -1;
			for(int i=0;i<m_RankOfFriends.Length;i++)
			{
				while(RandNumOld==RandNumNew)
				{
					RandNumNew = Random.Range(0,m_PrefabPortraits.Length-1);
				}
				RandNumOld = RandNumNew;
				m_RankOfFriends[i].m_PortraitSprite = m_PrefabPortraits[RandNumNew];
			}
			for(int i=0;i<m_RankOfAllPlayers.Length;i++)
			{
				while(RandNumOld==RandNumNew)
				{
					RandNumNew = Random.Range(0,m_PrefabPortraits.Length-1);
				}
				RandNumOld = RandNumNew;
				m_RankOfAllPlayers[i].m_PortraitSprite = m_PrefabPortraits[RandNumNew];
			}

			SwitchTab(0);

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

			SwitchTab(0);
		}

		public void Button_AllPlayers()
		{
			// Play Tap button sound
			SoundController.Instance.Play_SoundTap();

			SwitchTab(1);
		}

		public void Scrollbar_ValueChanged()
		{
			string CurrentLevel = Application.loadedLevelName;

			// Lanscape demo scene
			if(CurrentLevel.Contains("Landscape")==true)
			{ 
				RectTransform pRectTransform = m_PanelContent.transform.GetComponent<RectTransform>();
				if(pRectTransform!=null)
					pRectTransform.anchoredPosition = new Vector3(0, 2240.0f * m_Scrollbar.value, 0);
			}
			// Portrait demo scene
			else
			{
				RectTransform pRectTransform = m_PanelContent.transform.GetComponent<RectTransform>();
				if(pRectTransform!=null)
					pRectTransform.anchoredPosition = new Vector3(0, 1680.0f * m_Scrollbar.value, 0);
			}
		}

	#endregion //Respond functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		void SwitchTab(int index)
		{ 
			if(index == 0)
			{ 		
				m_ImageButtonFriends.color = new Color(1,1,1,1);
				m_TextButtonFriends.color = new Color(1,1,1,1);

				m_ImageButtonAllPlayers.color = new Color(0.75f,0.75f,0.75f,1);
				m_TextButtonAllPlayers.color = new Color(0.0f,0.0f,0.0f,1);

				m_TextHeader.text = "Top 10 of 256 friends";

				UpdateItemList(0);
			}
			if(index == 1)
			{
				m_ImageButtonFriends.color = new Color(0.75f,0.75f,0.75f,1);
				m_TextButtonFriends.color = new Color(0.0f,0.0f,0.0f,1);

				m_ImageButtonAllPlayers.color = new Color(1,1,1,1);
				m_TextButtonAllPlayers.color = new Color(1,1,1,1);
			
				m_TextHeader.text = "12,525,980 players";

				UpdateItemList(1);
			}
		}

		void UpdateItemList(int index)
		{
			if(index==0)
			{ 
				for(int i=0;i<m_RankItems.Length;i++)
				{
 					if(i<m_RankOfFriends.Length)
					{ 
						// Set information to current rank.
						// Note this function have to be called anytime after BindGameObjects is called.
						m_RankItems[i].SetInfo((i+1).ToString(), 
							m_RankOfFriends[i].m_Name, 
							string.Format("{0:n0}", m_RankOfFriends[i].m_Score),
							m_RankOfFriends[i].m_PortraitSprite);
					}
				}
			}
			else if(index==1)
			{
				for(int i=0;i<m_RankItems.Length;i++)
				{
 					if(i<m_RankOfAllPlayers.Length)
					{ 
						// Set information to current rank.
						// Note this function have to be called anytime after BindGameObjects is called.
						m_RankItems[i].SetInfo((i+1).ToString(), 
							m_RankOfAllPlayers[i].m_Name, 
							string.Format("{0:n0}", m_RankOfAllPlayers[i].m_Score),
							m_RankOfAllPlayers[i].m_PortraitSprite);
					}
				}
			}
		}

	#endregion //UI functions
}
