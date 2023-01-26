using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp.Model
{
    public enum CreatureSize
    {
        Tiny,
        Small,
        Medium,
        Large,
    }

    public class DNDRace
    {
        private DNDCharacter _owner;

        public string Name { get; set; }
        public CreatureSize Size { get; set; }
        public DNDSpeeds Speeds { get; set; }

        public DNDRaceInfo RaceInfo { get; set; }

        private ObservableRangeCollection<DNDAbilityScoreIncrease> _scoreIncreases;
        public ObservableRangeCollection<DNDAbilityScoreIncrease> ScoreIncreases { get
            {
                if(ReferenceEquals(_scoreIncreases, null))
                {
                    _scoreIncreases = new ObservableRangeCollection<DNDAbilityScoreIncrease>();
                }
                
                return _scoreIncreases;
            }
            set
            {
                _scoreIncreases = value;
            }
        }

        public DNDRace(DNDCharacter owner)
        {
            _owner = owner;
            RaceInfo = new DNDRaceInfo();
            RaceInfo.Features = new List<DNDFeature>();

            DNDTogglableFeature ff = new DNDTogglableFeature();
            ff.Name = "A";
            ff.Entry = "TEXT";
            ff.Toggled = false;
            ff.SetOwner(owner);
            RaceInfo.Features.Add(ff);

            DNDFeature f = new DNDFeature();
            f.Name = "B";
            f.Entry = "TEXT";
            f.SetOwner(owner);
            AddProficiencyEffect effect = new AddProficiencyEffect();
            effect.skillToImprove = SkillTypes.Nature;
            f.Effects.Add(effect);

            RaceInfo.Features.Add(f);
            ff = new DNDTogglableFeature();
            ff.Name = "C";
            ff.Entry = "TEXT";
            ff.Toggled = false;
            ff.SetOwner(owner);
            effect = new AddProficiencyEffect();
            effect.skillToImprove = SkillTypes.Performance;
            ff.Effects.Add(effect);
            RaceInfo.Features.Add(ff);
        }

        //public void ApplyRaceFeatures()
        //{
        //    foreach (DNDFeature f in RaceInfo.Features)
        //    {
        //        if (f is IFeature appliableFeature)
        //        {
        //            appliableFeature.ApplyFeature(_owner);
        //        }
        //    }
        //}

        public void ApplyRaceFeatures()
        {
            foreach (DNDFeature f in RaceInfo.Features)
            {
                f.ApplyFeatures(_owner);
            }
        }

        public void ApplyRaceAbilityScoreIncreases()
        {
            foreach(DNDAbilityScoreIncrease score in _scoreIncreases)
            {
                score.ApplyStatBonus();
            }
        }
    }
}
