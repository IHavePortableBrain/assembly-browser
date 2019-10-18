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
        protected AssemblyTypesInfo AssemblyTypesInfo;

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
                SelectedType = null;
                if (value != null)
                {
                    AssemblyTypesInfo?.Namespaces?.TryGetValue(value, out selectedNamespace);
                }
                OnPropertyChanged(nameof(Types));//must be first?
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
                    string typeName = GetLastWord(value);
                    NamespaceTypesInfo ns = null;
                    if (AssemblyTypesInfo?.Namespaces?.TryGetValue(SelectedNamespace, out ns) ?? false)
                        selectedType = ns.typeInfos?.Find(x => x.Name == typeName);
                }
                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(Properties));
                OnPropertyChanged(nameof(Methods));
            }
        }

        private string GetLastWord(string value)
        {
            return value.Split(' ').Last();
        }

        public IEnumerable<string> Namespaces
        {
            get
            {
                return AssemblyTypesInfo?.GetNamespacesDeclarations();
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
                return selectedNamespace?.GetTypesDeclarations();
                //IEnumerable<string> result = null;
                //if (selectedNamespace != null)
                //    result = selectedNamespace.typeInfos.Select(typeInfo => { return typeInfo.FullName; });
                //return result;
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
                AssemblyTypesInfo = new AssemblyTypesInfo(openFileDialog.FileName);
                SelectedNamespace = null;
                SelectedType = null;

                OnPropertyChanged(nameof(Namespaces));
                OnPropertyChanged(nameof(Types));
                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(Properties));
                OnPropertyChanged(nameof(Methods));
            }
        }
    }
}
