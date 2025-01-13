using System;

namespace Assets.Scripts.Infrastructure
{
    public class RandomService : IRandomService
    {
        Random rnd = new Random();
        public int Next(int Min, int Max) =>
            rnd.Next(Min, Max);
    }
}
