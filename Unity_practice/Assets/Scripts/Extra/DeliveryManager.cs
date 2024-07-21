using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour {

        public event EventHandler OnRecipeSpawned;
        public event EventHandler OnRecipeCompleted;

        public static DeliveryManager Instance_ {  get; private set; }
        [SerializeField] private RecipeListSO recipe_list_SO_;

        private List<RecipeSO> waiting_recipe_SO_list_;

        private float spawn_recipe_timer_;
        private float spawn_timer_max_ = 4f;
        private int waiting_recipes_max_ = 4;

        private void Awake() {

                Instance_ = this;       

                waiting_recipe_SO_list_ = new List<RecipeSO>();
        }

        private void Update() {
                spawn_recipe_timer_ -= Time.deltaTime;
                if(spawn_recipe_timer_ <= 0f) {
                        spawn_recipe_timer_ = spawn_timer_max_;

                        if(waiting_recipe_SO_list_.Count < waiting_recipes_max_) {
                                RecipeSO waiting_recipe_SO = recipe_list_SO_.recipe_SO_list_[UnityEngine.Random.Range(0, recipe_list_SO_.recipe_SO_list_.Count)];

                                waiting_recipe_SO_list_.Add(waiting_recipe_SO);

                                Debug.Log(waiting_recipe_SO.recipe_name_);
                                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                        }
                }
        }

        public void DeliverRecipe(PlateKitchenObject plate_kitchen_object) {
                for(int i = 0; i < waiting_recipe_SO_list_.Count; ++i) {
                        RecipeSO waiting_recipe_SO = waiting_recipe_SO_list_[i];
                        
                        if(waiting_recipe_SO.kitchen_object_SO_list_.Count == plate_kitchen_object.GetKitchenObjectSOList().Count) {
                                HashSet<KitchenObjectSO> set1 = new(waiting_recipe_SO.kitchen_object_SO_list_);
                                HashSet<KitchenObjectSO> set2 = new(plate_kitchen_object.GetKitchenObjectSOList());
                                bool same = true;
                                foreach(var obj_SO in set1) {
                                        if (!set2.Contains(obj_SO)) {
                                                same = false;
                                                break;
                                        }
                                }
                                if (same) {
                                        Debug.Log("Matching!");
                                        waiting_recipe_SO_list_.RemoveAt(i);

                                        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                                        return;
                                }
                        }
                }

                Debug.Log("wrong dish");
        }

        public List<RecipeSO> GetWaitingRecipeSOList() {
                return waiting_recipe_SO_list_;
        }
}
