namespace NeuralNetwork.Visualizer.Html.Infrastructure.Scripts
{
   internal class GlobalInstanceScriptRegistration : IScriptRegistration
   {
      public GlobalInstanceScriptRegistration(string globalInstanceName)
      {
         this.Code = $@"window['{globalInstanceName}'] = {{}};";
      }

      public string Code { get; }
   }
}
