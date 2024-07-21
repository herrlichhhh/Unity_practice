using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter {

        public override void Interact(Player player) {
                if (player.HasKitchenObject()) {
                        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate_kitchen_object)){

                                DeliveryManager.Instance_.DeliverRecipe(plate_kitchen_object);
                                player.GetKitchenObject().DestroySelf();
                        }
                }
        }

}