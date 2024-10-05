using System.Reflection;

namespace VetCheckup.Infrastructure;
public static class AssemblyUtility
{

    #region Methods

    public static Assembly GetAssembly()
        => typeof(AssemblyUtility).Assembly;

    #endregion

}
