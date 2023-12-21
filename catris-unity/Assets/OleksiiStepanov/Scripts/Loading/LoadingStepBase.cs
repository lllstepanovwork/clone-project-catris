using System;
using OleksiiStepanov.Game;

namespace OleksiiStepanov.Loading
{
	public abstract class LoadingStepBase
	{
		public event Action Exited;

		public abstract void Enter();

		public virtual void Exit()
		{
			if (Exited != null)
			{
				Exited.Invoke();
			}
		}

		public abstract LoadingStep GetStepType();
	}
}