using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

        public static GameManager Instance { get; private set; }

        private enum State {
                WaitingToStart, 
                CountdownToStart, 
                GamePlaying, 
                GameOver,
        }

        private State state;
        private float waiting_to_start_timer_ = 1f;
        private float countdown_to_start_timer = 3f;
        private float game_playing_timer = 10f;


        private void Awake() {
                Instance = this;

                state = State.WaitingToStart;
        }

        private void Update() {
                switch (state) {
                        case State.WaitingToStart:
                                waiting_to_start_timer_ -= Time.deltaTime;
                                if(waiting_to_start_timer_ < 0f) {
                                        state = State.CountdownToStart;
                                }
                                break;
                        case State.CountdownToStart:
                                countdown_to_start_timer -= Time.deltaTime;
                                if (countdown_to_start_timer < 0f) {
                                        state = State.GamePlaying;
                                }
                                break;
                        case State.GamePlaying:
                                game_playing_timer -= Time.deltaTime;
                                if (game_playing_timer < 0f) {
                                        state = State.GameOver;
                                }
                                break;
                        case State.GameOver:
                                break;
                }
                Debug.Log(state);
        }
}
