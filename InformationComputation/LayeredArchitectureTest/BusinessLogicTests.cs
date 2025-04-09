#nullable disable
using TP.InformationComputation.LayeredArchitecture.Instrumentation;
using TP.InformationComputation.LayeredArchitecture.Logic.AbstractLayerInterface;

namespace TP.InformationComputation.LayeredArchitecture
{
  [TestClass()]
  public class BusinessLogicTests
  {
    [TestMethod()]
    public void ConstructorTest()
    {
      DataLayerAbstractFixture dataLayerTestingFixture = new DataLayerAbstractFixture();
      ILogic logic = LayerFactory.CreateLayer(dataLayerTestingFixture);
      Assert.AreEqual<int>(1, dataLayerTestingFixture.ConnectedCallCount);
      Assert.IsNotNull(logic.NextService);
      Assert.IsNotNull(logic.NextService.Service.Service);
      Assert.IsNotNull(logic.NextService.Service); Assert.AreNotSame(logic.NextService, logic.NextService.Service);
      Assert.AreSame(logic.NextService, logic.NextService.Service.Service);
    }
  }
}
#nullable restore