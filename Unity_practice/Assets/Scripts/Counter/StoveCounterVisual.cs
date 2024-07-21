using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
        [SerializeField] private StoveCounter stove_counter_;
        [SerializeField] private GameObject stove_on_game_object_;
        [SerializeField] private GameObject particles_game_object_;

        private void Start() {
                stove_counter_.OnStateChanged += Stove_counter__OnStateChanged;
        }

        private void Stove_counter__OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
                bool show_visual = (e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried);
                stove_on_game_object_.SetActive(show_visual);
                particles_game_object_.SetActive(show_visual);
        }        
}
