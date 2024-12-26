using PathCreation;
using UnityEngine;

namespace PizzaMaker.Code.Core.Movement
{
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private float _followSpeed = 5;
        [SerializeField] private EndOfPathInstruction _finishBehaviour = EndOfPathInstruction.Stop;

        private float _distancePassed;

        public float PathProgress =>
            _pathCreator.path.GetPercentage(_distancePassed);
        
        public void Init(PathCreator pathCreator)
        {
            _pathCreator = pathCreator;
        }
        
        private void Update()
        {
            _distancePassed += _followSpeed * Time.deltaTime;

            transform.position = _pathCreator.path.GetPointAtDistance(_distancePassed, _finishBehaviour);
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_distancePassed, _finishBehaviour);
        }
    }
}