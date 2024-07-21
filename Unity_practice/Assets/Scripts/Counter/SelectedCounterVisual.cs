using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {

        [SerializeField] private BaseCounter base_counter_;
        [SerializeField] private GameObject[] visual_game_object_array_;

        // Start after Awake, notice the order
        private void Start() {
                Player.Instance.OnSelectedCounterChange += Player_on_selected_counter_change_;
        }

        private void Player_on_selected_counter_change_(object sender, Player.OnSelectedCounterChangedEventArgs e) {
                if(e.selected_counter_ == base_counter_) {
                        Show();
                }
                else {
                        Hide();
                }
        }

        private void Show() {
                foreach (GameObject visual_game_object in visual_game_object_array_) {
                        visual_game_object.SetActive(true);
                }
        }

        private void Hide() {
                foreach (GameObject visual_game_object in visual_game_object_array_) {
                        visual_game_object.SetActive(false);
                }
        }
}
