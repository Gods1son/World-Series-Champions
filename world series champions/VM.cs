using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace world_series_champions
{
    class VM : INotifyPropertyChanged
    {
        #region public
        public List<string> Teams
        {
            get { return _teams; }
            set { _teams = value; OnPropertyChanged(); }
        }
        List<string> _teams = new List<string>();
        public string SelectedTeam
        {
            get { return _selectedTeam; }
            set { _selectedTeam = value;display(); OnPropertyChanged(); }
        }
        private string _selectedTeam;
        public string Count
        {
            get { return _count; }
            set { _count = value; OnPropertyChanged("Count"); }
        }
        private string _count;
        #endregion
        Dictionary<string, int> winnersAll = new Dictionary<string, int>();
        public void work()
        {
            string [] whatever = File.ReadAllLines("Teams.txt");
            Teams = whatever.ToList();
            string[] winners = File.ReadAllLines("WorldSeriesWinners.txt");
            foreach(var name in winners)
            {
                int value = 1;
                if (winnersAll.ContainsKey(name))
                {
                    winnersAll[name]++;
                }
               else { winnersAll.Add(name, value); }                
            }
        }
        public void display()
        {
            Count = SelectedTeam + " has won " + winnersAll[SelectedTeam] + " time(s)"; ;
        }
        public VM()
        {
            work();
        }




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string caller = null)
        {
            // make sure only to call this if the value actually changes

            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
