using _3DHelixToolKitApp.Helpers;
using _3DHelixToolKitApp.Model;
using HelixToolkit.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace _3DHelixToolKitApp
{
    public class MainViewModel : ObservebledObject
    {
        DispatcherTimer timer;
        ObservableCollection<ModelObj> models;
        int minRange;
        int maxRange;
        bool vectorModel = true;
        

        public MainViewModel()
        {
            models = new ObservableCollection<ModelObj>();  //внутренний список всех обьектов. //
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
        }

        private ModelObj selectedModelObj;
        public ModelObj SelectedModelObj
        {
            get { return selectedModelObj; }
            set
            {
                if (selectedModelObj != value)
                {
                    selectedModelObj = value;
                    OnPropertyChanged("SelectedModelObj");
                }
            }
        }

        public ICollectionView ModelObjs //Список всех обьектов, подготовленный к привязке.
        {
            get
            {
                return CollectionViewSource.GetDefaultView(models);
            }
        }

        //Команды 
        RelayCommand deleteModel;
        RelayCommand loadModel3D;   
        RelayCommand startMoving;
        RelayCommand stopMoving;

        //Команда движения 3D Модели, запуск по таймеру.
        public RelayCommand StartMoving
        {
            get
            {
                return startMoving ?? (startMoving = new RelayCommand(obj =>
                {
                    try
                    {
                        if (obj != null)
                        {
                            minRange = (int)this.SelectedModelObj.MinimumSlider;
                            maxRange = (int)this.SelectedModelObj.MaximumSlider;
                            timer.Start();
                        }
                    } catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
                ));
            }
        }
        //Команда остановки движения 3D Модели, стоп таймер.
        public RelayCommand StopMoving
        {
            get
            {
                return stopMoving ?? (stopMoving = new RelayCommand(obj =>
                {
                    timer.Stop();
                }
                ));
            }
        }

        //движение 3D Модели по оси Z по таймеру с интервалом 10 миллисекунд (timer.Interval = new TimeSpan(0, 0, 0, 0, 10);)
        private void timer_Tick(object sender, EventArgs e)
        {

            if (SelectedModelObj.Offset == maxRange)
            {
                vectorModel = false;
            }
            else if (SelectedModelObj.Offset == minRange)
            {
                vectorModel = true;
            }
            if (vectorModel)
                SelectedModelObj.Offset += 1.0;
            else if (!vectorModel)
                SelectedModelObj.Offset -= 1.0;
        }

        //Удаление обьекта из списка Моделей, привязка к выбранному элементу в списке.
        public RelayCommand DeleteModel
        {
            get
            {
                return deleteModel ?? (deleteModel = new RelayCommand(obj =>
                {
                    if (SelectedModelObj != null)
                    {
                        models.Remove(SelectedModelObj);
                        ModelObjs.Refresh();
                    }
                }
                ));
            }
        }
        //Загрузка файла .obj с помощью OpenFileDialog, установление первичных данных;
        public RelayCommand LoadModel3D
        {
            get
            {
                return loadModel3D ?? (loadModel3D = new RelayCommand(obj =>
                {
                    try
                    {
                        OpenFileDialog d = new OpenFileDialog();
                        d.Filter = "Object Model Files|*.obj";

                        if (d.ShowDialog() == true)
                        {
                            ObjReader reader = new ObjReader();
                            ModelObj model = new ModelObj();

                            model.Name = d.SafeFileName;
                            model.Path = d.FileName;
                            model.ModelGroup = reader.Read(d.FileName);
                            model.MinimumSlider = -5;
                            model.MaximumSlider = 5;
                            model.Offset = 0;
                            models.Add(model);
                            ModelObjs.Refresh();
                        }
                    }catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }));
            }
        }
    }
}

