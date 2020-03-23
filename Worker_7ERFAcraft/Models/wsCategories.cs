using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Models
{
    public class CategoryModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ImageSource CategoryImage { get; set; }
        public bool isSelected { get; set; }
        private ImageSource _image { get; set; }
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }
        private ImageSource _selectedImage { get; set; }
        public ImageSource SelectedImage
        {
            get { return _selectedImage; }
            set
            {
                _selectedImage = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }

        public bool _hasShadow { get; set; }
        public bool HasShadow
        {
            get { return _hasShadow; }
            set
            {
                _hasShadow = value;

                OnPropertyChanged(); // Notify, that HasShadow has been changed
            }
        }

        public double _bGOpacity { get; set; }
        public double BGOpacity
        {
            get { return _bGOpacity; }
            set
            {
                _bGOpacity = value;

                OnPropertyChanged(); // Notify, that HasShadow has been changed
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
        public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryName_Hindi { get; set; }
        public string CategoryName_Urdu { get; set; }
        public string CategoryName_Bangali { get; set; }
        public string CategoryName_Arabic { get; set; }
        public string Image { get; set; }
    }

    public class wsCategories : wsBase
    { 
        public List<Category> categoryData { get; set; }
        public List<Worker> workersData { get; set; }
    }


     
    
 
}
