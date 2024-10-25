using BuzzGUI.Interfaces;
using System.Windows.Controls;

namespace EnvelopeBlock
{
    public class EnvelopeMachineGUI : UserControl, IMachineGUI
    {
        IMachine machine;
        EnvelopeBlockMachine envelopeBlockMachine;

        public IMachine Machine
        {
            get { return machine; }
            set
            {
                if (machine != null)
                {
                }

                machine = value;

                if (machine != null)
                {
                    envelopeBlockMachine = (EnvelopeBlockMachine)machine.ManagedMachine;
                }
            }
        }

        public EnvelopeMachineGUI()
        {
        }
    }
}