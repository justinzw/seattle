using Microsoft.Kinect.Toolkit.Controls;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static class Speech
    {
        public static SpeechRecognitionEngine Engine;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
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
            Speech.Engine = new SpeechRecognitionEngine();

            Choices keywords = new Choices();
            keywords.Add(new string[] { "design", "background", "photo" });

            // Create a GrammarBuilder object and append the Choices object.
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(keywords);

            // Create the Grammar instance and load it into the speech recognition engine.
            Grammar g = new Grammar(gb);
            Speech.Engine.LoadGrammar(g);

            // Configure the input to the speech recognizer.
            Speech.Engine.SetInputToDefaultAudioDevice();


        }
    
    }
}
