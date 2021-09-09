using System.Collections.Generic;

namespace DecisionMaking
{
    public interface ICriteria
    {
        List<GameVector> Optimum();
    }
}
