using TP.InformationComputation.LayeredArchitecture.Logic.AbstractLayerInterface;

namespace TP.InformationComputation.LayeredArchitecture.Logic
{
  /// <summary>
  /// Class ServiceC - an example of the indirect circular reference (recursion) at design time
  /// </summary>
  internal class ServiceC : IService
  {
    public IService? Service { get; set; }
  }
}