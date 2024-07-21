using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour {

        [SerializeField] private ContainerCounter container_conter_;

        private const string OPEN_CLOSE = "OpenClose";
        private Animator animator_;

        private void Awake() {
                animator_ = GetComponent<Animator>();
        }

        private void Start() {
                container_conter_.OnPlayerGrabbedObject += Container_conter__OnPlayerGrabbedObject;
        }

        private void Container_conter__OnPlayerGrabbedObject(object sender, System.EventArgs e) {
                animator_.SetTrigger(OPEN_CLOSE);
        }
}
