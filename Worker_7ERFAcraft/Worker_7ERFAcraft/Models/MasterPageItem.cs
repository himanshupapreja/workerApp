using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Worker_7ERFAcraft.Models
{
    public class MasterPageItem : INotifyPropertyChanged
    {
        // event handler for updating the list views
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Title { get; set; } 
        private ImageSource _icon { get; set; }
        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }

        public Type TargetType { get; set; }
       // public Color BGColor { get; set; }
        private Color _bGColor { get; set; }
        public Color BGColor
        {
            get { return _bGColor; }
            set
            {
                _bGColor = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }
        //public Color TxtColor { get; set; }
        private Color _txtColor { get; set; }
        public Color TxtColor
        {
            get { return _txtColor; }
            set
            {
                _txtColor = value;

                OnPropertyChanged(); // Notify, that SelectedImage has been changed
            }
        }
    }
}
