using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SpaceRails.UI
{
	public class LoseScreen : MonoBehaviour
	{
		private ZenjectSceneLoader _zenjectSceneLoader;

		[Inject]
		private void Construct(ZenjectSceneLoader zenjectSceneLoader)
		{
			_zenjectSceneLoader = zenjectSceneLoader;
		}
		
		public void BackToMenu()
		{
			_zenjectSceneLoader.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}