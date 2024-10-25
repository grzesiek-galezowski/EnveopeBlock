using BuzzGUI.Interfaces;

namespace EnvelopeBlock
{
    public class MachineGUIFactory : IMachineGUIFactory
    {
        public IMachineGUI CreateGUI(IMachineGUIHost host) { return new EnvelopeMachineGUI(); }
    }
}