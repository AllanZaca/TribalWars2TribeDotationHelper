using System.Collections.Generic;
using System.ComponentModel;

namespace TribalWars2TribeDotation
{
    public class ProgramData : INotifyPropertyChanged
    {
        public List<TribeMember> TribeMembers { get; set; }
        public List<DotationRole> DotationRoles { get; set; }
        public List<Dotation> Dotations { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ProgramData()
        {
            TribeMembers = new List<TribeMember>();
            DotationRoles = new List<DotationRole>();
            Dotations = new List<Dotation>();
        }

        public void AddTribeMember(TribeMember tribeMember)
        {
            TribeMembers.Add(tribeMember);
            OnPropertyChanged("TribeMembers");
        }

        public void AddDotation(Dotation dotation)
        {
            Dotations.Add(dotation);
            OnPropertyChanged("Dotations");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
