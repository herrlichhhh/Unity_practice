using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour {

        [SerializeField] private Image image_;
        public void SetKitchenObjectSO(KitchenObjectSO kitchen_object_SO) {
                image_.sprite = kitchen_object_SO.sprite_;
        }
}