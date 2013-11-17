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
using System.Windows.Media;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    class MainViewModel : Observable
    {
        private readonly IHelixViewport3D viewport;

        private readonly Dispatcher dispatcher;

        private string currentModelPath;

        private Model3D currentModel;

        //public double WindSpeed { get; set; }
        //public double WindDirection { get; set; }
        //public double FPS { get; private set; }

        //private static VerletIntegrator integrator;
        //public Vector3D Gravity = new Vector3D(0, 0, -9);


        //public Vector3D Wind
        //{
        //    get
        //    {
        //        double dir = WindDirection / 180 * Math.PI;
        //        return new Vector3D(Math.Cos(dir) * WindSpeed, Math.Sin(dir) * WindSpeed, 0);
        //    }
        //}

        //public double Damping { get; set; }

        //private int m = 48;
        //private int n = 32;
        //private double relax = 1.5;
        //private double normalforcecoeff = 10.0;

        //private double tangentforcecoeff = 0.1;
        private string texture1, texture2, currentTexture;


        private double Mass { get; set; }

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

            this.currentModelPath = @"singlet.stl";

            //Damping = 0.98;
            //integrator = new VerletIntegrator() { Iterations = 4, Damping = this.Damping };
            //WindSpeed = 6;
            //WindDirection = 180;
            //Mass = 0.8;

            texture1 = @"D:\Projects\FlagOfNorway.png";
            texture2 = @"D:\Projects\Cloth_Texture.jpg";

        }

        //public ICommand FileOpenCommand { get; set; }

        public async void FileOpen()
        {
            var tempModel = await this.LoadAsync(this.CurrentModelPath, false);



            //integrator.Init(Mesh);
            //CreateConstraints();
            //integrator.SetInverseMass(1 / Mass);

            this.CurrentModel = tempModel;
            this.viewport.ZoomExtents(0);
        }

        public void ChangeTexture()
        {
            var tempModel = this.CurrentModel;


            tempModel.Traverse<GeometryModel3D>((geometryModel, transform) =>
            {
                //this.Mesh = (MeshGeometry3D)geometryModel.Geometry;
                //@"D:\Projects\FlagOfNorway.png"
                if (currentTexture == texture1)
                {
                    currentTexture = texture2;

                }
                else
                {
                    currentTexture = texture1;
                }
                geometryModel.Material = MaterialHelper.CreateImageMaterial(currentTexture);
                geometryModel.BackMaterial = MaterialHelper.CreateImageMaterial(currentTexture);
            });

            //integrator.Init(Mesh);
            //CreateConstraints();
            //integrator.SetInverseMass(1 / Mass);

            this.CurrentModel = tempModel;

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


        //private void CreateConstraints()
        //{
        //    for (int i = 0; i < n; i++)
        //        for (int j = 0; j < m; j++)
        //        {
        //            int ij = i * m + j;
        //            if (j + 1 < m)
        //                integrator.AddConstraint(ij, ij + 1, relax);
        //            if (i + 1 < n)
        //                integrator.AddConstraint(ij, ij + m, relax);
        //        }
        //}

        //private void AccumulateForces()
        //{
        //    if (null == Mesh)
        //    {
        //        return;
        //    }
        //    //integrator.Damping = this.Damping;
        //    //var wind = Wind;
        //    //var gravity = Gravity;
        //    //double mass = Mass;
        //    //Vector3D[] normals = MeshGeometryHelper.CalculateNormals(this.Mesh).ToArray();
        //    //for (int i = 0; i < normals.Length; i++)
        //    //{
        //    //    var n = normals[i];
        //    //    var F = n * Vector3D.DotProduct(n, wind) * normalforcecoeff + wind * tangentforcecoeff;
        //    //    F += gravity * mass;
        //    //    integrator.SetForce(i, F);
        //    //}
        //}

        //public void Update(double dt)
        //{
        //    if (null == Mesh)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        UpdateFps(dt);
        //        AccumulateForces();
        //        integrator.TimeStep(dt);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //private double timeFrames = 0;
        //private int nFrames = 0;
        //private void UpdateFps(double dt)
        //{
        //    timeFrames += dt;
        //    nFrames++;
        //    if (timeFrames > 1)
        //    {
        //        FPS = nFrames / timeFrames;
        //        //RaisePropertyChanged("FPS");
        //        timeFrames = 0;
        //        nFrames = 0;
        //    }
        //}

        //public void Transfer()
        //{
        //    if (null == Mesh)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        integrator.TransferPositions(Mesh);
        //        CurrentModel.Traverse<GeometryModel3D>((geometryModel, transform) =>
        //        {
        //            geometryModel.Geometry = this.Mesh;
        //        });
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}
