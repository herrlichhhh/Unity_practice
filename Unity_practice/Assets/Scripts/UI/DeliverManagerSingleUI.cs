using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliverManagerSingleUI : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI recipe_name_text_;
        [SerializeField] private Transform icon_container_;
        [SerializeField] private Transform icon_template_;

        public void Awake() {
                icon_template_.gameObject.SetActive(false); 
        }

        public void SetRecipeSO(RecipeSO recipe_SO) {
                recipe_name_text_.text = recipe_SO.recipe_name_;

                foreach(Transform child in icon_container_) {
                        if(child != icon_template_) {
                                Destroy(child.gameObject);
                        }
                }

                foreach(KitchenObjectSO kitchen_object_SO in recipe_SO.kitchen_object_SO_list_) {
                        Transform icon_transform = Instantiate(icon_template_, icon_container_);
                        icon_transform.gameObject.SetActive(true);
                        icon_transform.GetComponent<Image>().sprite = kitchen_object_SO.sprite_;
                }
        }

}