using Model;
using Model.Extensions.DeclarationParsing;
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
using Model.Extensions.IEnumerable;
using Model.Extensions.String;
using Model.Extensions;
using System.IO;

namespace ViewModel
{
    //TODO: handle same file open bars do not refresh bug.
    //TODO: unknown declarations c<> remove from show list
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
                    AssemblyTypesInfo?.Namespaces?.TryGetValue(value, out selectedNamespace);
                OnPropertyChanged(nameof(Types));
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
                    string typeName = value.GetLastWord();
                    NamespaceTypesInfo ns = null;
                    if (AssemblyTypesInfo?.Namespaces?.TryGetValue(SelectedNamespace, out ns) ?? false)
                        selectedType = ns.typeInfos?.Find(x => x.Name == typeName);
                }
                OnPropertyChanged(nameof(Fields));
                OnPropertyChanged(nameof(Properties));
                OnPropertyChanged(nameof(Methods));
            }
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
                IEnumerable<string> value = selectedNamespace?.GetTypesDeclarations();
                return value;
            }
        }

        public IEnumerable<string> Fields
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                    result = selectedType.DeclaredFields.Select(fieldInfo => { return fieldInfo.GetDeclaration(); }).ToList();
                return result;
            }
        }

        public IEnumerable<string> Properties
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                    result = selectedType.DeclaredProperties.Select(propInfo => { return propInfo.GetDeclaration(); }).ToList();
                return result;
            }
        }

        public IEnumerable<string> Methods
        {
            get
            {
                IEnumerable<string> result = null;
                if (selectedType != null)
                {
                    result = selectedType.DeclaredMethods.Select(mi => {
                        if (!AssemblyTypesInfo.IsExtensionMethod(mi, selectedType))
                            return mi.GetDeclaration();
                        else
                            return null;//extension methods where transported to extended class 
                    }).ToList();
                    AddExtMethodsToMethodShowListOfSelectedType(ref result);
                    result = result.Concat(selectedType.DeclaredConstructors.Select(ci => { return ci.GetDeclaration(); }).ToList());
                }
                return result.ClearOfNulls();
            }
        }

        //TODO: Add null showList parametr concatenation
        private void AddExtMethodsToMethodShowListOfSelectedType(ref IEnumerable<string> showList)
        {
            IEnumerable<string> extensionMethodsDeclarations = AssemblyTypesInfo.GetExtensionMethods(selectedType)?.Select(mi => mi.GetDeclaration()).ToList();
            if (extensionMethodsDeclarations != null)
                showList = showList?.Concat(extensionMethodsDeclarations).ToList();
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
