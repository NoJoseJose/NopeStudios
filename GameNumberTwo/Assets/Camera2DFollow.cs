using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float MaxCameraYPos = 15f;
        public float MinCameraYPos = 5f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPosx;
        private Vector3 m_LookAheadPosz;

        private float nextTimeToSearch = 0;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            if (target == null)
            {
                FindPlayer();
                return;
            }
            // only update lookahead pos if accelerating or changed direction
            Vector3 diff = target.position - m_LastTargetPosition;
            float xMoveDelta = diff.x;
            float zMoveDelta = diff.z;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold || Math.Abs(zMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPosx = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
                m_LookAheadPosz = lookAheadFactor * Vector3.right * Mathf.Sign(zMoveDelta);
            }
            else
            {
                m_LookAheadPosx = Vector3.MoveTowards(m_LookAheadPosx, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
                m_LookAheadPosz = Vector3.MoveTowards(m_LookAheadPosz, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPosX = target.position + m_LookAheadPosx + Vector3.forward*m_OffsetZ;
            Vector3 aheadTargetPosZ = target.position + m_LookAheadPosz + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(aheadTargetPosX, aheadTargetPosZ, ref m_CurrentVelocity, damping);

            newPos = new Vector3(newPos.x, Mathf.Clamp(newPos.y, MinCameraYPos, MaxCameraYPos), newPos.z);

            transform.position = newPos;

            m_LastTargetPosition = target.position;
        }

        private void FindPlayer()
        {
            if (nextTimeToSearch <= Time.time)
            {
                GameObject result = GameObject.FindGameObjectWithTag("Player");
           

            if (result != null)
                target = result.transform;
            nextTimeToSearch = Time.time + 0.5f;
            }
        }
    }
}
