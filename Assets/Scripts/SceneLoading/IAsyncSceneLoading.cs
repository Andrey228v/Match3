using Cysharp.Threading.Tasks;

namespace Assets.Scripts.SceneLoading
{
    public interface IAsyncSceneLoading
    {
        public UniTask LoadScene(string sceneName);

        public UniTask UnLoadScene(string sceneName);

        public void LoadingIsDone(bool value);
    }
}
