using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ViewModel
{
    public class ViewModelMediator:INotifyPropertyChanged 
    {
        protected AssemblyTypesInfo typesInfo;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected NamespaceTypesInfo selectedNamespace;
        public string SelectedNamespace {
            get
            {
                return selectedNamespace?.Name;
            }
            set
            {
                selectedNamespace = null;
                if (value != null)
                {
                    typesInfo?.Namespaces?.TryGetValue(value, out selectedNamespace);
                    OnPropertyChanged(nameof(Types));
                }
            }
        }

        protected TypeInfo selectedType;
        public string SelectedType
        {
            get
            {
                return selectedType?.Name;
            }
            set
            {
                selectedType = null;
                if (value != null)
                {
                    NamespaceTypesInfo ns = null;
                    if (typesInfo?.Namespaces?.TryGetValue(SelectedNamespace, out ns) ?? false)
                        selectedType = ns.typeInfos?.Find(x => x.FullName == value);

                    OnPropertyChanged(nameof(Fields));
                    OnPropertyChanged(nameof(Properties));
                    OnPropertyChanged(nameof(Methods));
                }
            }
        }

        public IEnumerable<string> Namespaces
        {
            get
            {
                return typesInfo?.Namespaces?.Keys;
            }
            protected set
            {
                Namespaces = value;
            }
        }

        public IEnumerable<string> Types
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedNamespace != null)
                    result = selectedNamespace.typeInfos.Select(typeInfo => { return typeInfo.FullName; });
                return result;
            }
            protected set
            {
                Types = value;
            }
        }

        public IEnumerable<string> Fields
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                    result = selectedType.DeclaredFields.Select(fieldInfo => { return fieldInfo.Name; }).ToList();
                return result;
            }
            protected set
            {
                Types = value;
            }
        }

        public IEnumerable<string> Properties
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                    result = selectedType.DeclaredProperties.Select(propInfo => { return propInfo.Name; }).ToList();
                return result;
            }
            protected set
            {
                Types = value;
            }
        }

        public IEnumerable<string> Methods
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                    result = selectedType.DeclaredMethods.Select(methodInfo => { return methodInfo.Name; }).ToList();
                return result;
            }
            protected set
            {
                Types = value;
            }
        }


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
                typesInfo = new AssemblyTypesInfo(openFileDialog.FileName);
                SelectedNamespace = null;
                SelectedType = null;

                OnPropertyChanged(nameof(Namespaces));
                OnPropertyChanged(nameof(Types));
                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(Properties));
                OnPropertyChanged(nameof(Methods));
            }
        }

        public void Exit(object o, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
