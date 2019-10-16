using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelMediator 
    {
        public void OpenAssembly(object o, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Assemblies|*.dll;*.exe",
                Title = "Select assembly",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //assembly = new AssemblyStringProcessor(openFileDialog.FileName);
                //OnPropertyChanged(nameof(Namespaces));
                //Namespace = null;
                //Datatype = null;
            }
        }

        public void Exit(object o, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
