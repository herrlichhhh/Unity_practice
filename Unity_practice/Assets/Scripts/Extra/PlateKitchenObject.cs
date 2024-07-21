using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

        public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
        public class OnIngredientAddedEventArgs : EventArgs{
                public KitchenObjectSO kitchen_object_SO_;
        }

        [SerializeField] private List<KitchenObjectSO> valid_SO_list_;

        private List<KitchenObjectSO> kitchen_object_SO_list_;

        private void Awake() {
                kitchen_object_SO_list_ = new List<KitchenObjectSO>();
        }

        public bool AddIngredient(KitchenObjectSO kitchen_object_SO) {
                if (!valid_SO_list_.Contains(kitchen_object_SO)) {
                        Debug.Log("not valid");
                        return false;
                }
                else if (kitchen_object_SO_list_.Contains(kitchen_object_SO)) {
                        Debug.Log("on the plate");
                        return false; 
                }
                else {
                        kitchen_object_SO_list_.Add(kitchen_object_SO);

                        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                                kitchen_object_SO_ = kitchen_object_SO
                        });

                        return true;
                }

        }

        public List<KitchenObjectSO> GetKitchenObjectSOList() {
                return kitchen_object_SO_list_;
        }

}