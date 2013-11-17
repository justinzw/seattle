using Microsoft.Kinect.Toolkit.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battlehack
{
    public static class Navigation
    {
        public static Frame Frame;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SpeechRecognitionEngine sre;
        public MainWindow()
        {
            InitializeComponent(); sre = new SpeechRecognitionEngine();
            mainFrame.Navigate(new Uri("/MainSelector.xaml", UriKind.Relative));
            Navigation.Frame = mainFrame;
            SpeechInit();
        }

        public override void BeginInit()
        {
            base.BeginInit();
        }

        private void SpeechInit()
        {
            // Create a new SpeechRecognitionEngine instance.

            // Create a simple grammar that recognizes "red", "green", or "blue".
            Choices colors = new Choices();
            colors.Add(new string[] { "red", "green", "blue" });

            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance and load it into the speech recognition engine.
            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            // Configure the input to the speech recognizer.
            sre.SetInputToDefaultAudioDevice();

            // Register a handler for the SpeechRecognized event.
            sre.SpeechRecognized += sre_SpeechRecognized;
            // Start asynchronous, continuous speech recognition.
            sre.RecognizeAsync(RecognizeMode.Single);

        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Speech recognized: " + e.Result.Text);
        }        
    }
}
