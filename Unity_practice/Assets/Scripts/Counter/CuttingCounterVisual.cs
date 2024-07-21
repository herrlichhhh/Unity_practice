using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

        [SerializeField] private CuttingCounter cutting_conter_;

        private const string CUT = "Cut";
        private Animator animator_;

        private void Awake() {
                animator_ = GetComponent<Animator>();
        }

        private void Start() {
                cutting_conter_.OnCut += Cutting_conter__OnCut;          
        }

        private void Cutting_conter__OnCut(object sender, System.EventArgs e) {
                animator_.SetTrigger(CUT);
        }

}
