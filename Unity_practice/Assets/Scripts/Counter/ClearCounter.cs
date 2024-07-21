using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

        [SerializeField] private KitchenObjectSO kitchen_object_SO_;

        public override void Interact(Player player) {
                Debug.Log("Interact!");
                if (!HasKitchenObject()) {
                        // no kitchen object here
                        if (player.HasKitchenObject()) {
                                player.GetKitchenObject().SetKitchenObjectParent(this);
                        }
                        else {
                                // do nothing
                        }
                }
                else {
                        // already there
                        if (player.HasKitchenObject()) {
                                // do nothing
                                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate_kitchen_object)){
                   
                                        if (plate_kitchen_object.AddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                                                GetKitchenObject().DestroySelf();
                                        };
                                }
                                else {
                                        if(GetKitchenObject().TryGetPlate(out plate_kitchen_object)){
                                                if (plate_kitchen_object.AddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                                                        player.GetKitchenObject().DestroySelf();
                                                }                             
                                        }
                                }
                        }
                        else {
                                GetKitchenObject().SetKitchenObjectParent(player);
                        }
                }
        }
}
