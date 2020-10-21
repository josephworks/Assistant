using System;
using Microsoft.CognitiveServices.Speech;
using SpeechLib.Synthesis;
using static System.Console;

namespace ConsoleAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            String msg = "Cystemz is really bad";
            Console.WriteLine(msg);

            SpeechLib.Synthesis.SpeechSynthesis speechSynthesis = new SpeechSynthesis();
            speechSynthesis.Speak(msg);

            Console.ReadLine();
        }
    }
}
