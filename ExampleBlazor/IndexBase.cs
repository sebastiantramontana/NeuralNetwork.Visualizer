using Microsoft.AspNetCore.Components;

namespace ExampleBlazor
{
   public class IndexBase : LayoutComponentBase
   {
      public string GetTextFromMethodInClass()
      {
         return "The source for this text was external C# code in a .cs file";
      }
   }
}