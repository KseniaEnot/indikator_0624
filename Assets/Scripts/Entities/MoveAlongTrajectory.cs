using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class MoveAlongTrajectory : MonoBehaviour
    {
        [field: SerializeField] private float _speed;
        [field: SerializeField] private List<Vector3> _path;

        private int _currenPoint;

        private void FixedUpdate()
        {
            if(_currenPoint >= _path.Count)
                _currenPoint = 0;
            // transform.rotation = Quaternion.LookRotation(Vector3.forward, _path[_currenPoint] - transform.position);
            transform.position = Vector2.MoveTowards(transform.position, _path[_currenPoint], _speed * Time.fixedDeltaTime);

            if (_path[_currenPoint] == transform.position)
                _currenPoint++;
        }
    }
}
