using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

        [SerializeField] private Transform counter_top_point_;

        private KitchenObject kitchen_object_;

        public virtual void Interact(Player player) { }

        public virtual void InteractAlternate(Player player) { }

        public Transform GetKitchenObjectFollowTransform() {
                return counter_top_point_;
        }

        public void SetKitchenObject(KitchenObject kitchen_object) {
                this.kitchen_object_ = kitchen_object;
        }

        public KitchenObject GetKitchenObject() { return kitchen_object_; }
        public void ClearKitchenObject() {
                kitchen_object_ = null;
        }
        public bool HasKitchenObject() {
                return kitchen_object_ != null;
        }
}