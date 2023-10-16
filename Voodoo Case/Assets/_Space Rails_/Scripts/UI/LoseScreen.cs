using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceRails.UI
{
	public class LoseScreen : MonoBehaviour
	{
		public void Revive()
		{
			throw new NotImplementedException();
		}

		public void BackToMenu()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}