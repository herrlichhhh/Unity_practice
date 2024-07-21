using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static StoveCounter;

public class StoveCounter : BaseCounter, IProgress {

        public event EventHandler<IProgress.OnProgressChangedEventArgs> OnProgressChanged;
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public class OnStateChangedEventArgs: EventArgs {
                public State state;
        }
        
        [SerializeField] private FryingRecipeSO[] frying_recipe_SO_array_;

        public enum State {
                Idle,
                Frying,
                Fried,
                Burned,
        }

        private State state;  
        private float frying_timer_;
        private FryingRecipeSO frying_recipe_SO_;

        private void Start() {
                state = State.Idle;
        }

        private void Update() {
                if (HasKitchenObject()) {
                        switch (state) {
                                case State.Idle:
                                        break;
                                case State.Frying:

                                        frying_timer_ += Time.deltaTime;

                                        OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                progress_normalized_ = frying_timer_ / frying_recipe_SO_.frying_time_max_
                                        });

                                        if (frying_timer_ > frying_recipe_SO_.frying_time_max_) {

                                                GetKitchenObject().DestroySelf();

                                                KitchenObject.SpawnKitchenObject(frying_recipe_SO_.output_, this);
                                                
                                                Debug.Log("Fried");
                                                state = State.Fried;
                                                frying_recipe_SO_ = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                                                frying_timer_ = 0f;

                                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                                        state = state
                                                });

                                        }
                                        break;

                                case State.Fried:

                                        frying_timer_ += Time.deltaTime;

                                        OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                progress_normalized_ = frying_timer_ / frying_recipe_SO_.frying_time_max_
                                        });

                                        if (frying_timer_ > frying_recipe_SO_.frying_time_max_) {

                                                GetKitchenObject().DestroySelf();

                                                KitchenObject.SpawnKitchenObject(frying_recipe_SO_.output_, this);

                                                Debug.Log("Burned");

                                                frying_recipe_SO_ = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                                                state = State.Burned;
                                                frying_timer_ = 0f;

                                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                                        state = state
                                                });

                                                OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                        progress_normalized_ = frying_timer_ / frying_recipe_SO_.frying_time_max_
                                                });


                                        }

                                        break;

                                case State.Burned:
                                        break;
                        }
                        Debug.Log(state);
                }
        }

        public override void Interact(Player player) {
                Debug.Log("Interact!");
                if (!HasKitchenObject()) {
                        // no kitchen object here
                        Debug.Log("No object on the stove");

                        if (player.HasKitchenObject()) {

                                Debug.Log("Initialise frying");

                                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                                        
                                        Debug.Log("Cook!");

                                        player.GetKitchenObject().SetKitchenObjectParent(this);
                                        
                                        frying_recipe_SO_ = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                                        state = State.Frying;
                                        frying_timer_ = 0f;

                                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                                state = state
                                        });

                                        OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                progress_normalized_ = frying_timer_ / frying_recipe_SO_.frying_time_max_
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

                                                state = State.Idle;

                                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                                        state = state
                                                });

                                                OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                                        progress_normalized_ = 0f
                                                });
                                        };
                                }
                        }
                        else {
                                Debug.Log("Pick up from the stove");
                                
                                GetKitchenObject().SetKitchenObjectParent(player);

                                state = State.Idle;

                                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                                        state = state
                                });

                                OnProgressChanged?.Invoke(this, new IProgress.OnProgressChangedEventArgs {
                                        progress_normalized_ = 0f
                                });
                        }
                }
        }

        private bool HasRecipeWithInput(KitchenObjectSO input_kitchen_object_SO) {
                FryingRecipeSO frying_recipe_SO = GetFryingRecipeSOWithInput(input_kitchen_object_SO);
                return frying_recipe_SO != null;
        }

        private KitchenObjectSO GetOutputWithInput(KitchenObjectSO input_kitchen_object_SO) {
                FryingRecipeSO frying_recipe_SO = GetFryingRecipeSOWithInput(input_kitchen_object_SO);
                if (frying_recipe_SO != null) return frying_recipe_SO.output_;
                return null;
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input_kitchen_object_SO) {
                foreach (FryingRecipeSO frying_recipe_SO in frying_recipe_SO_array_) {
                        if (frying_recipe_SO.input_ == input_kitchen_object_SO) {
                                return frying_recipe_SO;
                        }
                }
                return null;
        }
}
