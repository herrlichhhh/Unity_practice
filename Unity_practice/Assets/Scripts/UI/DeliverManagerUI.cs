using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverManagerUI : MonoBehaviour {

        [SerializeField] private Transform container_;
        [SerializeField] private Transform recipe_template_;

        private void Awake() {
                recipe_template_.gameObject.SetActive(false);
        }

        private void Start() {
                DeliveryManager.Instance_.OnRecipeSpawned += Delivery_manager__OnRecipeSpawned;
                DeliveryManager.Instance_.OnRecipeCompleted += Delivery_manager__OnRecipeCompleted;

                UpdateVisual();
        }

        private void Delivery_manager__OnRecipeCompleted(object sender, System.EventArgs e) {
                UpdateVisual();
        }

        private void Delivery_manager__OnRecipeSpawned(object sender, System.EventArgs e) {
                UpdateVisual();
        }

        private void UpdateVisual() {
                foreach(Transform child in container_) {
                        if (child == recipe_template_) continue;
                        Destroy(child.gameObject);
                }

                foreach(RecipeSO recipe_SO in DeliveryManager.Instance_.GetWaitingRecipeSOList()) {
                        Transform recipe_transform = Instantiate(recipe_template_, container_);
                        recipe_transform.gameObject.SetActive(true);
                        recipe_transform.GetComponent<DeliverManagerSingleUI>().SetRecipeSO(recipe_SO);
                }
        }
}