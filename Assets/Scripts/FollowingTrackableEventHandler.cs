/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class FollowingTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES
        public Transform werewolfTransform;
        public Vector3 targetPosition;
        private float separateDistance;
        private Animator animator;
        private bool found = false, separated = false;
        private Vector3 smoothVelocity = Vector3.zero;
        private Vector3 arCameraPosition;
        public float initHeight;
        private Vector3 finalPosition;

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
            arCameraPosition = MainController.ARCamera.GetComponentInChildren<Camera>().transform.position;
            separateDistance = Vector3.Distance(arCameraPosition, targetPosition);
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        void Update()
        {
            if(found)
            {
                Debug.Log(separateDistance);
                if(Vector3.Distance(werewolfTransform.position, arCameraPosition) >separateDistance)
                {
                    separated = true;
                    animator.SetBool("Walk", true);
                    werewolfTransform.SetParent(null);
                    smoothVelocity = new Vector3();
                    finalPosition = targetPosition - Vector3.up * initHeight;
                }
                if(separated)
                {
                    Debug.Log(werewolfTransform.position.ToString() + ":" + finalPosition.ToString() + ":" + smoothVelocity);
                    werewolfTransform.position =
                        Vector3.SmoothDamp(werewolfTransform.position, finalPosition, ref smoothVelocity, 3f,2f,Time.deltaTime);
                }
            }
        }

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                animator.SetBool("Hide", false);
                animator.SetBool("Show", true);
                found = true;
            }
            else if(!separated)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Show", false);
                animator.SetBool("Hide", true);
                found = false;
            }
        }

        #endregion // PUBLIC_METHODS



    }
}
