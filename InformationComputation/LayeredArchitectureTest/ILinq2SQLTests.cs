using TP.InformationComputation.LayeredArchitecture.Data;

namespace TP.InformationComputation.LayeredArchitecture
{
  [TestClass()]
  public class ILinq2SQLTests
  {
    [TestMethod()]
    public void CreateLinq2SQLTest()
    {
      DataLayerAbstract linq2SQL = DataLayerAbstract.CreateLinq2SQL();
      Assert.IsNotNull(linq2SQL);
      Assert.ThrowsException<NotImplementedException>(() => linq2SQL.Connect());
    }
  }
}