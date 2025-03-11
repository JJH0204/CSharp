using System;
using CSharp_Basic.Assets;

namespace CSharp_Basic
{
  class Program
  {
    private static void Main(string[] args)
    {
      #region CSharp-Basic
      // CsharpBasic.ArrayNList();
      // CsharpBasic.DataTypes();
      // CsharpBasic.Func_Enum();
      // CsharpBasic.Func_ValueNRefernce();
      // CsharpBasic.CallByValueNCallByReference();
      // CsharpBasic.StringFormat();
      // CsharpBasic.NullCheck();
      // CsharpBasic.TryCatch();
      #endregion

      #region CSharp-Advanced
      // CSharpAdvanced.Overloading();
      // CSharpAdvanced.Params();
      // CSharpAdvanced.Optional();
      // CSharpAdvanced.Delegate();
      // CSharpAdvanced.Class();
      // CSharpAdvanced.Static();
      // CSharpAdvanced.DeepCopy();
      #endregion

      #region CSharp-Advanced-DependencyInjection
      // DI.DIMain();
      #endregion

      #region CSharp-Advanced-Delegate
      Delegate_CS delegate_CS = new Delegate_CS();
      delegate_CS.DelegateMain();
      // Delegate_CS.DelegateMain();
      #endregion
    }
  }
}