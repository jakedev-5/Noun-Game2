﻿// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

#region Namespaces

	using UnityEngine;
	using System.Collections;

	using UnityEngine.UI;

#endregion

// This class describe information of Item
[System.Serializable]
public class ItemInfo
{
	public int				m_ItemCount				= 0;
	public float			m_CoolDownTime			= 0;
	public float			m_RequiredCoolDownTime	= 0;
}

// This class describe information of Player
[System.Serializable]
public class PlayerInfo
{
	public bool				m_Alive					= true;
	public int				m_Score					= 0;
	public int				m_Coin					= 0;
	public int				m_HP					= 50;
	public int				m_MaxHP					= 50;
	public int				m_EXP					= 0;
	public int				m_NextLevelEXP			= 100;
	public int				m_Level					= 1;
	public ItemInfo			m_Item0;
	public ItemInfo			m_Item1;
	public ItemInfo			m_Item2;
}

// ######################################################################
// This class handles "Demo 04 - Landscape - Gameplay" and "Demo 04 - Portrait - Gameplay"
// ######################################################################

public class Demo04_GameplayAndLevelClear : MonoBehaviour
{
	#region Variables

		// Demo_Pause
		public Demo_Pause		m_Pause				= null;

		// Canvases
		public Canvas			m_CanvasGamePlay				= null;
		public Canvas			m_CanvasPause					= null;
		public Canvas			m_CanvasLevelClear				= null;

		// Demo_LevelClear
		public Demo_LevelClear	m_LevelClear					= null;


		// GameObject
		public GameObject		m_Gameplay						= null;
		public GameObject		m_PanelForButtons				= null;

		// Demo_Settings
		public Demo_Settings	m_Settings				= null;
	
		// UIs
		public GEAnim		m_Gameplay_MSG_Combo			= null;
		public GEAnim		m_Gameplay_MSG_GameOver			= null;
		public GEAnim		m_Gameplay_MSG_HEAL				= null;
		public GEAnim		m_Gameplay_MSG_LevelUp			= null;
		public GEAnim		m_Gameplay_MSG_Ready			= null;
		public GEAnim		m_Gameplay_MSG_Win				= null;
		public GEAnim		m_Gameplay_TopPanel				= null;
		public GEAnim		m_Gameplay_BottomPanel			= null;

		// Texts
		public Text				m_Gameplay_Text_Score			= null;
		public Text				m_Gameplay_Text_RemainingTime	= null;
		public Text				m_Gameplay_Text_Coin			= null;
		public Text				m_Gameplay_Text_PlayerHP		= null;
		public Text				m_Gameplay_Text_PlayerLevel		= null;
		public Text				m_Gameplay_Text_Item0			= null;
		public Text				m_Gameplay_Text_Item1			= null;
		public Text				m_Gameplay_Text_Item2			= null;
	
		// Sliders
		public Slider			m_Gameplay_Slider_Clock			= null;
		public Slider			m_Gameplay_Slider_HP			= null;
		public Slider			m_Gameplay_Slider_EXP			= null;

		// Images
		public Image			m_Gameplay_FilledImage_Item0			= null;
		public Image			m_Gameplay_FilledImage_Item1			= null;
		public Image			m_Gameplay_FilledImage_Item2			= null;

		// Buttons
		public Button			m_Gameplay_Button_Item0			= null;
		public Button			m_Gameplay_Button_Item1			= null;
		public Button			m_Gameplay_Button_Item2			= null;

		// Level Info
		public float			m_RemainingTimeSeconds				= 0;
		public float			m_MaxLimiteTimeSeconds				= 120;

		// Player Info
		public PlayerInfo		m_PlayerInfo;
	
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
			if (m_Gameplay.activeSelf == false)
				m_Gameplay.SetActive(true);
			if (m_Pause.gameObject.activeSelf == false)
				m_Pause.gameObject.SetActive(true);
			if (m_LevelClear.gameObject.activeSelf == false)
				m_LevelClear.gameObject.SetActive(true);
			if (m_Settings.gameObject.activeSelf == false)
				m_Settings.gameObject.SetActive(true);
		}

		// Use this for initialization
		void Start()
		{
			m_RemainingTimeSeconds = m_MaxLimiteTimeSeconds;

			m_Gameplay_Text_Score.text			= string.Format("{0:00000}", m_PlayerInfo.m_Score);
			m_Gameplay_Text_Coin.text			= string.Format("{0:n0}", m_PlayerInfo.m_Coin);
			m_PlayerInfo.m_EXP					= 0;
			m_Gameplay_Slider_EXP.value			= m_PlayerInfo.m_EXP/m_PlayerInfo.m_NextLevelEXP;
			m_Gameplay_Text_PlayerHP.text		= string.Format("{00:00}", m_PlayerInfo.m_HP.ToString());
			m_Gameplay_Text_PlayerLevel.text	= string.Format("{00:00}", m_PlayerInfo.m_Level.ToString());

			//m_PlayerInfo.m_Item0.m_CoolDownTime = m_PlayerInfo.m_Item0.m_RequiredCoolDownTime;
			//m_PlayerInfo.m_Item1.m_CoolDownTime = m_PlayerInfo.m_Item1.m_RequiredCoolDownTime;
			//m_PlayerInfo.m_Item2.m_CoolDownTime = m_PlayerInfo.m_Item2.m_RequiredCoolDownTime;
			UpdatePlayerItems();

			// Play MoveIn animations
			GEAnimSystem.Instance.MoveIn(m_Gameplay_TopPanel.transform, true);
			GEAnimSystem.Instance.MoveIn(m_Gameplay_BottomPanel.transform, true);

			StartCoroutine(ShowMSG(m_Gameplay_MSG_Ready));
		}
	
		// Update is called once per frame.
		void Update ()
		{
			m_RemainingTimeSeconds -= Time.deltaTime;
			if(m_RemainingTimeSeconds<0)
				m_RemainingTimeSeconds = m_MaxLimiteTimeSeconds;
			m_Gameplay_Text_RemainingTime.text = string.Format("{00:00}", (int)m_RemainingTimeSeconds/60) + ":" + string.Format("{00:00}", (int)m_RemainingTimeSeconds%60);
			float value_RemainingTime = (float) m_RemainingTimeSeconds / m_MaxLimiteTimeSeconds;
			m_Gameplay_Slider_Clock.value = value_RemainingTime;


			int Score = int.Parse(m_Gameplay_Text_Score.text);
			if(Score<m_PlayerInfo.m_Score)
			{
				Score+=7;
				if(Score>m_PlayerInfo.m_Score)
					Score=m_PlayerInfo.m_Score;
				m_Gameplay_Text_Score.text = string.Format("{0:00000}", Score);
			}
			else if(Score>m_PlayerInfo.m_Score)
			{
				Score-=1;
				if(Score<m_PlayerInfo.m_Score)
					Score=m_PlayerInfo.m_Score;
				m_Gameplay_Text_Score.text = string.Format("{0:00000}", Score);
			}


			int Coin = int.Parse(m_Gameplay_Text_Coin.text.Replace(",",""));
			if(Coin<m_PlayerInfo.m_Coin)
			{
				Coin+=3;
				if(Coin>m_PlayerInfo.m_Coin)
					Coin=m_PlayerInfo.m_Coin;
				m_Gameplay_Text_Coin.text = string.Format("{0:n0}", Coin);
			}


			float value_HP = (float) m_PlayerInfo.m_HP / (float) m_PlayerInfo.m_MaxHP;
			if(m_Gameplay_Slider_HP.value<value_HP)
			{
				m_Gameplay_Slider_HP.value += 0.01f;
				if(m_Gameplay_Slider_HP.value>value_HP)
					m_Gameplay_Slider_HP.value=value_HP;
			}
			else if(m_Gameplay_Slider_HP.value>value_HP)
			{
				m_Gameplay_Slider_HP.value -= 0.01f;
				if(m_Gameplay_Slider_HP.value<value_HP)
					m_Gameplay_Slider_HP.value=value_HP;
			}
			else
			{
				if(m_Gameplay_Slider_HP.value<=0.0f && m_PlayerInfo.m_Alive == true)
				{
					m_PlayerInfo.m_Alive = false;
					GamePlay_Button_MSG_GameOver();
				}
			}

			float value_EXP = (float) m_PlayerInfo.m_EXP / (float) m_PlayerInfo.m_NextLevelEXP;
			if(m_Gameplay_Slider_EXP.value<value_EXP)
			{
				m_Gameplay_Slider_EXP.value += 0.01f;
				if(m_Gameplay_Slider_EXP.value>value_EXP)
					m_Gameplay_Slider_EXP.value=value_EXP;

				if(m_Gameplay_Slider_EXP.value==1.0f)
				{
					StartCoroutine(PlayerLevelUp());
				}
			}
			else if(m_Gameplay_Slider_EXP.value>value_EXP)
			{
				m_Gameplay_Slider_EXP.value -= 0.01f;
				if(m_Gameplay_Slider_EXP.value<value_EXP)
					m_Gameplay_Slider_EXP.value=value_EXP;
			}

			if(m_PlayerInfo.m_Item0.m_ItemCount>0)
			{
				if(m_PlayerInfo.m_Item0.m_CoolDownTime < m_PlayerInfo.m_Item0.m_RequiredCoolDownTime)
				{ 
					m_PlayerInfo.m_Item0.m_CoolDownTime += Time.deltaTime;
					if(m_PlayerInfo.m_Item0.m_CoolDownTime >= m_PlayerInfo.m_Item0.m_RequiredCoolDownTime)
					{ 
						m_PlayerInfo.m_Item0.m_CoolDownTime = m_PlayerInfo.m_Item0.m_RequiredCoolDownTime;
						m_Gameplay_Button_Item0.interactable = true;

						ParticleController.Instance.CreateParticle(m_Gameplay_FilledImage_Item0.gameObject, ParticleController.Instance.m_PrefabFullFIll);
					}
				}
			}
			else
			{ 
				if(m_Gameplay_Button_Item0.interactable==true)
				{ 
					m_Gameplay_Button_Item0.interactable = false;
				}
			}
			m_Gameplay_FilledImage_Item0.fillAmount = m_PlayerInfo.m_Item0.m_CoolDownTime/m_PlayerInfo.m_Item0.m_RequiredCoolDownTime;

			if(m_PlayerInfo.m_Item1.m_ItemCount>0)
			{
				if(m_PlayerInfo.m_Item1.m_CoolDownTime < m_PlayerInfo.m_Item1.m_RequiredCoolDownTime)
				{ 
					m_PlayerInfo.m_Item1.m_CoolDownTime += Time.deltaTime;
					if(m_PlayerInfo.m_Item1.m_CoolDownTime >= m_PlayerInfo.m_Item1.m_RequiredCoolDownTime)
					{ 
						m_PlayerInfo.m_Item1.m_CoolDownTime = m_PlayerInfo.m_Item1.m_RequiredCoolDownTime;
						m_Gameplay_Button_Item1.interactable = true;

						ParticleController.Instance.CreateParticle(m_Gameplay_FilledImage_Item1.gameObject, ParticleController.Instance.m_PrefabFullFIll);
					}
				}
			}
			else
			{ 
				if(m_Gameplay_Button_Item1.interactable==true)
				{ 
					m_Gameplay_Button_Item1.interactable = false;
				}
			}
			m_Gameplay_FilledImage_Item1.fillAmount = m_PlayerInfo.m_Item1.m_CoolDownTime/m_PlayerInfo.m_Item1.m_RequiredCoolDownTime;

			if(m_PlayerInfo.m_Item2.m_ItemCount>0)
			{
				if(m_PlayerInfo.m_Item2.m_CoolDownTime < m_PlayerInfo.m_Item2.m_RequiredCoolDownTime)
				{ 
					m_PlayerInfo.m_Item2.m_CoolDownTime += Time.deltaTime;
					if(m_PlayerInfo.m_Item2.m_CoolDownTime >= m_PlayerInfo.m_Item2.m_RequiredCoolDownTime)
					{ 
						m_PlayerInfo.m_Item2.m_CoolDownTime = m_PlayerInfo.m_Item2.m_RequiredCoolDownTime;
						m_Gameplay_Button_Item2.interactable = true;

						ParticleController.Instance.CreateParticle(m_Gameplay_FilledImage_Item2.gameObject, ParticleController.Instance.m_PrefabFullFIll);
					}
				}
			}
			else
			{ 
				if(m_Gameplay_Button_Item2.interactable==true)
				{ 
					m_Gameplay_Button_Item2.interactable = false;
				}
			}
			m_Gameplay_FilledImage_Item2.fillAmount = m_PlayerInfo.m_Item2.m_CoolDownTime/m_PlayerInfo.m_Item2.m_RequiredCoolDownTime;
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Respond functions
	// ######################################################################

	#region Respond functions

		public void GamePlay_Button_Score(GameObject goButton)
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);

			m_PlayerInfo.m_Score += Random.Range(1, 10) * 50;
		}

		public void GamePlay_Button_Coin(GameObject goButton)
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);

			m_PlayerInfo.m_Coin += 100;
		}

		public void GamePlay_Button_Pause()
		{
			// Play Pause button sound
			SoundController.Instance.Play_SoundPause();
		
			// Show this panel
			m_Pause.Show();
		}

		public void GamePlay_Button_HP(GameObject goButton)
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);

			m_PlayerInfo.m_HP -= 25;
			if(m_PlayerInfo.m_HP<0)
			{ 
				m_PlayerInfo.m_HP=0;
			}
			m_Gameplay_Text_PlayerHP.text = string.Format("{00:00}", m_PlayerInfo.m_HP.ToString());
		}

		public void GamePlay_Button_EXP(GameObject goButton)
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabButton_01);

			m_PlayerInfo.m_EXP += m_PlayerInfo.m_NextLevelEXP/2;
			if(m_PlayerInfo.m_EXP>=m_PlayerInfo.m_NextLevelEXP)
			{ 
				m_PlayerInfo.m_EXP=m_PlayerInfo.m_NextLevelEXP;
			}
		}

		public void GamePlay_Button_Item0(GameObject goButton)
		{
			if(m_PlayerInfo.m_Item0.m_ItemCount>0)
			{
				StartCoroutine(PlayerHeal());
			
				// Play Move Out animation
				GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
				StartCoroutine(ShowMSG(m_Gameplay_MSG_HEAL));

				// Play Click sound
				SoundController.Instance.Play_SoundClick();
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabUseItem);
			}
			else
			{ 
				// Play Disable button sound
				SoundController.Instance.Play_SoundDisable();
			}
		
			// Decrease Item
			m_PlayerInfo.m_Item0.m_ItemCount --;
			if(m_PlayerInfo.m_Item0.m_ItemCount<0)
			{ 
				m_PlayerInfo.m_Item0.m_ItemCount=0;
			}

			// Update Text
			m_Gameplay_Text_Item0.text = m_PlayerInfo.m_Item0.m_ItemCount.ToString();

			// Reset cooldown time
			m_PlayerInfo.m_Item0.m_CoolDownTime = 0;
			m_Gameplay_FilledImage_Item0.fillAmount = m_PlayerInfo.m_Item0.m_CoolDownTime / m_PlayerInfo.m_Item0.m_RequiredCoolDownTime;

			// Deactivate button
			m_Gameplay_Button_Item0.interactable = false;

		}

		public void GamePlay_Button_Item1(GameObject goButton)
		{		
			if(m_PlayerInfo.m_Item1.m_ItemCount>0)
			{ 	
				// Play Move Out animation
				GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
				StartCoroutine(ShowMSG(m_Gameplay_MSG_HEAL));

				// Play Click sound
				SoundController.Instance.Play_SoundClick();
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabUseItem);
			}
			else
			{ 
				// Play Disable button sound
				SoundController.Instance.Play_SoundDisable();
			}

			// Decrease Item
			m_PlayerInfo.m_Item1.m_ItemCount --;
			if(m_PlayerInfo.m_Item1.m_ItemCount<0)
			{ 
				m_PlayerInfo.m_Item1.m_ItemCount=0;
			}
		
			// Update Text
			m_Gameplay_Text_Item1.text = m_PlayerInfo.m_Item1.m_ItemCount.ToString();
		
			// Reset cooldown time
			m_PlayerInfo.m_Item1.m_CoolDownTime = 0;
			m_Gameplay_FilledImage_Item1.fillAmount = m_PlayerInfo.m_Item1.m_CoolDownTime / m_PlayerInfo.m_Item1.m_RequiredCoolDownTime;

			// Deactivate button
			m_Gameplay_Button_Item1.interactable = false;
		}

		public void GamePlay_Button_Item2(GameObject goButton)
		{
			if(m_PlayerInfo.m_Item2.m_ItemCount>0)
			{ 
				// Play Click sound
				SoundController.Instance.Play_SoundClick();
				ParticleController.Instance.CreateParticle(goButton, ParticleController.Instance.m_PrefabUseItem);
			}
			else
			{ 
				// Play Disable button sound
				SoundController.Instance.Play_SoundDisable();
			}
		
			// Decrease Item
			m_PlayerInfo.m_Item2.m_ItemCount --;
			if(m_PlayerInfo.m_Item2.m_ItemCount<0)
			{ 
				m_PlayerInfo.m_Item2.m_ItemCount=0;
			}
		
			// Update Text
			m_Gameplay_Text_Item2.text = m_PlayerInfo.m_Item2.m_ItemCount.ToString();
		
			// Reset cooldown time
			m_PlayerInfo.m_Item2.m_CoolDownTime = 0;
			m_Gameplay_FilledImage_Item2.fillAmount = m_PlayerInfo.m_Item2.m_CoolDownTime / m_PlayerInfo.m_Item2.m_RequiredCoolDownTime;

			// Deactivate button
			m_Gameplay_Button_Item2.interactable = false;
		}

	#endregion //Respond functions	

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		IEnumerator PlayerHeal()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);
			m_PlayerInfo.m_HP += 30;
			if(m_PlayerInfo.m_HP>m_PlayerInfo.m_MaxHP)
			{
				m_PlayerInfo.m_HP=m_PlayerInfo.m_MaxHP;
			}
			m_Gameplay_Text_PlayerHP.text = string.Format("{00:00}", m_PlayerInfo.m_HP.ToString());
		}

		IEnumerator ShowMSG(GEAnim pGUIANimFREE)
		{
			// Disable GraphicRaycasters of Canvas in m_Gameplay
			GEAnimSystem.Instance.SetGraphicRaycasterEnable(m_Gameplay, false);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);
		
			if(pGUIANimFREE.gameObject.activeSelf==false)
				pGUIANimFREE.gameObject.SetActive(true);

			// Reset all animations' information of before replay
			pGUIANimFREE.Reset();

			// Play MoveIn animation
			pGUIANimFREE.MoveIn();
		
			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(pGUIANimFREE.transform);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.5f);

			StartCoroutine(HideMSG(pGUIANimFREE));
		}

		IEnumerator HideMSG(GEAnim pGUIANimFREE)
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(2.5f);

			if(pGUIANimFREE.gameObject.activeSelf==false)
				pGUIANimFREE.gameObject.SetActive(true);

			// Play Move Out animation
			pGUIANimFREE.MoveOut();

			// Stop particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.StopParticle(pGUIANimFREE.transform);

			// Enable GraphicRaycasters of Canvas in m_Gameplay
			GEAnimSystem.Instance.SetGraphicRaycasterEnable(m_Gameplay, true);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(1.5f);

			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(m_PanelForButtons.transform, true);
		}

		void UpdatePlayerItems()
		{
			m_Gameplay_Text_Item0.text = m_PlayerInfo.m_Item0.m_ItemCount.ToString();
			m_Gameplay_Text_Item1.text = m_PlayerInfo.m_Item1.m_ItemCount.ToString();
			m_Gameplay_Text_Item2.text = m_PlayerInfo.m_Item2.m_ItemCount.ToString();

			if(m_PlayerInfo.m_Item0.m_ItemCount>0)
			{
				m_Gameplay_FilledImage_Item0.fillAmount = m_PlayerInfo.m_Item0.m_CoolDownTime / m_PlayerInfo.m_Item0.m_RequiredCoolDownTime;
			}
			else
			{
				m_Gameplay_FilledImage_Item0.fillAmount = 0.0f;
			}
		
			if(m_PlayerInfo.m_Item1.m_ItemCount>0)
			{
				m_Gameplay_FilledImage_Item1.fillAmount = m_PlayerInfo.m_Item1.m_CoolDownTime / m_PlayerInfo.m_Item1.m_RequiredCoolDownTime;
			}
			else
			{
				m_Gameplay_FilledImage_Item1.fillAmount = 0.0f;
			}

			if(m_PlayerInfo.m_Item2.m_ItemCount>0)
			{
				m_Gameplay_FilledImage_Item2.fillAmount = m_PlayerInfo.m_Item2.m_CoolDownTime / m_PlayerInfo.m_Item2.m_RequiredCoolDownTime;
			}
			else
			{
				m_Gameplay_FilledImage_Item2.fillAmount = 0.0f;
			}

			if(m_PlayerInfo.m_Item0.m_ItemCount>0 && m_Gameplay_FilledImage_Item0.fillAmount==1.0f)
				m_Gameplay_Button_Item0.interactable = true;
			else
				m_Gameplay_Button_Item0.interactable = false;

			if(m_PlayerInfo.m_Item1.m_ItemCount>0 && m_Gameplay_FilledImage_Item1.fillAmount==1.0f)
				m_Gameplay_Button_Item1.interactable = true;
			else
				m_Gameplay_Button_Item1.interactable = false;

			if(m_PlayerInfo.m_Item2.m_ItemCount>0 && m_Gameplay_FilledImage_Item2.fillAmount==1.0f)
				m_Gameplay_Button_Item2.interactable = true;
			else
				m_Gameplay_Button_Item2.interactable = false;
		}	
		
		public void GamePlay_Button_MSG_Ready()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
			StartCoroutine(ShowMSG(m_Gameplay_MSG_Ready));
		}
	
		public void GamePlay_Button_MSG_Combo()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
			StartCoroutine(ShowMSG(m_Gameplay_MSG_Combo));
		}
	
		public void GamePlay_Button_MSG_Win()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
			StartCoroutine(ShowMSG(m_Gameplay_MSG_Win));
		}
	
		public void GamePlay_Button_MSG_GameOver()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();

			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
			StartCoroutine(Show_MSG_GameOver());
		}
	
		public void GamePlay_Button_MSG_LevelClear()
		{
			// Play Click sound
			SoundController.Instance.Play_SoundClick();
		
			// Play Move Out animations
			GEAnimSystem.Instance.MoveOut(m_Gameplay.transform, true);
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);

			m_LevelClear.Show(m_PlayerInfo.m_Score, m_RemainingTimeSeconds, m_PlayerInfo.m_Coin);

		}

		IEnumerator Show_MSG_GameOver()
		{
			// Disable GraphicRaycasters of Canvas in m_Gameplay
			GEAnimSystem.Instance.SetGraphicRaycasterEnable(m_Gameplay, false);
			m_Gameplay_MSG_GameOver.gameObject.SetActive(true);

			// Play MoveIn animation
			m_Gameplay_MSG_GameOver.MoveIn();

			// Play particles in the hierarchy of given transfrom
			GEAnimSystem.Instance.PlayParticle(m_Gameplay_MSG_GameOver.transform);
		
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(4.0f);

			m_Gameplay_MSG_GameOver.gameObject.SetActive(true);

			// Play Move Out animation
			m_Gameplay_MSG_GameOver.MoveOut();

			// Keep particles stay alive until it finished playing.
			GEAnimSystem.Instance.DontDestroyParticleWhenLoadNewScene(m_Gameplay_MSG_GameOver.transform, true);
		
			// Load next scene according to orientation of current scene.
			string CurrentLevel = Application.loadedLevelName;
			string OrientationName = "Portrait";
			if(CurrentLevel.Contains("Landscape")==true)
				OrientationName = "Landscape";
			GEAnimSystem.Instance.LoadLevel("Demo 03 - " + OrientationName + " - Level Select", 1.0f);
		}

		IEnumerator PlayerLevelUp()
		{
			// Creates a yield instruction to wait for a given number of seconds
			// http://docs.unity3d.com/400/Documentation/ScriptReference/WaitForSeconds.WaitForSeconds.html
			yield return new WaitForSeconds(0.25f);

			m_PlayerInfo.m_Level++;
			m_PlayerInfo.m_EXP = 0;
			m_Gameplay_Slider_EXP.value = m_PlayerInfo.m_EXP;

			m_Gameplay_Text_PlayerLevel.text = string.Format("{00:00}", m_PlayerInfo.m_Level.ToString());
		
			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(m_PanelForButtons.transform, true);
			StartCoroutine(ShowMSG(m_Gameplay_MSG_LevelUp));
		}

	#endregion //UI functions
}
