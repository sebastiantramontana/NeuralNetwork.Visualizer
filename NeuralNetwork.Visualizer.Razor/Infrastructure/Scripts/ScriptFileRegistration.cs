using System.Collections.Generic;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal class ScriptFileRegistration
   {
      public ScriptFileRegistration(string fileName)
      {
         this.FileName = fileName;
         this.InstanceRegistrations = new List<ScriptInstanceRegistration>();
      }

      internal string FileName { get; }
      internal ICollection<ScriptInstanceRegistration> InstanceRegistrations { get; }
   }
}
