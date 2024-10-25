using System.ComponentModel;

namespace EnvelopeBlock
{
    public class EnvelopeBlockEvent : INotifyPropertyChanged
    {
        public int envelopPatternIndex;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}