using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject {

        public KitchenObjectSO input_;
        public KitchenObjectSO output_;
        public int cuting_progress_max_;
}

