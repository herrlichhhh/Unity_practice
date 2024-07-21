using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour {

        public event EventHandler OnInteractAction;
        public event EventHandler OnInteractAlternateAction;

        private PlayerInputActions player_input_actions;

        private void Awake() {
                player_input_actions = new PlayerInputActions();
                player_input_actions.Player.Enable();

                player_input_actions.Player.Interact.performed += Interact_performed;
                player_input_actions.Player.InteractAlternate.performed += InteractAlternate_performed;
        }

        private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
                OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }

        // Event
        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
                OnInteractAction?.Invoke(this, EventArgs.Empty);
        }

        public Vector2 GetMovementVectorNormalized() {
                // separate input from movement!
                Vector2 input_vector = player_input_actions.Player.Move.ReadValue<Vector2>();

                input_vector = input_vector.normalized;

                return input_vector;
        }
}
