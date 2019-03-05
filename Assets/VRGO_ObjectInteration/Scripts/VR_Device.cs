using UnityEngine;

namespace VrGrabber
{

	public static class VRDevice
	{
		public enum ControllerSide
		{
			Left,
			Right,
		}

		private static OVRInput.Controller GetOVRController(ControllerSide side)
		{
			return (side == ControllerSide.Left) ?
				OVRInput.Controller.LTrackedRemote :
				OVRInput.Controller.RTrackedRemote;
		}

		public static Vector3 GetLocalPosition(ControllerSide side)
		{ 
			return OVRInput.GetLocalControllerPosition(GetOVRController(side));
		}

		public static Quaternion GetLocalRotation(ControllerSide side)
		{ 
			return OVRInput.GetLocalControllerRotation(GetOVRController(side));
		}

		public static float GetHold(ControllerSide side)
		{ 
			return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, GetOVRController(side));
		}

		public static bool GetHover(ControllerSide side)
		{
			return OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, GetOVRController(side));
		}

		public static bool GetClick(ControllerSide side)
		{ 
			return OVRInput.Get(OVRInput.Button.One, GetOVRController(side));
		}

		public static Vector2 GetCoord(ControllerSide side)
		{ 
			return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, GetOVRController(side));
		}
	}
}