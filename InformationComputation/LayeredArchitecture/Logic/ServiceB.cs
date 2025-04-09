using TP.InformationComputation.LayeredArchitecture.Logic.AbstractLayerInterface;

namespace TP.InformationComputation.LayeredArchitecture.Logic
{
  /// <summary>
  /// Class ServiceB - an example of the indirect circular (recursion) reference at design time
  /// </summary>
  internal class ServiceB : IService
  {
    internal ServiceB(ServiceC service)
    {
      Service = service;
    }

    public IService? Service { get; set; }
  }
}