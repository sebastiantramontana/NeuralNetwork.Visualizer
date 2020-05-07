namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptInstanceRegistration
   {
      internal ScriptInstanceRegistration(string functionName)
      {
         this.FunctionName = functionName;
      }

      internal string FunctionName { get; }
   }
}
