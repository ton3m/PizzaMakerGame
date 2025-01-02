using UnityEngine;

namespace PizzaMaker
{
    public class CockroachPatrol: MonoBehaviour
    {
        [SerializeField] private Transform _model;
        
        [SerializeField] private Transform _pointA;
        [SerializeField] private Transform _pointB;
        [SerializeField] private Transform targetPoint;
        
        [SerializeField] private float _speed = 5;
        [SerializeField] private float _speedRotation = 300;
        void Start()
        {
            targetPoint = _pointB;
            
            _model.position = _pointA.position;
        }

        private void Update()
        {
            Move(targetPoint);
            Dance();
            if (Vector3.Distance(_model.position, targetPoint.position) < 0.1f)
            {
                targetPoint = targetPoint == _pointA ? _pointB : _pointA;
            }
        }
        private void Move(Transform targetPoint)
        {
            _model.position = Vector3.MoveTowards(_model.position, targetPoint.position, _speed * Time.deltaTime);
        }

        private void Dance()
        {
            _model.Rotate(Vector3.up, _speedRotation * Time.deltaTime);
        }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.CompareTag("Player"))
        //     {
        //         //проигрывание партикла
        //         //раскидывание сегментов пиццы
        //     }
        // }
    }
}