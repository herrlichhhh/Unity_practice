using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {

        [SerializeField] private Image bar_image_;
        [SerializeField] private GameObject bar_on_object_;
        
        private IProgress bar_progress_;


        private void Start() {

                bar_progress_ = bar_on_object_.GetComponent<IProgress>();
                if (bar_progress_ == null) {
                        Debug.LogError("-Nobar-");
                }

                bar_progress_.OnProgressChanged += Bar_progress__OnProgressChanged;
                
                bar_image_.fillAmount = 0f;
                
                Hide();
        }

        private void Bar_progress__OnProgressChanged(object sender, IProgress.OnProgressChangedEventArgs e) {
                bar_image_.fillAmount = e.progress_normalized_;

                if (e.progress_normalized_ == 0f || e.progress_normalized_ == 1f) {
                        Hide();
                }
                else {
                        Show();
                }
        }

        private void Show() {
                gameObject.SetActive(true);
        }

        private void Hide() {
                gameObject.SetActive(false);
        }
}