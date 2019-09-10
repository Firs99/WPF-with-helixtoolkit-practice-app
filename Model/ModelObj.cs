using _3DHelixToolKitApp.Helpers;
using System.Windows.Media.Media3D;

namespace _3DHelixToolKitApp.Model
{
    //Главная модель 
    public class ModelObj : ObservebledObject
    {
        private double minimumSlider;

        public double MinimumSlider
        {
            get { return minimumSlider; }

            set
            {
                if (minimumSlider != value)
                {
                    minimumSlider = value;
                    OnPropertyChanged("MinimumSlider");
                }
            }
        }

        private double maximumSlider;

        public double MaximumSlider
        {
            get { return maximumSlider; }

            set
            {
                if (maximumSlider != value)
                {
                    maximumSlider = value;
                    OnPropertyChanged("MaximumSlider");
                }
            }
        }

        private double offset;

        public double Offset
        {
            get { return offset; }

            set
            {
                if(offset != value)
                {
                    offset = value;
                    OnPropertyChanged("Offset");
                }
            }
        }

        private string path;
        public string Path
        {
            get { return path; }

            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        private string name;

        public string Name
        {
            get { return name; }

            set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private Model3DGroup modelGroup;
        
        public Model3DGroup ModelGroup
        {
            get { return modelGroup; }

            set
            {
                if (modelGroup != value)
                {
                    modelGroup = value;
                    OnPropertyChanged("ModelGroup");
                }
            }
        }
    }
}
