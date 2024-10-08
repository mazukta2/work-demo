﻿using System.Collections.Generic;
using UnityEngine;

namespace Debug.Positions
{
    // Puts targets on initial position on "Space" key.
    public class RestartPositions : MonoBehaviour
    {
        [SerializeField] private GameObject[] _targets;
        [SerializeField] private KeyCode _hotkey = KeyCode.Space;
        
        Dictionary<GameObject, Vector3> _positions = new ();
        Dictionary<GameObject, Quaternion> _rotations = new ();

        protected void OnEnable()
        {
            foreach (var target in _targets)
            {
                _positions[target] = target.transform.position;
                _rotations[target] = target.transform.rotation;
            }
        }

        protected void LateUpdate()
        {
            if (Input.GetKeyDown(_hotkey))
            {
                foreach (var target in _targets)
                {
                    var rigidbody = target.GetComponent<Rigidbody>();
                    if (rigidbody != null)
                    {
                        rigidbody.velocity= Vector3.zero;
                        rigidbody.angularVelocity= Vector3.zero;
                        rigidbody.position = _positions[target];
                        rigidbody.rotation = _rotations[target];
                        target.transform.position = _positions[target];
                        target.transform.rotation = _rotations[target];
                        rigidbody.ResetInertiaTensor();
                    }
                    else
                    {
                        target.transform.position = _positions[target];
                        target.transform.rotation = _rotations[target];
                    }
                }
            }
        }

        public void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Press Space to restart positions");
        }
    }
}