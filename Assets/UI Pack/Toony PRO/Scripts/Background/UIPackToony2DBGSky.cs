// UI Pack : Toony PRO
// Version: 1.0.5
// Author: Gold Experience Team (http://ge-team.com/pages/unity-3d/)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to support e-mail (geteamdev@gmail.com)

#region Namespaces

	using UnityEngine;
	using System.Collections;

#endregion

// ######################################################################
// This class strengths sky sprite, fit the background.
// ######################################################################

public class UIPackToony2DBGSky : MonoBehaviour
{

	// ######################################################################
	// Variables
	// ######################################################################
	#region Variables

		// If you have error at this line on Unity 5.x, please make sure that you are using Unity 5.x with a valid license.
		RectTransform	m_RectTransform		= null; 

		Vector2			m_OldSize;
	
	#endregion //Variables

	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Use this for initialization
		void Start ()
		{
			InitMe();
		}

		// Update is called once per frame
		void Update ()
		{
			// Check if the screen resolution was changed
			if(m_CameraRightEdge!=m_ParentCanvasRectTransform.rect.width/2 || m_CameraTopEdge!=m_ParentCanvasRectTransform.rect.height/2)
			{
				InitMe();
			}
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Utility functions
	// ######################################################################

	#region Utility functions
	
		// Initial sprite
		void InitMe()
		{
			// Search for parent Canvas and calculate camera view size
			FindParentCanvasAndCameraArea();
		
			m_RectTransform = (RectTransform) this.transform;
			this.transform.localScale = new Vector3((m_CameraRightEdge-m_CameraLeftEdge)/m_RectTransform.rect.width, (m_CameraTopEdge-m_CameraBottomEdge)/m_RectTransform.rect.height, 1);
		}
	
		// This class need Canvas to work properly.
		Canvas	m_Parent_Canvas = null;

		// Edge position of camera perspective
		float m_CameraLeftEdge;
		float m_CameraRightEdge;
		float m_CameraTopEdge;
		float m_CameraBottomEdge;

		// If you have error at this line on Unity 5.x, please make sure that you are using Unity 5.x with a valid license.
		RectTransform	m_ParentCanvasRectTransform		= null;

		// Search for parent Canvas and calculate view size of camera 
		void FindParentCanvasAndCameraArea()
		{
			// Search for the parent Canvas
			if(m_Parent_Canvas==null)
				m_Parent_Canvas = GEAnimSystem.Instance.GetParent_Canvas(transform);

			// Calculate view size of camera 
			if(m_Parent_Canvas!=null)
			{
				m_ParentCanvasRectTransform = m_Parent_Canvas.GetComponent<RectTransform>();
			
				m_CameraRightEdge = (m_ParentCanvasRectTransform.rect.width/2);
				m_CameraLeftEdge = -m_CameraRightEdge;
				m_CameraTopEdge = (m_ParentCanvasRectTransform.rect.height/2);
				m_CameraBottomEdge = -m_CameraTopEdge;
			}

		}

	#endregion //Utility functions

}