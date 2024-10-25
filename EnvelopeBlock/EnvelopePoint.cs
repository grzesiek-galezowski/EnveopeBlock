namespace EnvelopeBlock
{
    /// <summary>
    /// Here we save all the Volume Envelope time stamps and values. Could use struct, but class it is.
    /// </summary>
    public class EnvelopePoint : IEnvelopePoint
    {
        double timeStamp;
        double value;
        //bool freezed;

        public double TimeStamp { get => timeStamp; set => timeStamp = value; }
        public double Value { get => value; set => this.value = value; }
        public bool Freezed { get; set; }

        public EnvelopePoint()
        {
            TimeStamp = -1;
            Value = 0;
            Freezed = true;
        }

        internal EnvelopePoint Clone()
        {
            EnvelopePoint ep = new EnvelopePoint();
            ep.TimeStamp = timeStamp;
            ep.Value = Value;
            ep.Freezed = Freezed;
            return ep;
        }
    }
}