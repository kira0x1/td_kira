using UnityEngine;
using UnityEngine.Splines;

namespace Kira
{
    public class Enemy : MonoBehaviour
    {
        private SplineContainer splineContainer;
        private Spline spline;

        public void Init(SplineContainer splineContainer)
        {
            this.splineContainer = splineContainer;
            spline = splineContainer.Spline;
        }
    }
}