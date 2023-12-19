using System;
public class GameRandom : Random
{
    public bool Test(float p)
    {
        return this.NextDouble() < p;
    }
    public bool Test(double p)
    {
        return this.NextDouble() < p;
    }
}