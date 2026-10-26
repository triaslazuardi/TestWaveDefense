using Nitzz.utility.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nitzz.Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] string gameSceneName = "Game";

        void Start()
        {
        }

        public void StartGame()
        {
            SoundManager.Play(Sfx.Click);
            SceneManager.LoadScene(gameSceneName);
        }



        public void Quit()
        {
            SoundManager.Play(Sfx.Click);
            Application.Quit(); 
        }
    }
}