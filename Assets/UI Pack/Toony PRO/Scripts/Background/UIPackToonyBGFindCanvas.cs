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
// This class is use with Canvas_Background_Landscape and Canvas_Background_Portrait.
// It searchs for Canvas in this object and also seach for a Camera that render "Ignore Raycast" layer then set Camera to the worldCamera variable of the Canvas.
// ######################################################################

public class UIPackToonyBGFindCanvas : MonoBehaviour
{
	
	// ######################################################################
	// Variables
	// ######################################################################
	#region Variables
	
		Canvas m_Canvas = null;

	#endregion //Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Awake ()
		{
			// Search for Canvas component of this object.
			if(m_Canvas==null)
				m_Canvas = gameObject.GetComponent<Canvas>();
			
			// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
			if(m_Canvas!=null)
			{ 
				if(m_Canvas.worldCamera == null)
				{ 
					FindCamera();
				}
			}
		}

		// Use this for initialization
		void Start ()
		{
			// Search for Canvas component of this object.
			if(m_Canvas==null)
				m_Canvas = gameObject.GetComponent<Canvas>();

			// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
			if(m_Canvas!=null)
			{ 
				if(m_Canvas.worldCamera == null)
				{ 
					FindCamera();
				}
			}
		}
	
		// Update is called once per frame
		void Update ()
		{			
			// If there is Canvas component then assign a Camera that render "Ignore Raycast" layer to it.
			if(m_Canvas!=null)
			{ 
				if(m_Canvas.worldCamera == null)
				{ 
					FindCamera();
				}
			}
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Utility functions
	// ######################################################################

	#region Utility functions

		// Look for a Camera that render "Ignore Raycast" layer 
		void FindCamera()
		{ 
			Camera[] pCameraList = GameObject.FindObjectsOfType<Camera>();

			foreach(Camera child in pCameraList)
			{
				if(child.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
				{
					// Assign a Camera that render "Ignore Raycast" layer to worldCamera variable of the Canvas
					m_Canvas.worldCamera = child;
				}
			}
		
		}

	#endregion //Utility functions

}
