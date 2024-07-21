using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectSO kitchen_object_SO_;

        public override void Interact(Player player) {
                Debug.Log("Interact!");
                if (!player.HasKitchenObject()) {

                        KitchenObject.SpawnKitchenObject(kitchen_object_SO_, player);

                        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                }

        }
}
