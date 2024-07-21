using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour, IKitchenObjectParent{

        // Singleton Pattern
        public static Player Instance { get; private set;}
        private void Awake() {
                if (Instance != null) Debug.LogError("multi-player invalid");
                Instance = this;
        }

        // Event Module
        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChange;
        public class OnSelectedCounterChangedEventArgs: EventArgs {
                public BaseCounter selected_counter_;
        }

        // modificable variables with private decorator
        [SerializeField] private float move_speed_ = 7f;
        [SerializeField] private GameInput game_input_;
        [SerializeField] private LayerMask counter_layer_mask_;
        [SerializeField] private Transform kitchen_object_hold_point_;

        // inner variables 
        private bool is_walking_;
        private Vector3 last_interact_direction_;
        private BaseCounter selected_counter_;
        private KitchenObject kitchen_object_;

        // test encounter
        private bool is_counter_;
        private const string IS_COUNTER = "IsCounter";
        [SerializeField] private Animator animator;

        // Interaction Module
        private void Start() {
                game_input_.OnInteractAction += Game_input__OnInteractAction;
                game_input_.OnInteractAlternateAction += Game_input__OnInteractAlternateAction;
        }

        private void Game_input__OnInteractAlternateAction(object sender, EventArgs e) {
                //(!GameManager.Instance.isGamePlaying()) return;
                if(selected_counter_ != null) {
                        selected_counter_.InteractAlternate(this);
                }
        }

        private void Game_input__OnInteractAction(object sender, System.EventArgs e) {
                //(!GameManager.Instance.isGamePlaying()) return;
                if (selected_counter_ != null) {
                        selected_counter_.Interact(this);
                }
        }

        private void Update() {
                HandleMovement();
                HandleInteractions();
                animator.SetBool(IS_COUNTER, IsCounter());
        }

        public bool IsWalking() {
                return is_walking_; 
        }

        public bool IsCounter() {
                return is_counter_;
        }

        public void HandleInteractions() {
                Vector2 input_vector = game_input_.GetMovementVectorNormalized();

                Vector3 move_direction = new(input_vector.x, 0f, input_vector.y);

                if (move_direction != Vector3.zero) {
                        last_interact_direction_ = move_direction;
                }

                float interact_distance = 2f;
                if(Physics.Raycast(transform.position,
                        last_interact_direction_,
                        out RaycastHit raycastHit,
                        interact_distance,
                        counter_layer_mask_
                        )) {
                        is_counter_ = true;
                        if(raycastHit.transform.TryGetComponent(out BaseCounter base_counter)) {
                                // Has ClearCounter
                                if (base_counter != selected_counter_) {
                                        SetSelectedCounter(base_counter);
                                }
                        }
                        else {
                                SetSelectedCounter(null);
                        }
                }
                else {
                        is_counter_ = false;
                        SetSelectedCounter(null);
                }
        }

        // Movement Module: using vector / time period / speed
        public void HandleMovement() {

                Vector2 input_vector = game_input_.GetMovementVectorNormalized();

                Vector3 move_direction = new(input_vector.x, 0f, input_vector.y);

                float move_distance = move_speed_ * Time.deltaTime;
                float player_radius = .7f;
                float player_height = 2f;
                bool can_move = !Physics.CapsuleCast(
                        transform.position,
                        transform.position + Vector3.up * player_height,
                        player_radius,
                        move_direction,
                        move_distance
                        );


                if (!can_move) {
                        // obstacles/limitation

                        // Attempt only x movement
                        Vector3 move_dir_x = new Vector3(move_direction.x, 0f, 0f).normalized;
                        can_move = move_direction.x != 0 && !Physics.CapsuleCast(
                                transform.position,
                                transform.position + Vector3.up * player_height,
                                player_radius,
                                move_dir_x,
                                move_distance
                                );

                        if (can_move) {
                                move_direction = move_dir_x;
                        }
                        else {
                                Vector3 move_dir_z = new Vector3(0f, 0f, move_direction.z).normalized;
                                can_move = move_direction.z != 0 && !Physics.CapsuleCast(
                                        transform.position,
                                        transform.position + Vector3.up * player_height,
                                        player_radius,
                                        move_dir_z,
                                        move_distance
                                        );

                                if (can_move) {
                                        move_direction = move_dir_z;
                                }
                        }
                }
                if (can_move) {
                        transform.position += move_direction * (move_speed_ * Time.deltaTime);
                }


                is_walking_ = move_direction != Vector3.zero;

                float rotate_speed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, move_direction, Time.deltaTime * rotate_speed);
        }

        private void SetSelectedCounter(BaseCounter selected_counter_) {
                this.selected_counter_ = selected_counter_;

                OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangedEventArgs {
                        selected_counter_ = selected_counter_
                });
        }

        public Transform GetKitchenObjectFollowTransform() {
                return kitchen_object_hold_point_;
        }

        public void SetKitchenObject(KitchenObject kitchen_object) {
                this.kitchen_object_ = kitchen_object;
        }

        public KitchenObject GetKitchenObject() { 
                return this.kitchen_object_;
        }
        public void ClearKitchenObject() {
                kitchen_object_ = null;
        }

        public bool HasKitchenObject() {
                return this.kitchen_object_ != null;
        }
}