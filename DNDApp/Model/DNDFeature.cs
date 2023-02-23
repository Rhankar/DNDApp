using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDFeature
    {
        protected DNDCharacter _owner;

        public string Name { get; set; }
        private List<string> _entry;
        public List<string> Entry
        {
            get
            {
                if (ReferenceEquals(_entry, null))
                {
                    _entry = new List<string>();
                }

                return _entry;
            }
            set
            {
                _entry = value;
            }
        }

        private List<DNDFeatureEffect> _effects;
        public List<DNDFeatureEffect> Effects
        {
            get
            {
                if (ReferenceEquals(_effects, null))
                {
                    _effects = new List<DNDFeatureEffect>();
                }

                return _effects;
            }
            set
            {
                _effects = value;
            }
        }

        public virtual void ApplyFeatures(DNDCharacter owner)
        {
            _owner = owner;

            if (Effects.Count == 0)
            {
                return;
            }

            foreach (DNDFeatureEffect e in Effects)
            {
                e.ApplyEffect(owner);
            }
        }

        public virtual void RemoveFeatures(DNDCharacter owner)
        {
            if (Effects.Count == 0)
            {
                return;
            }

            foreach (DNDFeatureEffect e in Effects)
            {
                e.RemoveEffect(owner);
            }
        }

        public void SetOwner(DNDCharacter owner)
        {
            _owner = owner;
        }
    }

    public class DNDTogglableFeature : DNDFeature
    {
        private bool _toggled;
        public bool Toggled
        {
            get
            {
                return _toggled;
            }
            set
            {
                if (_toggled == value) return;

                if (value)
                {
                    _toggled = value;
                    ApplyFeatures(_owner);
                }
                else
                {
                    RemoveFeatures(_owner);
                    _toggled = value;
                }
            }
        }

        public override void ApplyFeatures(DNDCharacter owner)
        {
            //only add if it's toggle on

            if (!Toggled)
            {
                return;
            }

            base.ApplyFeatures(owner);
        }

        public override void RemoveFeatures(DNDCharacter owner)
        {
            //only remove if it was toggled on

            if (!Toggled)
            {
                return;
            }

            base.RemoveFeatures(owner);
        }
    }
}
