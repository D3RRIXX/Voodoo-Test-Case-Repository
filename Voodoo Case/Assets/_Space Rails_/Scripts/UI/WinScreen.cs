using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceRails.UI
{
	public class WinScreen : MonoBehaviour
	{
		public void Continue()
		{
			int sceneCount = SceneManager.sceneCountInBuildSettings;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1 % sceneCount);
		}
	}
}