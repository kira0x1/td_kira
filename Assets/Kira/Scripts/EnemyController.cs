using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Splines;
using Interpolators = UnityEngine.Splines.Interpolators;
using Quaternion = UnityEngine.Quaternion;

namespace Unity.Splines.Examples
{
    public class AnimateCarAlongSpline : MonoBehaviour
    {
        [SerializeField] private SplineContainer m_SplineContainer;
        [SerializeField] Transform m_mobTransform;
        [SerializeField] SplineData<float> m_Speed = new SplineData<float>();
        [SerializeField] private SplineData<float3> m_Tilt = new SplineData<float3>();

        private float m_CurrentOffset;
        private float m_CurrentSpeed;
        private float m_SplineLength;
        private Spline m_Spline;

        private void Start()
        {
            Assert.IsNotNull(m_SplineContainer);

            m_Spline = m_SplineContainer.Spline;
            m_SplineLength = m_Spline.GetLength();
            m_CurrentOffset = 0f;
        }

        private void OnValidate()
        {
            if (m_Speed != null)
            {
                for (int index = 0; index < m_Speed.Count; index++)
                {
                    DataPoint<float> data = m_Speed[index];

                    //We don't want to have a value that is negative or null as it might block the simulation
                    if (data.Value <= 0)
                    {
                        data.Value = Mathf.Max(0f, m_Speed.DefaultValue);
                        m_Speed[index] = data;
                    }
                }
            }

            if (m_Tilt != null)
            {
                for (int index = 0; index < m_Tilt.Count; index++)
                {
                    DataPoint<float3> data = m_Tilt[index];

                    //We don't want to have a up vector of magnitude 0
                    if (math.length(data.Value) == 0)
                    {
                        data.Value = m_Tilt.DefaultValue;
                        m_Tilt[index] = data;
                    }
                }
            }
        }

        private void Update()
        {
            m_CurrentOffset = (m_CurrentOffset + m_CurrentSpeed * Time.deltaTime / m_SplineLength) % 1f;

            if (m_Speed.Count > 0)
            {
                m_CurrentSpeed = m_Speed.Evaluate(m_Spline, m_CurrentOffset, PathIndexUnit.Normalized, new Interpolators.LerpFloat());
            }
            else
            {
                m_CurrentSpeed = m_Speed.DefaultValue;
            }

            float3 posOnSplineLocal = m_Spline.EvaluatePosition(m_CurrentOffset);
            float3 direction = m_Spline.EvaluateTangent(m_CurrentOffset);
            float3 upSplineDirection = m_Spline.EvaluateUpVector(m_CurrentOffset);
            float3 right = math.normalize(math.cross(upSplineDirection, direction));


            m_mobTransform.transform.position = m_SplineContainer.transform.TransformPoint(posOnSplineLocal * right);

            float3 up = m_Tilt.Count == 0 ? m_Tilt.DefaultValue : m_Tilt.Evaluate(m_Spline, m_CurrentOffset, PathIndexUnit.Normalized, new Interpolators.LerpFloat3());

            Quaternion rot = Quaternion.LookRotation(direction, upSplineDirection);
            m_mobTransform.transform.rotation = Quaternion.LookRotation(direction, rot * up);
        }
    }
}