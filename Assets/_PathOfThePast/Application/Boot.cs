using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PathOfThePast.Boot
{
    public class Boot : MonoBehaviour
    {
        private async void Start()
        {
            await SceneManager.LoadSceneAsync("Statics", LoadSceneMode.Additive);
            await SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Additive);
            
        }
    }
}
