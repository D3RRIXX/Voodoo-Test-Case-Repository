﻿using SpaceRails.Game;
using UnityEngine;
using Zenject;

namespace SpaceRails.Infrastructure
{
	[CreateAssetMenu(fileName = "Game Installer", menuName = "Space Rails/Game Installer", order = -100)]
	public class GameInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private Pole.Settings _poleSettings;
		[SerializeField] private PrefabContainer _prefabContainer;
		
		public override void InstallBindings()
		{
			Container.BindInstance(_poleSettings);
			Container.BindInstance(_prefabContainer);
		}
	}
}