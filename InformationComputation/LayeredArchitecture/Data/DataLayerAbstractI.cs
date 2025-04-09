namespace TP.InformationComputation.LayeredArchitecture.Data
{
  /// <summary>
  /// Class DataLayerAbstract - an example of the abstract interface to the Data layer
  /// </summary>
  public abstract class DataLayerAbstract
  {
    /// <summary>
    /// A place holder to implement the connection functionality.
    /// </summary>
    public abstract void Connect();

    /// <summary>
    /// A factory method to provide new instance of the <see cref="DataLayerAbstract" />.
    /// </summary>
    /// <returns>An instance of DataLayerAbstract.</returns>
    public static DataLayerAbstract CreateLinq2SQL()
    {
      return new DataLayerImplementation();
    }

    #region Layer implementation

    private class DataLayerImplementation : DataLayerAbstract
    {
      /// <summary>
      /// A place holder to implement the connection functionality.
      /// </summary>
      public override void Connect()
      {
        throw new NotImplementedException();
      }

      #endregion Layer implementation
    }
  }
}