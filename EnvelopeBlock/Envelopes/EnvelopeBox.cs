using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace EnvelopeBlock
{
    class EnvelopeBox : Canvas, INotifyPropertyChanged
    {
        public IEnvelopeLayer EnveloperLayer { get; private set; }

        private Rectangle rect;

        // Seconds from beginning from pattern
        private bool frozen;
        MenuItem miFreeze;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Frozen
        {
            get { return frozen; }
            set
            {
                frozen = value;
                DisableEnvets();
                MiFreeze.IsChecked = frozen;
                foreach (MenuItem mi in ContextMenu.Items)
                    if (mi != MiFreeze)
                        mi.IsEnabled = !frozen;
                EnableEvents();
            }
        }

        public int DraggedEnvIndex { get; internal set; }
        public int EnvelopePatternIndex { get; internal set; }
        public int EnvelopeParamIndex { get; internal set; }
        public int Index { get; internal set; }
        public MenuItem MiFreeze { get => miFreeze; set => miFreeze = value; }
        public double TimeStamp { get; internal set; }

        public EnvelopeBox(IEnvelopeLayer enveloperL, double width, double height, Brush fill, Brush stroke)
        {
            Width = width;
            Height = height;

            EnveloperLayer = enveloperL;

            rect = new Rectangle();
            rect.Width = width;
            rect.Height = height;
            rect.Fill = fill;
            rect.Stroke = stroke;

            SetLeft(rect, 0);
            SetTop(rect, 0);
            Children.Add(rect);

            ContextMenu contextMenu = new ContextMenu() { Margin = new Thickness(4, 4, 4, 4) };

            MiFreeze = new MenuItem();
            MiFreeze.Header = "Frozen";
            MiFreeze.IsCheckable = true;

            contextMenu.Items.Add(MiFreeze);

            MenuItem mi = new MenuItem();
            mi.Header = "Set Value...";
            mi.Click += Mi_Click_SetValue;
            contextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "Reset";
            mi.Click += Mi_Click_Reset;
            contextMenu.Items.Add(mi);

            mi = new MenuItem();
            mi.Header = "Delete";
            mi.Click += Mi_Click_Delete;
            contextMenu.Items.Add(mi);

            CacheMode = new BitmapCache() { EnableClearType = false, SnapsToDevicePixels = false, RenderAtScale = 1 };

            ContextMenu = contextMenu;
            frozen = true;

            MouseRightButtonDown += EnvelopeBox_MouseRightButtonDown;
            EnableEvents();

            MouseEnter += EnvelopeBox_MouseEnter;
            MouseLeave += EnvelopeBox_MouseLeave;

            Unloaded += (sender, e) =>
            {
                MouseEnter -= EnvelopeBox_MouseEnter;
                MouseLeave -= EnvelopeBox_MouseLeave;
                DisableEnvets();
                EnveloperLayer = null;
            };

            ToolTipOpening += (sender, e) =>
            {
                EnvelopeLayer el = (EnvelopeLayer)EnveloperLayer;
                el.UpdateToolTip(this);
            };
        }

        private void EnvelopeBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            EnveloperLayer.EnvelopeBoxMouseLeave();
        }

        private void EnvelopeBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            EnveloperLayer.EnvelopeBoxMouseEnter();
        }

        private void Mi_Click_SetValue(object sender, RoutedEventArgs e)
        {
            if (!Frozen)
            {
                EnveloperLayer.SetEnvelopeBoxValue(this);
            }
        }

        internal EnvelopeBox Clone(IEnvelopeLayer enveloperL)
        {
            EnvelopeBox evb = new EnvelopeBox(enveloperL, Width, Height, rect.Fill, rect.Stroke);
            evb.DraggedEnvIndex = DraggedEnvIndex;
            evb.EnvelopePatternIndex = EnvelopePatternIndex;
            evb.EnvelopeParamIndex = EnvelopeParamIndex;
            evb.Frozen = Frozen;
            evb.Index = Index;
            evb.TimeStamp = TimeStamp;

            SetLeft(evb, GetLeft(this));
            SetTop(evb, GetTop(this));

            return evb;
        }

        internal void EnableEvents()
        {
            MiFreeze.Checked += Mi_Checked;
            MiFreeze.Unchecked += Mi_Unchecked;
        }

        internal void DisableEnvets()
        {
            MiFreeze.Checked -= Mi_Checked;
            MiFreeze.Unchecked -= Mi_Unchecked;
        }

        private void EnvelopeBox_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ContextMenu != null)
                ContextMenu.IsOpen = true;

            e.Handled = true;
        }

        private void Mi_Click_Reset(object sender, RoutedEventArgs e)
        {
            if (!Frozen)
                EnveloperLayer.ResetEnvelopeBox(this);
        }

        private void Mi_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (!Frozen)
                EnveloperLayer.DeleteEnvelopeBox(this);
        }

        private void Mi_Unchecked(object sender, RoutedEventArgs e)
        {
            EnveloperLayer.SetFreezed(this, false);
        }

        private void Mi_Checked(object sender, RoutedEventArgs e)
        {
            EnveloperLayer.SetFreezed(this, true);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
        }
    }
}
