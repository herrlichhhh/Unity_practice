using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {

        [Serializable]
        public struct KitchenObjectSO_GameObject {
                public KitchenObjectSO kitchen_object_SO_;
                public GameObject game_object_;
        }

        [SerializeField] private PlateKitchenObject plate_kitchen_object_;
        [SerializeField] private List<KitchenObjectSO_GameObject> kitchen_object_SO_game_object_list_;

        private void Start() {
                plate_kitchen_object_.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
                
                foreach (KitchenObjectSO_GameObject kitchen_object_SO_game_object in kitchen_object_SO_game_object_list_) {
                        kitchen_object_SO_game_object.game_object_.SetActive(false);
                }
        }

        private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
                foreach(KitchenObjectSO_GameObject kitchen_object_SO_game_object in kitchen_object_SO_game_object_list_) {
                        if(kitchen_object_SO_game_object.kitchen_object_SO_ == e.kitchen_object_SO_) {
                                kitchen_object_SO_game_object.game_object_.SetActive(true);
                        }
                }
        }
}