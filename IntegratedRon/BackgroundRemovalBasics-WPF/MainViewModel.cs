using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using System.Linq;

namespace Microsoft.Samples.Kinect.BackgroundRemovalBasics
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    class MainViewModel : Observable
    {
        private readonly IHelixViewport3D viewport;

        private readonly Dispatcher dispatcher;

        private string currentModelPath;

        private Model3D currentModel;

        //private string texture1, texture2, currentTexture;
        private int currentindex;

        private string[] shirtList = {
            @"..\..\Images\kraken.obj",
            @"..\..\Images\hack.obj",
            @"..\..\Images\sendgrid.obj",
            @"..\..\Images\nokia.obj",
            @"..\..\Images\twilio.obj",
            @"..\..\Images\macklemores.obj",
            @"..\..\Images\paypal.obj"};

        MeshGeometry3D mesh;
        public MeshGeometry3D Mesh
        {
            get
            {
                return this.mesh;
            }
            set
            {
                this.mesh = value;
            }
        }


        public Model3D CurrentModel
        {
            get
            {
                return this.currentModel;
            }

            set
            {
                this.currentModel = value;
                this.RaisePropertyChanged("CurrentModel");
            }
        }

        public string CurrentModelPath
        {
            get
            {
                return this.currentModelPath;
            }

            set
            {
                this.currentModelPath = value;
                this.RaisePropertyChanged("CurrentModelPath");
            }
        }

        public MainViewModel(HelixViewport3D viewport)
        {
            if (viewport == null)
            {
                throw new ArgumentNullException("viewport");
            }

            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.viewport = viewport;

            this.currentModelPath = shirtList[0];
        }

        public void ChangeShirt()
        {
            currentindex++;
            this.CurrentModelPath = shirtList[currentindex % shirtList.Count()];
            FileOpen();
        }

        public async void FileOpen()
        {
            this.CurrentModel = await this.LoadAsync(this.CurrentModelPath, false);
            this.viewport.ZoomExtents(0);
            //ChangeTexture();
        }

        private async Task<Model3DGroup> LoadAsync(string model3DPath, bool freeze)
        {
            return await Task.Factory.StartNew(() =>
            {
                var mi = new ModelImporter();

                if (freeze)
                {
                    // Alt 1. - freeze the model 
                    return mi.Load(model3DPath, null, true);
                }

                // Alt. 2 - create the model on the UI dispatcher
                return mi.Load(model3DPath, this.dispatcher);
            });
        }
    }
}
