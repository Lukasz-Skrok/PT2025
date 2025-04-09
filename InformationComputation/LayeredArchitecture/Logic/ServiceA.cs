using TP.InformationComputation.LayeredArchitecture.Logic.AbstractLayerInterface;

namespace TP.InformationComputation.LayeredArchitecture.Logic
{
  /// <summary>
  /// Class ServiceA - an example of the indirect circular reference (recursion) at design time
  /// </summary>
  internal class ServiceA : IService
  {
    public ServiceA(ServiceB? serviceB)
    {
            Service = serviceB;
    }

    public IService? Service { get; set; }
  }
}