using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Battlehack.Models
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    class TransformViewModel : Observable
    {
        private readonly IHelixViewport3D viewport;

        private readonly Dispatcher dispatcher;

        private string currentModelPath;

        private Model3D currentModel;

        private string texture1, texture2, currentTexture;
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

        public TransformViewModel(HelixViewport3D viewport):this(viewport, 6)
        {
        }
        public TransformViewModel(HelixViewport3D viewport, int id)
        {
            if (viewport == null)
            {
                throw new ArgumentNullException("viewport");
            }

            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.viewport = viewport;

            this.currentModelPath = shirtList[id];
        }

        public void ChangeShirt(int id)
        {
            this.CurrentModelPath = shirtList[id];
            FileOpen();
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
