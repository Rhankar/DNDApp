using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDApp
{
    public class DNDAction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ActionType ActionType { get; set; }

    }

    public enum ActionType
    {
        Action,
        BonusAction,
        Reaction,
    }
}
