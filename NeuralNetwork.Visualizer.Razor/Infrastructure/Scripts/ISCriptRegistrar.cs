﻿using System.Threading.Tasks;

namespace NeuralNetwork.Visualizer.Razor.Infrastructure.Scripts
{
   internal interface IScriptRegistrar
   {
      ValueTask Register(IScriptRegistration script);
   }
}