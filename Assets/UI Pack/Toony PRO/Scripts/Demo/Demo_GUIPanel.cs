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
// This class control panel such as Pause, Settings, News, Helps.
// It disables Graphic Raycasters that ordered as array in m_CanvasesToDeactivated then it enables self Graphic Raycaster.
// ######################################################################

public class Demo_GUIPanel : MonoBehaviour {
	
	#region Variables

		public Canvas[]			m_CanvasesToDeactivated;						// Canvases in this array will be ignored from raycasting.
		public bool				m_ReactivateCanvasesWhenFinished	= true;

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
		}
	
		// Update is called once per frame
		void Update ()
		{	
		}
	
	#endregion //MonoBehaviour Functions

	// ######################################################################
	// UI functions
	// ######################################################################

	#region UI functions

		// Show this panel
		public void Show()
		{
			if(m_CanvasesToDeactivated!=null)
			{
				if(m_CanvasesToDeactivated.Length>0)
				{
					// Disable GraphicRaycaster of Canvas in m_CanvasesToDeactivated
					foreach (Canvas child in m_CanvasesToDeactivated)
					{
						// Disable GraphicRaycasters of Canvas in child of m_CanvasesToDeactivated
						GEAnimSystem.Instance.SetGraphicRaycasterEnable(child, false);
					}
				}
			}

			// Enable GraphicRaycasters of Canvas in this.gameObject
			GEAnimSystem.Instance.SetGraphicRaycasterEnable(this.gameObject, true);

			// Play MoveIn animation
			GEAnimSystem.Instance.MoveIn(this.transform, true);
		}

		// Hide this panel
		public void Hide()
		{
			// Disable GraphicRaycasters of Canvas in this.gameObject
			GEAnimSystem.Instance.SetGraphicRaycasterEnable(this.gameObject, false);

			if(m_CanvasesToDeactivated!=null && m_ReactivateCanvasesWhenFinished==true)
			{ 
				if(m_CanvasesToDeactivated.Length>0)
				{
					foreach (Canvas child in m_CanvasesToDeactivated)		
					{ 
						// Enable GraphicRaycasters of Canvas in child of m_CanvasesToDeactivated
						GEAnimSystem.Instance.SetGraphicRaycasterEnable(child, true);
					}
				}
			}
			
			// Play Move Out animation
			GEAnimSystem.Instance.MoveOut(this.transform, true);
		}
	
	#endregion //UI functions
}
