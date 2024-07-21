using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual: MonoBehaviour {

        [SerializeField] private PlatesCounter plate_counter_;
        [SerializeField] private Transform count_top_point_;
        [SerializeField] private Transform plate_visual_prefab_;

        private List<GameObject> plate_visual_objects_list_;

        private void Awake() {
                plate_visual_objects_list_ = new List<GameObject>();
        }


        private void Start() {
                plate_counter_.OnPlateSpawned += Plate_counter__OnPlateSpawned;
                plate_counter_.OnPlateRemoved += Plate_counter__OnPlateRemoved;
        }

        private void Plate_counter__OnPlateRemoved(object sender, System.EventArgs e) {
                GameObject plate_game_object = plate_visual_objects_list_[^1];
                plate_visual_objects_list_.Remove(plate_game_object);
                Destroy(plate_game_object);
        }

        private void Plate_counter__OnPlateSpawned(object sender, System.EventArgs e) {
                Transform plate_visual_transform = Instantiate(plate_visual_prefab_, count_top_point_);

                float plate_offset = 0.1f;
                plate_visual_transform.localPosition = new Vector3(0, plate_offset * plate_visual_objects_list_.Count, 0);
                plate_visual_objects_list_.Add(plate_visual_transform.gameObject);
        }
}