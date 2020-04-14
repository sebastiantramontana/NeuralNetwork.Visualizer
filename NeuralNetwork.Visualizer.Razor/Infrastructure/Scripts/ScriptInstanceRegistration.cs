namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptInstanceRegistration
   {
      internal ScriptInstanceRegistration(string functionName, string globalInstanceName)
      {
         this.FunctionName = functionName;
         this.GlobalInstanceName = globalInstanceName;
      }

      internal string FunctionName { get; }
      internal string GlobalInstanceName { get; }
   }
}
