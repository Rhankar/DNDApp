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
            ff.Name = "Feature A";
            ff.Entry.Add("TEXT");
            ff.Toggled = false;
            ff.SetOwner(owner);
            RaceInfo.Features.Add(ff);

            DNDFeature f = new DNDFeature();
            f.Name = "Feature B";
            f.Entry.Add("TEXT");
            f.SetOwner(owner);
            AddProficiencyEffect effect = new AddProficiencyEffect();
            effect.skillToImprove = SkillTypes.Nature;
            f.Effects.Add(effect);
            RaceInfo.Features.Add(f);

            f = new DNDFeature();
            f.Name = "Feature D";
            f.Entry.Add("TEXT");
            f.SetOwner(owner);
            AddActionEffect efffect = new AddActionEffect();
            DNDAction action = new DNDAction();
            action.ActionType = ActionType.Action;
            action.Name = "Action C";
            action.Description = "Description C";
            efffect.actionToAdd = action;
            f.Effects.Add(efffect);

            RaceInfo.Features.Add(f);
            ff = new DNDTogglableFeature();
            ff.Name = "Feature C";
            ff.Entry.Add("TEXT");
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
