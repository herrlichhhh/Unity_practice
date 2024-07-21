using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlatesCounter : BaseCounter {

        public event EventHandler OnPlateRemoved;
        public event EventHandler OnPlateSpawned;

        [SerializeField] private KitchenObjectSO plate_kitchen_object_SO_;
        private float spawn_plate_timer_;
        private float plate_max_timer_ = 4f;
        private int plate_count_;
        private int plate_max_ = 4;

        private void Update() {

                spawn_plate_timer_ += Time.deltaTime;
                if (spawn_plate_timer_ > plate_max_timer_) {
                        spawn_plate_timer_ = 0f;
                        if (plate_count_ < plate_max_) {
                                plate_count_++;
                               
                                OnPlateSpawned?.Invoke(this, EventArgs.Empty);

                        }
                }
        }

        public override void Interact(Player player) {
                if (!player.HasKitchenObject()) {
                        if(plate_count_ > 0) {
                                plate_count_--;

                                KitchenObject.SpawnKitchenObject(plate_kitchen_object_SO_, player);

                                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
                        }
                }
        }

}
