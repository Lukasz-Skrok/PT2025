using TP.InformationComputation.LayeredArchitecture.Data;

namespace TP.InformationComputation.LayeredArchitecture.Instrumentation
{
  public class DataLayerAbstractFixture : DataLayerAbstract
  {
    internal int ConnectedCallCount = 0;

    #region DataLayerAbstract

    public override void Connect()
    {
      ConnectedCallCount++;
    }

    #endregion DataLayerAbstract
  }
}