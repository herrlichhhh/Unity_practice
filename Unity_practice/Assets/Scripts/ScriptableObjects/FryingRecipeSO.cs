using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject {

        public KitchenObjectSO input_;
        public KitchenObjectSO output_;
        public float frying_time_max_;
}

