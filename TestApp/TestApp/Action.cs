using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    class Action
    {
        public delegate void ShowFunc(ComfortDevTestContext db);
        public delegate void AddFunc(ComfortDevTestContext db, List<string> args);
        public ShowFunc Show { get; }
        public AddFunc Add { get; }
        public Action(ShowFunc show, AddFunc add)
        {
            Show = show;
            Add = add;
        }
    }
}
