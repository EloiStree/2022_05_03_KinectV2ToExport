using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;


public class JointToDebugPosition : MonoBehaviour
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("Kinect joint that is going to be overlayed.")]
	public KinectInterop.JointType trackedJoint = KinectInterop.JointType.Head;
	public bool m_flipX = true;

	public Transform m_inKinectZonePositionDebug;
	public Transform m_rootToDisplay;

	[Header("Debug")]
	public bool m_isJoinTracked;
	public Vector3 m_joinInKinectSpacePosition;

	public void Start()
	{

		if (m_inKinectZonePositionDebug && m_rootToDisplay)
			m_inKinectZonePositionDebug.parent = m_rootToDisplay;
	}
	void Update()
	{
		KinectManager manager = KinectManager.Instance;
		if (manager && manager.IsInitialized())
		{
			long userId = manager.GetUserIdByIndex(playerIndex);
			int iJointIndex = (int)trackedJoint;
			 m_isJoinTracked = manager.IsJointTracked(userId, iJointIndex);
			if (m_inKinectZonePositionDebug)
				m_inKinectZonePositionDebug.gameObject.SetActive(m_isJoinTracked);
			if (m_isJoinTracked)
			{
				m_joinInKinectSpacePosition = manager.GetJointKinectPosition(userId, iJointIndex);
				if (m_flipX)
					m_joinInKinectSpacePosition = new Vector3(
						-m_joinInKinectSpacePosition.x,
						m_joinInKinectSpacePosition.y,
						m_joinInKinectSpacePosition.z
						);
				if (m_joinInKinectSpacePosition != Vector3.zero)
				{
					if (m_inKinectZonePositionDebug)
					{

						m_inKinectZonePositionDebug.localPosition = m_joinInKinectSpacePosition;
					}
				}
			}
			else
			{
				if (m_inKinectZonePositionDebug)
					m_inKinectZonePositionDebug.localPosition = Vector3.zero;

			}

		}
	}
}
