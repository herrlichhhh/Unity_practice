using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour {

        [SerializeField] private KitchenObjectSO kitchen_object_SO_;

        private IKitchenObjectParent kitchen_object_parent_;

        public KitchenObjectSO GetKitchenObjectSO() {
                return kitchen_object_SO_;
        }

        public void SetKitchenObjectParent(IKitchenObjectParent kitchen_object_parent) {

                this.kitchen_object_parent_?.ClearKitchenObject();

                this.kitchen_object_parent_ = kitchen_object_parent;

                if (kitchen_object_parent_.HasKitchenObject()) {
                        Debug.LogError("already has a object");
                }

                kitchen_object_parent.SetKitchenObject(this);
                transform.parent = kitchen_object_parent.GetKitchenObjectFollowTransform();
                transform.localPosition = Vector3.zero;
        }

        public IKitchenObjectParent GetKitchenObjectParent() {
                return this.kitchen_object_parent_;
        } 

        public void DestroySelf() {
                kitchen_object_parent_.ClearKitchenObject();
                Destroy(gameObject);
        }

        public bool TryGetPlate(out PlateKitchenObject plate_kitchen_object) {
                if (this is PlateKitchenObject) {
                        plate_kitchen_object = this as PlateKitchenObject;
                        return true;
                }
                else {
                        plate_kitchen_object = null;
                        return false;
                }
        }

        public static KitchenObject SpawnKitchenObject(
                KitchenObjectSO kitchen_object_SO, 
                IKitchenObjectParent kitchen_object_parent) {

                Transform kitchen_object_transform = Instantiate(kitchen_object_SO.prefab_);
                KitchenObject kitchen_object = kitchen_object_transform.GetComponent<KitchenObject>();
                
                kitchen_object.SetKitchenObjectParent(kitchen_object_parent);

                return kitchen_object;
        }
}
