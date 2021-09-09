﻿using System;

namespace Genetic.Algorithm
{
    public class Evolution
	{
		public Population Population { get; set; }

		public Evolution(int num)
		{
			Population.Init();
			Population = new Population(num);
			Genotype.Init();
		}

		public Genotype[] StartEvolution(int maxNum)
		{
			Population.GenerateRandomPopulation();
			for (int i = 0; i < maxNum; i++)
				Population.ChangeGeneration();
			return Population.Members;
		}

		public static double GetFitnessFunction(int x1, int x2, int x3, int x4)
		{
			return Math.Abs(x1 - x2) + Math.Abs(x3 - x4) + Math.Abs(x1 * Math.Sin(x2));
		}
	}
}
