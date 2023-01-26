using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDCharacterBiography : INotifyPropertyChanged
    {
        private string _name;
        public string Name { 
            get
            {
                return _name;
            }
            set
            {
                if(_name == value) return;

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _nickname;
        public string Nickname {
            get
            {
                return _nickname;
            }
            set
            {
                if (_nickname == value) return;

                _nickname = value;
                OnPropertyChanged(nameof(Nickname));
            }
        }

        private string _age;
        public string Age { 
            get
            {
                return _age;
            }
            set
            {
                if(_age == value) return;

                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _height;
        public string Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (_height == value) return;

                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

        private string _weight;
        public string Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (_weight == value) return;

                _weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }

        private string _eyeColor;
        public string EyeColor
        {
            get
            {
                return _eyeColor;
            }
            set
            {
                if (_eyeColor == value) return;

                _eyeColor = value;
                OnPropertyChanged(nameof(EyeColor));
            }
        }

        private string _skinColor;
        public string SkinColor
        {
            get
            {
                return _skinColor;
            }
            set
            {
                if (_skinColor == value) return;

                _skinColor = value;
                OnPropertyChanged(nameof(SkinColor));
            }
        }

        private string _hairColor;
        public string HairColor
        {
            get
            {
                return _hairColor;
            }
            set
            {
                if (_hairColor == value) return;

                _hairColor = value;
                OnPropertyChanged(nameof(HairColor));
            }
        }

        private string _personalityTraits;
        public string PersonalityTraits
        {
            get
            {
                return _personalityTraits;
            }
            set
            {
                if (_personalityTraits == value) return;

                _personalityTraits = value;
                OnPropertyChanged(nameof(PersonalityTraits));
            }
        }

        private string _ideals;
        public string Ideals
        {
            get
            {
                return _ideals;
            }
            set
            {
                if (_ideals == value) return;

                _ideals = value;
                OnPropertyChanged(nameof(Ideals));
            }
        }

        private string _bonds;
        public string Bonds
        {
            get
            {
                return _bonds;
            }
            set
            {
                if (_bonds == value) return;

                _bonds = value;
                OnPropertyChanged(nameof(Bonds));
            }
        }

        private string _flaws;
        public string Flaws
        {
            get
            {
                return _flaws;
            }
            set
            {
                if (_flaws == value) return;

                _flaws = value;
                OnPropertyChanged(nameof(Flaws));
            }
        }

        private string _backstory;
        public string Backstory
        {
            get
            {
                return _backstory;
            }
            set
            {
                if (_backstory == value) return;

                _backstory = value;
                OnPropertyChanged(nameof(Backstory));
            }
        }

        public DNDCharacterBiography()
        {
            Name = "";
            Nickname = "";
            Age = "";
            Height = "";
            Weight = "";
            EyeColor = "";
            SkinColor = "";
            HairColor = "";
            PersonalityTraits = "";
            Ideals = "";
            Bonds = "";
            Flaws = "";
            Backstory = "";
        }

        public DNDCharacterBiography(DNDCharacterBiography copy)
        {
            Name = copy.Name;
            Nickname = copy.Nickname;
            Age = copy.Age;
            Height = copy.Height;
            Weight = copy.Weight;
            EyeColor = copy.EyeColor;
            SkinColor = copy.SkinColor;
            HairColor = copy.HairColor;
            
            PersonalityTraits = copy.PersonalityTraits;
            Ideals = copy.Ideals;
            Bonds = copy.Bonds;
            Flaws = copy.Flaws;
            Backstory = copy.Backstory;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
