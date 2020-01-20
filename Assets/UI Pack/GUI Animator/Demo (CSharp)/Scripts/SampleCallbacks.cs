// GUI Animator for Unity UI version: 0.9.93 (Product page: http://ge-team.com/pages/unity-3d/gui-animator-for-unity-ui/)
//
// Author:			Gold Experience Team (http://www.ge-team.com/pages/)
// Products:		http://ge-team.com/pages/unity-3d/
// Support:			geteamdev@gmail.com
// Documentation:	http://ge-team.com/pages/unity-3d/gui-animator-for-unity-ui/gui-animator-for-unity-ui-documentation/
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion

/**************
* SampleCallbacks class
* This class handles "Sample Callbacks" scene.
* It automatically plays animation (using "Auto Animaiton" flag in GUIAnimSystem in the scene.
* 
* When Image UI plays In animation, it moves from top of screen to the center. 
* You will see the messages in Console tab, these messages are from MoveIn_Started() and MoveIn_Finished() functions.
* 
* When Image UI plays Out animation, it moves from the center to bottom of screen. 
* You will see the messages in Console tab, these messages are from MoveOut_Started() and MoveOut_Finished() functions.
* 
* NOTE this scene is just a sample of how to use the callback, 
* we do use four functions (MoveIn_Started, MoveIn_Finished, MoveOut_Started, MoveOut_Finished). Others are left for your practices.
**************/

public class SampleCallbacks : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// - - - - - - -  -- - -  - - - - - - - - -  -- - -  - -
	// In Sample Callbacks scene we do use these four functions
		public void MoveIn_Started()
		{ 
			Debug.Log("MoveIn Started at "+Time.realtimeSinceStartup);
		}

		public void MoveIn_Finished()
		{ 
			Debug.Log("MoveIn Finished at "+Time.realtimeSinceStartup);
		}

		public void MoveOut_Started()
		{ 
			Debug.Log("MoveOut Started at "+Time.realtimeSinceStartup);
		}

		public void MoveOut_Finished()
		{ 
			Debug.Log("MoveOut Finished at "+Time.realtimeSinceStartup);
		}
	// - - - - - - -  -- - -  - - - - - - - - -  -- - -  - -

	// - - - - - - -  -- - -  - - - - - - - - -  -- - -  - -
	// You should try these.
		public void RotateIn_Started()
		{ 
			Debug.Log("RotateIn Started at "+Time.realtimeSinceStartup);
		}

		public void RotateIn_Finished()
		{ 
			Debug.Log("RotateIn Finished at "+Time.realtimeSinceStartup);
		}

		public void RotateOut_Started()
		{ 
			Debug.Log("RotateOut Started at "+Time.realtimeSinceStartup);
		}

		public void RotateOut_Finished()
		{ 
			Debug.Log("RotateOut Finished at "+Time.realtimeSinceStartup);
		}

		public void ScaleIn_Started()
		{ 
			Debug.Log("ScaleIn Started at "+Time.realtimeSinceStartup);
		}

		public void ScaleIn_Finished()
		{ 
			Debug.Log("ScaleIn Finished at "+Time.realtimeSinceStartup);
		}

		public void ScaleOut_Started()
		{ 
			Debug.Log("ScaleOut Started at "+Time.realtimeSinceStartup);
		}

		public void ScaleOut_Finished()
		{ 
			Debug.Log("ScaleOut Finished at "+Time.realtimeSinceStartup);
		}

		public void FadeIn_Started()
		{ 
			Debug.Log("FadeIn Started at "+Time.realtimeSinceStartup);
		}

		public void FadeIn_Finished()
		{ 
			Debug.Log("FadeIn Finished at "+Time.realtimeSinceStartup);
		}

		public void FadeOut_Started()
		{ 
			Debug.Log("FadeOut Started at "+Time.realtimeSinceStartup);
		}

		public void FadeOut_Finished()
		{ 
			Debug.Log("FadeOut Finished at "+Time.realtimeSinceStartup);
		}
}
