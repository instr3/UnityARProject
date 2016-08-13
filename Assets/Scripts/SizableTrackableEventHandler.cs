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
    public class SizableTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES
        public float fadetime = 3f;
        private bool found = false;
        private bool recentlyfound = false;
        private float accumlatetime = 0f;

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS




        void Update()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            //  Debug.Log(Time.deltaTime);
            if (found)
                accumlatetime = 0;
            else if (recentlyfound)
                accumlatetime += Time.deltaTime;
            //    if (accumlatetime >= fadetime)
            //   {
            //     Debug.Log("time to fade out");
            //    accumlatetime = 0f;
            //   recentlyfound = false;

            if (recentlyfound)
            {
                foreach (Transform child in transform)
                {

                    if (found)
                    {
                        if (child.gameObject.transform.localScale.x < 1)
                            child.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                    }
                    else
                    {
                        if (child.gameObject.transform.localScale.x > 0)
                            child.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                        if (child.gameObject.transform.localScale.x <= 0)
                        {
                            foreach (Renderer component in rendererComponents)
                            {
                                component.enabled = false;
                            }
                            foreach (Collider component in colliderComponents)
                            {
                                component.enabled = false;
                            }
                        }

                    }
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
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
            foreach (Transform child in transform)
            {
                Debug.Log(child.gameObject.name);
            }

            recentlyfound = true;
            found = true;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");


        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            if (!recentlyfound)
            {
                foreach (Renderer component in rendererComponents)
                {
                    component.enabled = false;
                }

                // Disable colliders:
                foreach (Collider component in colliderComponents)
                {
                    component.enabled = false;
                }
            }
            found = false;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
