using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {

        [SerializeField] private PlateKitchenObject plate_kitchen_object_;
        [SerializeField] private Transform icon_template_transform_;

        private void Awake() {
                icon_template_transform_.gameObject.SetActive(false); 
        }

        private void Start() {
                plate_kitchen_object_.OnIngredientAdded += Plate_kitchen_object__OnIngredientAdded;
        }

        private void Plate_kitchen_object__OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
                UpdateVisual();
        }

        private void UpdateVisual() {
                foreach(Transform child in transform) {
                        if (child != icon_template_transform_) {
                                Destroy(child.gameObject);
                        }
                }

                foreach(KitchenObjectSO kitchen_object_SO in plate_kitchen_object_.GetKitchenObjectSOList()) {
                        Transform icon_transform = Instantiate(icon_template_transform_, transform);
                        icon_transform.gameObject.SetActive(true);
                        icon_transform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchen_object_SO);
                }
        }
}