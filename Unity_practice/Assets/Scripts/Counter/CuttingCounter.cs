using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter, IProgress {

        public event EventHandler<IProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler OnCut;

        [SerializeField] private CuttingRecipeSO[] cutting_recipe_SO_array_;

        private int cutting_progress_;

        public override void Interact(Player player) {
                Debug.Log("Interact!");
                if (!HasKitchenObject()) {
                        // no kitchen object here
                        if (player.HasKitchenObject()) {
                                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())){
                                        player.GetKitchenObject().SetKitchenObjectParent(this);
                                        cutting_progress_ = 0;

                                        CuttingRecipeSO cutting_recipe_SO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                                        OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                progress_normalized_ = (float)cutting_progress_ / cutting_recipe_SO.cuting_progress_max_
                                        }); 
                                }
                        }
                        else {
                                // do nothing
                        }
                }
                else {
                        // already there
                        if (player.HasKitchenObject()) {

                                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate_kitchen_object)) {

                                        if (plate_kitchen_object.AddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                                                GetKitchenObject().DestroySelf();
                                        };
                                }
                        }
                        else {
                                GetKitchenObject().SetKitchenObjectParent(player);
                        }
                }
        }

        public override void InteractAlternate(Player player) {
                if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())){
                        // cut it 
                        cutting_progress_++;

                        OnCut?.Invoke(this, EventArgs.Empty);

                        CuttingRecipeSO cutting_recipe_SO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                progress_normalized_ = (float)cutting_progress_ / cutting_recipe_SO.cuting_progress_max_
                        });

                        if(cutting_progress_ >= cutting_recipe_SO.cuting_progress_max_) { 
                                KitchenObjectSO output_kitchen_object_SO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                                GetKitchenObject().DestroySelf();
                                Debug.Log("cut");

                                KitchenObject.SpawnKitchenObject(output_kitchen_object_SO, this);
                        }
                }
        }

        private bool HasRecipeWithInput(KitchenObjectSO input_kitchen_object_SO) {
                CuttingRecipeSO cutting_recipe_SO = GetCuttingRecipeSOWithInput(input_kitchen_object_SO);
                return cutting_recipe_SO != null;

        } 

        private KitchenObjectSO GetOutputForInput(KitchenObjectSO input_kitchen_object_SO) {
                CuttingRecipeSO cutting_recipe_SO = GetCuttingRecipeSOWithInput(input_kitchen_object_SO);
                if (cutting_recipe_SO != null) return cutting_recipe_SO.output_;
                return null;
        }

        private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input_kitchen_object_SO) {
                foreach (CuttingRecipeSO cutting_recipe_SO in cutting_recipe_SO_array_) {
                        if (cutting_recipe_SO.input_ == input_kitchen_object_SO) {
                                return cutting_recipe_SO;
                        }
                }
                return null;
        }
}