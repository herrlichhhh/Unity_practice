using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

        private enum Mode {
                LookAt,
                LookAtInverted,
                CameraForward,
                CameraForwardInverted, 
        }

        [SerializeField] private Mode mode;

        private void LateUpdate() {
                switch (mode) {
                        case Mode.LookAt:
                                transform.LookAt(Camera.main.transform); // may change
                                break;
                        case Mode.LookAtInverted:
                                Vector3 direction_from_camera = transform.position - Camera.main.transform.position;
                                transform.LookAt(transform.position + direction_from_camera);
                                break;
                        case Mode.CameraForward:
                                transform.forward = Camera.main.transform.forward;      
                                break;
                        case Mode.CameraForwardInverted:
                                transform.forward = -1 * Camera.main.transform.forward;
                                break;

                }

        }

}

